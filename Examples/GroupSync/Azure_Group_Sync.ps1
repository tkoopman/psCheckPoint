<#

.SYNOPSIS
One way sync of Microsoft Azure networks into Check Point groups.

.DESCRIPTION
This script will create/update Check Point groups for each Microsoft Azure Region, with the list of networks Microsoft publish.
By default this will create over 3000 objects first time it is run, which you may have problems publishing. 
Use -RegionsLike to create initial groups in batches. Once created should be no problems syncing them all at once.

.PARAMETER ManagementServer
IP or Hostname of the Check point Management Server

.PARAMETER ManagementPort
Port Web API running on.

.PARAMETER Credentials
PSCredential containing User name and Password. If not provided you will be prompted.

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

.PARAMETER RegionsMatch
Only process regions which match this string. Uses -match

.PARAMETER PrintRegions
Will output list of regions only.

.EXAMPLE
./Azure_Group_Sync.ps1 -Rename -Verbose

.NOTES
Requires psCheckPoint v0.7.9+.
Microsoft's list only includes IPv4 networks.

.LINK
https://github.com/tkoopman/psCheckPoint

.LINK
https://www.microsoft.com/en-au/download/details.aspx?id=41653

#>
[CmdletBinding(DefaultParameterSetName='Standard')]
param(
	[Parameter(Mandatory = $true, ParameterSetName='Standard')]
    [string]$ManagementServer,
	[Parameter(ParameterSetName='Standard')]
    [int]$ManagementPort = 443,
	[Parameter(Mandatory = $true, ParameterSetName='Standard')]
	[PSCredential]$Credentials,
	[Parameter(ParameterSetName='Standard')]
	[switch]$Publish,
	[Parameter(ParameterSetName='Standard')]
	[switch]$IgnoreWarnings,
	[Parameter(ParameterSetName='Standard')]
	[switch]$Rename,
	[Parameter(ParameterSetName='Standard')]
	[string]$Color = "red",
	[Parameter(ParameterSetName='Standard')]
	[string]$HostPrefix = "Microsoft",
	[Parameter(ParameterSetName='Standard')]
	[string]$GroupPrefix = "Microsoft_Azure",
	[Parameter(ParameterSetName='Standard')]
	[string]$CommentPrefix = "Microsoft Azure",
	[Parameter(ParameterSetName='Standard')]
	[string]$Tag = "Microsoft_Azure",
	[Parameter(ParameterSetName='Standard')]
	[string]$RegionsMatch = "",
	[Parameter(Mandatory = $true, ParameterSetName='Print Regions')]
	[switch]$PrintRegions
)

# Download code from https://gallery.technet.microsoft.com/scriptcenter/Adds-Azure-Datacenter-IP-dbeebe0c
# Download Microsoft Azure IP Ranges and Names into Object
$downloadUri = "https://www.microsoft.com/en-in/download/confirmation.aspx?id=41653";
$downloadPage = Invoke-WebRequest -Uri $downloadUri;
$xmlFileUri = ($downloadPage.RawContent.Split('"') -like "https://*PublicIps*")[0];
$response = Invoke-WebRequest -Uri $xmlFileUri;

# Get list of regions & public IP ranges
[xml]$xmlResponse = [System.Text.Encoding]::UTF8.GetString($response.Content);
$regions = $xmlResponse.AzurePublicIpAddresses.Region;

if ($PrintRegions.IsPresent) {
	$regions.Name | Sort-Object;
	exit;
}

# Set variables
$Updated = [datetime]::parseexact($response.Headers.'Last-Modified',"r", [System.Globalization.CultureInfo]::InvariantCulture).ToShortDateString();
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

ForEach($region in $regions) {
	if ($region.Name -notmatch $RegionsMatch) {
		Continue;
	}

	$GroupName = $GroupPrefix + "_" + $region.Name;
	Write-Verbose "Processing $GroupName";

	$region.IpRange.Subnet | New-SyncMember -Prefix "${HostPrefix}_" |
		Invoke-CheckPointGroupSync -Session $Session -Name $GroupName -Rename:$Rename.IsPresent -IgnoreWarnings:$IgnoreWarnings.IsPresent -Color $Color -Comments $Comments -Tags $Tag -CreateGroup |
		Tee-Object -Variable output;
	if (($output | Where-Object {$_.Actions -ne 0 -and -not $_.Error} | Measure-Object).Count -ne 0) {
		# Updates made
		Write-Verbose "Updating $GroupName group's comment";
		$Group = Set-CheckPointGroup -Session $Session -Name $GroupName -Comments "$GroupComments" -Verbose:$false -PassThru;
	}
	$Errors = $Errors + ($output | Where-Object {$_.Error} | Measure-Object).Count;
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