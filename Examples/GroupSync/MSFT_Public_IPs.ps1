<#

.SYNOPSIS
One way sync of Microsoft Public networks into Check Point group.

.DESCRIPTION
This script will create/update a Check Point groups of Microsoft Public IPs, with the list of networks Microsoft publish.

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

.PARAMETER GroupName
Name of group to sync with.

.PARAMETER CommentPrefix
Prefix used on comments (Groups, Session, Created Hosts & Networks).

.PARAMETER Tag
Tag set when creating objects.

.PARAMETER CertificateValidation
Which certificate validation method(s) to use.

.EXAMPLE
./MSFT_Public_IPs.ps1 -Rename -Verbose

.NOTES
Requires psCheckPoint v1.0.0+.

.LINK
https://github.com/tkoopman/psCheckPoint

.LINK
https://www.microsoft.com/en-au/download/details.aspx?id=41653

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
	[string]$GroupName = "Microsoft_Public_IP_Space",
	[string]$CommentPrefix = "Microsoft Public IP Space",
	[string]$Tag = "Microsoft_Public_IP_Space",
	[ValidateSet("All", "Auto", "CertificatePinning", "None", "ValidCertificate")]
	[string]$CertificateValidation = "Auto"
)
# Download IPs
$downloadUri = "https://www.microsoft.com/en-us/download/confirmation.aspx?id=53602";
$downloadPage = Invoke-WebRequest -Uri $downloadUri;
$xmlFileUri = ($downloadPage.RawContent.Split('"') -like "https://*msft-public-ips.csv")[0];
$response = Invoke-WebRequest -Uri $xmlFileUri;
$csvResponse = [System.Text.Encoding]::UTF8.GetString($response.Content) | ConvertFrom-Csv;

# Set variables
$Updated = [datetime]::Now;
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

	Write-Verbose "Processing $GroupName";

	$IPs = $csvResponse.Prefix
	if ($NoIPv4.IsPresent) {
		$IPs = $IPs | Where-Object { $_ -notmatch "\." }
	}
	if ($NoIPv6.IsPresent) {
		$IPs = $IPs | Where-Object { $_ -notmatch ":" }
	}
	
	$IPs |
		Invoke-CheckPointGroupSync -Session $Session -GroupName $GroupName -Prefix "${HostPrefix}_" -Rename:$Rename.IsPresent -Ignore $Ignore -Color $Color -Comments $Comments -Tags $Tag -CreateGroup |
		Tee-Object -Variable output;
	if (($output | Where-Object {$_.Actions -ne 0 -and -not $_.Error} | Measure-Object).Count -ne 0) {
		# Updates made
		Write-Verbose "Updating $GroupName group's comment";
		$Group = Set-CheckPointGroup -Session $Session -Name $GroupName -Comments "$GroupComments" -Verbose:$false -PassThru;
	}
	$Errors = $Errors + ($output | Where-Object {$_.Error} | Measure-Object).Count;

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