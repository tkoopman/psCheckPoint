# Example of importing list of known Tor IPs and adding them to a Role called "Tor_IPs"
# "Tor_IPs" Role must already exist.
# You can then use that role in a rule to block traffic to those IPs.
# I Do not own or manage the URL used by this script.
# See details of list @ https://www.dan.me.uk/tornodes
# Site only allows you to download the list once every 30 minutes

Write-Host "Allow self-signed certs"
[System.Net.ServicePointManager]::ServerCertificateValidationCallback = { $True }

Write-Host "Importing Check Point Identity Awareness Module"
import-module ..\IdentityAwareness.psm1

$(Invoke-WebRequest https://www.dan.me.uk/torlist/).content | 
    ConvertFrom-Csv -Header "IPAddress" | 
	AddIdentity -SessionTimeout 7200 -FetchUserGroups:$False -FetchMachineGroups:$FALSE -CalculateRoles:$FALSE -Roles 'Tor_IPs' -HostType 'Tor Node' -Machine {"Tor_$($_.IPAddress)"} | 
	Format-Table -Wrap

Remove-Module IdentityAwareness
