<#

.SYNOPSIS
One way sync of Microsoft Office365 hosts & networks into Check Point groups.

.DESCRIPTION
This script will create/update Check Point groups for each Microsoft Office365 product, with the list of hosts & networks Microsoft publish.

.PARAMETER ManagementServer
IP or Hostname of the Check point Management Server

.PARAMETER ManagementPort
Port Web API running on.

.PARAMETER Credentials
PSCredential containing User name and Password. If not provided you will be prompted.

.PARAMETER NoIPv4
Do not include IPv4 addresses.

.PARAMETER NoIPv6
Do not include IPv6 addresses.

.PARAMETER Publish
If any changes made publish them automatically. By default session will just be closed pending you to manually open session in SmartConsole and publish the changes.
Publish will only happen if no errors during sync.

.PARAMETER IgnoreWarnings
When creating new Check Point objects pass the IgnoreWarnings switch. This is required if your Check Point database already contains duplicate addresses with different names.

.PARAMETER Rename
If existing object not found by name, first search by IP/Subnet and if matching object found rename it and add to group.

.PARAMETER Color
Check Point color to set on created objects.

.PARAMETER Prefix
Prefix used on host/network objects.

.PARAMETER GroupPrefix
Prefix used on group objects.

.PARAMETER CommentPrefix
Prefix used on comments (Groups, Session, Created Hosts & Networks).

.PARAMETER Tag
Tag set when creating objects.

.EXAMPLE
./Office365_Group_Sync.ps1 -NoIPv6 -Rename -Verbose

.NOTES
Requires psCheckPoint v0.7.9+.

.LINK
https://github.com/tkoopman/psCheckPoint

.LINK
https://support.content.office.net/en-us/static/O365IPAddresses.xml

#>
[CmdletBinding()]
param(
	[Parameter(Mandatory = $true)]
    [string]$ManagementServer,
    [int]$ManagementPort = 443,
	[Parameter(Mandatory = $true)]
	[PSCredential]$Credentials,
	[switch]$NoIPv4,
	[switch]$NoIPv6,
	[switch]$Publish,
	[switch]$IgnoreWarnings,
	[switch]$Rename,
	[string]$Color = "red",
	[string]$HostPrefix = "Microsoft",
	[string]$GroupPrefix = "Microsoft_Office365",
	[string]$CommentPrefix = "Microsoft Office365",
	[string]$Tag = "Microsoft_Office365"
)

# Download Microsoft Cloud IP Ranges and Names into Object
$O365IPAddresses = "https://support.content.office.net/en-us/static/O365IPAddresses.xml";
[XML]$O365 = Invoke-WebRequest -Uri $O365IPAddresses -DisableKeepAlive;

# Set variables
$Updated = ([datetime]::parseexact($O365.products.updated,"M/d/yyyy",[System.Globalization.CultureInfo]::InvariantCulture)).ToShortDateString();
$Comments = "$CommentPrefix added $Updated";
$GroupComments = "$CommentPrefix updated $Updated";
$Errors = 0;

# Login to Check Point API to get Session ID
Write-Verbose " *** Log in to Check Point Smart Center API *** ";
$Session = Open-CheckPointSession -SessionName $CommentPrefix -SessionComments "$CommentPrefix Group Sync" -ManagementServer $ManagementServer -ManagementPort $ManagementPort -Credentials $Credentials -NoCertificateValidation -PassThru;
if (-not $Session) {
	# Failed login
	exit;
}

ForEach ($Product in $O365.products.product) {
	$GroupName = $GroupPrefix + "_" + $Product.Name;
	Write-Verbose "Processing $GroupName";

	$Product.addresslist | Where-Object { ($_.type -eq "IPv4" -and -not $NoIPv4.IsPresent) -or ($_.type -eq "IPv6" -and -not $NoIPv6.IsPresent) } |
		Select-Object -ExpandProperty address -ErrorAction SilentlyContinue | New-SyncMember -Prefix "${HostPrefix}_" |
		Invoke-CheckPointGroupSync -Session $Session -Name $GroupName -Rename:$Rename.IsPresent -IgnoreWarnings:$IgnoreWarnings.IsPresent -Color $Color -Comments $Comments -Tags $Tag -CreateGroup |
		Tee-Object -Variable output;
	if (($output | Where-Object {$_.Actions -ne 0 -and -not $_.Error} | Measure-Object).Count -ne 0) {
		# Updates made
		Write-Verbose "Updating $GroupName group's comment";
		$Group = Set-CheckPointGroup -Session $Session -Name $GroupName -Comments "$GroupComments" -Verbose:$false -PassThru;
	}
	$Errors = $Errors + ($output | Where-Object {$_.Error} | Measure-Object).Count;
}

$Stats = Get-CheckPointSession -Session $Session -UID $Session.UID
Write-Verbose "Total Errors: $Errors";
if ($Stats.Changes -eq 0) {
	Write-Host "No changes made. Closing session.";
	Reset-CheckPointSession -Session $Session -Verbose:$false;
	Close-CheckPointSession -Session $Session -Verbose:$false;
} elseif ($Publish.IsPresent -and $Errors -eq 0) {
	# Publish Changes
	Write-Host "Publishing $($Stats.Changes) changes.";
	Publish-CheckPointSession -Session $Session -Verbose:$false;
	Close-CheckPointSession -Session $Session -Verbose:$false;
} else {
	# Logout from Check Point API
	Write-Host "View $($Stats.Changes) changes in SmartConsole to publish.";
	Close-CheckPointSession -Session $Session -ContinueSessionInSmartconsole -Verbose:$false;
}
# DONE!