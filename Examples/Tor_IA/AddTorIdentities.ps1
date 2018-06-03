#
# AddTorIdentities.ps1
#
[CmdletBinding()]
param(
	[Parameter(Mandatory = $true)]
    [string]$Gateway,
	[Parameter(Mandatory = $true)]
	[string]$SharedSecret,
	[string]$CertificateHash,
	[ValidateSet("All", "Auto", "CertificatePinning", "None", "ValidCertificate")]
	[string]$CertificateValidation = "Auto",
	[switch]$ExitNodesOnly
)

# Full list of Tor Nodes
$URL="https://www.dan.me.uk/torlist/";
if ($ExitNodesOnly.IsPresent) {
	$URL="$($URL)?exit";
}

$Cache="TorNodesCache.csv";

$Download=$false
if (Test-Path $Cache) {
	$lastWrite = (get-item $Cache).LastWriteTime;
	$timespan = new-timespan -minutes 30;

	if (((get-date) - $lastWrite) -gt $timespan) {
		$Download=$true;
		Write-Warning "Tor nodes cache older than 30 minutes. Downloading new copy from $($URL)";
	}
} else {
	$Download=$true;
	Write-Warning "Tor nodes cache missing. Downloading new copy from $($URL)";
}

if ($Download) {
	[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12;
	Invoke-WebRequest -Uri $URL -OutFile $Cache;
}

if (Test-Path $Cache) {
	$ips = Import-Csv -Path $Cache -Header "IPAddress";
	Write-Output "Associating $($ips.Length) IPs with PDP...";
	$sw = [Diagnostics.Stopwatch]::StartNew();
	$output = $ips | Add-CheckPointIdentity -Gateway $Gateway -SharedSecret $SharedSecret -CertificateHash $CertificateHash -CertificateValidation $CertificateValidation -SessionTimeout 7200 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -Roles 'Tor' -HostType 'Tor Node' -Machine {"Tor_$($_.IPAddress)"} -BatchSize 100;
	$sw.Stop();
	Write-Output "Associated $($ips.Length) IPs with PDP in $($sw.Elapsed.TotalSeconds) seconds";
} else {
	Write-Error "Failed to download list.";
}