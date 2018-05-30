<#

.SYNOPSIS
One way sync of Microsoft Office365 hosts & networks into Check Point groups.

.DESCRIPTION
This script will create/update Check Point groups for each Microsoft Office365 product, with the list of hosts & networks Microsoft publish.

.PARAMETER ManagementServer
IP or Hostname of the Check point Management Server

.PARAMETER Credentials
PSCredential containing User name and Password. If not provided you will be prompted.

.PARAMETER CertificateHash
The server's SSL certificate hash

.PARAMETER ManagementPort
Port Web API running on.

.PARAMETER NoIPv4
Do not include IPv4 addresses.

.PARAMETER NoIPv6
Do not include IPv6 addresses.

.PARAMETER Publish
If any changes made publish them automatically. By default session will just be closed pending you to manually open session in SmartConsole and publish the changes.
Publish will only happen if no errors during sync.

.PARAMETER Ignore
Weather Check Point warnings or errors should be ignored.

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

.PARAMETER CertificateValidation
Which certificate validation method(s) to use.

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
	[Parameter(Mandatory = $true)]
	[PSCredential]$Credentials,
	[string]$CertificateHash,
    [int]$ManagementPort = 443,
	[switch]$NoIPv4,
	[switch]$NoIPv6,
	[switch]$Publish,
	[ValidateSet("No", "Warnings", "Errors")]
	[string]$Ignore = "No",
	[switch]$Rename,
	[string]$Color = "red",
	[string]$HostPrefix = "Microsoft",
	[string]$GroupPrefix = "Microsoft_Office365",
	[string]$CommentPrefix = "Microsoft Office365",
	[string]$Tag = "Microsoft_Office365",
	[ValidateSet("All", "Auto", "CertificatePinning", "None", "ValidCertificate")]
	[string]$CertificateValidation = "Auto"
)
# path where client ID will be stored
$datapath = $Env:TEMP + "\MS_O365_ClientRequestId.txt";
Write-Verbose "Client ID File: $datapath";

# fetch client ID if data file exists; otherwise create new file
if (Test-Path $datapath) {
    $content = Get-Content $datapath;
    $clientRequestId = $content;
}
else {
	Write-Verbose "Creating new Client ID";
    $clientRequestId = [GUID]::NewGuid().Guid;
    $clientRequestId | Out-File $datapath;
}

Write-Verbose "Client ID: $clientRequestId";

# Download Microsoft Cloud IP Ranges and Names into Object
$Version = Invoke-RestMethod https://endpoints.office.com/version/O365Worldwide?ClientRequestId=$clientRequestId;
Write-Verbose "Version: $($Version.latest)";
$O365IPAddresses = Invoke-RestMethod https://endpoints.office.com/endpoints/O365Worldwide?ClientRequestId=$clientRequestId;

# Set variables
$Updated = ([datetime]::parseexact($Version.latest.Substring(0, 8),"yyyyMMdd",[System.Globalization.CultureInfo]::InvariantCulture)).ToShortDateString();
$Comments = "$CommentPrefix added $Updated";
$GroupComments = "$CommentPrefix updated $Updated";
$Errors = 0;

# Login to Check Point API to get Session ID
Write-Verbose " *** Log in to Check Point Smart Center API *** ";
$Session = Open-CheckPointSession -SessionName $CommentPrefix -SessionComments "$CommentPrefix Group Sync" -ManagementServer $ManagementServer -ManagementPort $ManagementPort -Credentials $Credentials -CertificateValidation $CertificateValidation -CertificateHash $CertificateHash -PassThru;
if (-not $Session) {
	# Failed login
	exit;
}

$ServiceAreas = $O365IPAddresses | Select-Object -ExpandProperty serviceArea | Sort-Object -Unique

ForEach ($ServiceArea in $ServiceAreas) {
	$GroupName = $GroupPrefix + "_" + $ServiceArea;
	Write-Verbose "Processing $GroupName";

	$ServiceAreaIPs = $O365IPAddresses | Where-Object {$_.serviceArea -eq $ServiceArea -and $_.ips} | Select-Object -ExpandProperty ips;
	if ($NoIPv4.IsPresent) {
		$ServiceAreaIPs = $ServiceAreaIPs | Where-Object { $_ -notmatch "\." }
	}
	if ($NoIPv6.IsPresent) {
		$ServiceAreaIPs = $ServiceAreaIPs | Where-Object { $_ -notmatch ":" }
	}

	$ServiceAreaIPs |
		Invoke-CheckPointGroupSync -Session $Session -GroupName $GroupName -Prefix "${HostPrefix}_" -Rename:$Rename.IsPresent -Ignore $Ignore -Color $Color -Comments $Comments -Tags $Tag -CreateGroup |
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