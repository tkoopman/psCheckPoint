Write-Host "Allow self-signed certs"
[System.Net.ServicePointManager]::ServerCertificateValidationCallback = { $True }

Write-Host "Importing Module"
import-module ..\CheckPoint.psm1

Write-Host "Logging in. You will be prompted for creds"
$Session = Invoke-CPLogin

Write-Host "Create Group"
Add-CPGroup -Session $Session -Name TestGroup1
Write-Host "Create Networks"
Add-CPNetwork -Session $Session -Name TestNetwork1 -Subnet 10.0.0.0 -MaskLength 8 -Groups TestGroup1 -Colour Green
Add-CPNetwork -Session $Session -Name TestNetwork2 -Subnet 192.168.0.0 -MaskLength 16 -Groups TestGroup1 -Tags TestTag2,TestTag4 -Colour Green

Write-Host "Import Hosts from CSV. Last host will fail due to conflicts."
Import-Csv .\AddHosts.csv | Add-CPHost -Session $Session

Write-Host "Remove Network - Will Fail due to being used in group"
Remove-CPNetwork -Session $Session -Name TestNetwork2
Write-Host "Trying again with ignore warnings"
Remove-CPNetwork -Session $Session -Name TestNetwork2 -IgnoreWarnings

Write-Host "Logout and allow session to continue from SmartConsole."
Write-Host "Discard changes in SmartConsole if you want to run this again."
Invoke-CPContinueSessionInSmartconsole $Session


Remove-Module CheckPoint