<#

.SYNOPSIS
One way sync of AWS networks into Check Point groups.

.DESCRIPTION
This script will create/update Check Point groups for each AWS Service & Region, with the list of networks AWS publish.

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
./AWS_Group_Sync.ps1 -NoIPv6 -Rename -Verbose

.NOTES
Requires psCheckPoint v0.7.9+.
Requires AWSPowerShell.

.LINK
https://github.com/tkoopman/psCheckPoint

.LINK
https://aws.amazon.com/documentation/powershell/

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
	[string]$Prefix = "AWS",
	[string]$GroupPrefix = "AWS",
	[string]$CommentPrefix = "AWS",
	[string]$Tag = "AWS"
)

$AWSPIAR = Get-AWSPublicIpAddressRange | Where-Object { ($_.IpAddressFormat -eq "Ipv4" -and -not $NoIPv4.IsPresent) -or ($_.IpAddressFormat -eq "Ipv6" -and -not $NoIPv6.IsPresent) };
$Services = $AWSPIAR | Select-Object -ExpandProperty Service -Unique | Sort-Object;

# Set variables
$Updated = (Get-AWSPublicIpAddressRange -OutputPublicationDate).ToShortDateString();
$Comments = "Amazon AWS added $Updated";
$GroupComments = "updated $Updated";
$Errors = 0;

# Login to Check Point API to get Session ID
Write-Verbose " *** Log in to Check Point Smart Center API *** ";
$Session = Open-CheckPointSession -SessionName $CommentPrefix -SessionComments "$CommentPrefix Group Sync" -ManagementServer $ManagementServer -ManagementPort $ManagementPort -Credentials $Credentials -NoCertificateValidation -PassThru;
if (-not $Session) {
	# Failed login
	exit;
}

ForEach($Service in $Services) {
	$Regions = $AWSPIAR | Where-Object {$_.Service -eq $Service} | Select-Object -ExpandProperty Region -Unique | Sort-Object;
	ForEach($Region in $Regions) {
		if ($Service -eq "AMAZON") {
			$GroupName = $GroupPrefix + "_" + $Region;
		} else {
			$GroupName = $GroupPrefix + "_" + $Service + "_" + $Region;
		}

		Write-Verbose "Processing $GroupName";

		$AWSPIAR | Where-Object {$_.Service -eq $Service -and $_.Region -eq $Region} |
			Select-Object -ExpandProperty IpPrefix | New-SyncMember -Prefix "${Prefix}_" |
			Invoke-CheckPointGroupSync -Session $Session -Name $GroupName -Rename:$Rename.IsPresent -IgnoreWarnings:$IgnoreWarnings.IsPresent -Color $Color -Comments $Comments -Tags $Tag -CreateGroup |
			Tee-Object -Variable output;

		if (($output | Where-Object {$_.Actions -ne 0 -and -not $_.Error} | Measure-Object).Count -ne 0) {
			# Updates made
			Write-Verbose "Updating $GroupName group's comment";
			$Desc = (Get-AWSRegion -SystemName $Region).Name;
			$Group = Set-CheckPointGroup -Session $Session -Name $GroupName -Comments "$CommentPrefix $Desc $GroupComments" -Verbose:$false -PassThru;
		}
		$Errors = $Errors + ($output | Where-Object {$_.Error} | Measure-Object).Count;
	}
}

$Stats = Get-CheckPointSession -Session $Session -UID $Session.UID;
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