<#

.SYNOPSIS
One way sync of Microsoft Azure networks into Check Point groups.

.DESCRIPTION
This script will create/update Check Point groups for each Microsoft Azure Region, with the list of networks Microsoft publish.
By default this will create over 3000 objects first time it is run, which you may have problems publishing.
Use -RegionsLike to create initial groups in batches. Once created should be no problems syncing them all at once.

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

.PARAMETER RegionsMatch
Only process regions which match this string. Uses -match

.PARAMETER PrintRegions
Will output list of regions only.

.PARAMETER CertificateValidation
Which certificate validation method(s) to use.

.PARAMETER Domain
Specifies the Check Point MDS Domain to connect to.

.EXAMPLE
./Azure_Group_Sync.ps1 -Rename -Verbose

.NOTES
Requires psCheckPoint v1.0.0+.
Microsoft's list only includes IPv4 networks.

.LINK
https://github.com/tkoopman/psCheckPoint

.LINK
https://www.microsoft.com/en-au/download/details.aspx?id=56519

#>
[CmdletBinding(DefaultParameterSetName='Standard')]
param(
	[Parameter(Mandatory = $true, ParameterSetName='Standard')]
    [string]$ManagementServer,
	[Parameter(Mandatory = $true, ParameterSetName='Standard')]
	[PSCredential]$Credentials,
	[Parameter(ParameterSetName='Standard')]
	[string]$CertificateHash,
	[Parameter(ParameterSetName='Standard')]
    [int]$ManagementPort = 443,
	[Parameter(ParameterSetName='Standard')]
	[switch]$NoIPv4,
	[Parameter(ParameterSetName='Standard')]
	[switch]$NoIPv6,
	[Parameter(ParameterSetName='Standard')]
	[switch]$Publish,
	[Parameter(ParameterSetName='Standard')]
	[ValidateSet("No", "Warnings", "Errors")]
	[string]$Ignore = "No",
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
	[string]$RegionsMatch = "",
	[Parameter(Mandatory = $true, ParameterSetName='Print Regions')]
	[switch]$PrintRegions,
	[Parameter(ParameterSetName='Standard')]
	[ValidateSet("All", "Auto", "CertificatePinning", "None", "ValidCertificate")]
	[string]$CertificateValidation = "Auto",
	[Parameter(ParameterSetName='Standard')]
	[string]$Domain = ""
)
# Download code from https://gallery.technet.microsoft.com/scriptcenter/Adds-Azure-Datacenter-IP-dbeebe0c
# Download Microsoft Azure IP Ranges and Names into Object
$downloadUri = "https://www.microsoft.com/en-in/download/confirmation.aspx?id=56519";
$downloadPage = Invoke-WebRequest -Uri $downloadUri;
$jsonFileUri = ($downloadPage.RawContent.Split('"') -like "https://*.json*")[0];
$responseDate = Invoke-WebRequest -Uri $jsonFileUri;
$response = Invoke-RestMethod -Uri $jsonFileUri;
if ($PrintRegions.IsPresent) {
    $response.values.name | Where-Object {$_ -match $RegionsMatch} | Sort-Object

	exit;
}

# Set variables
$Updated = [datetime]::parseexact($responseDate.Headers.'Last-Modified',"r", [System.Globalization.CultureInfo]::InvariantCulture).ToShortDateString();
$Comments = "$CommentPrefix added $Updated";
$GroupComments = "$CommentPrefix updated $Updated";
$Errors = 0;

# Login to Check Point API to get Session ID
Write-Verbose " *** Log in to Check Point Smart Center API *** ";
$Session = Open-CheckPointSession -SessionName "$CommentPrefix" -SessionComments "$CommentPrefix Group Sync" -ManagementServer $ManagementServer -ManagementPort $ManagementPort -Domain $Domain -Credentials $Credentials -CertificateValidation $CertificateValidation -CertificateHash $CertificateHash -PassThru;
if (-not $Session) {
	# Failed login
	exit;
}

ForEach($region in $response.values) {
	if ($region.name -notmatch $RegionsMatch) {
		Continue;
	}

	$GroupName = $GroupPrefix + "_" + $region.name;
	Write-Verbose "Processing $GroupName";

	$IPs = $region.properties.addressPrefixes
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
