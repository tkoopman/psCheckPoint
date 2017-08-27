#
# AddTorIdentities.ps1
#

# Full list of Tor Nodes
$URL="https://www.dan.me.uk/torlist/"
# Limit to just exit nodes - Uncomment if you wish
#$URL="$($URL)?exit"

$Cache="TorNodesCache.csv"

$Download=$false
if (Test-Path $Cache) {
	$lastWrite = (get-item $Cache).LastWriteTime
	$timespan = new-timespan -minutes 30

	if (((get-date) - $lastWrite) -gt $timespan) {
		$Download=$true
		Write-Warning "Tor nodes cache older than 30 minutes. Downloading new copy from $($URL)"
	}
} else {
	$Download=$true
	Write-Warning "Tor nodes cache missing. Downloading new copy from $($URL)"
}

if ($Download) {
	[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
	Invoke-WebRequest -Uri $URL -OutFile $Cache
}

if (Test-Path $Cache) {
	Import-Csv -Path $Cache -Header "IPAddress" |
		Add-CheckPointIdentity -SessionTimeout 7200 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -NoCertificateValidation -Roles 'Tor' -HostType 'Tor Node' -Machine {"Tor_$($_.IPAddress)"} -BatchSize 100 |
		Format-Table -Wrap
} else {
	Write-Error "Failed to download list."
}
