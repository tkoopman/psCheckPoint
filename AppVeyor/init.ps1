cd $env:Temp
iex ((New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/appveyor/secure-file/master/install.ps1'))

$InfoVer = $env:APPVEYOR_REPO_TAG_NAME.TrimStart("v");
$Ver = $InfoVer.Split("-")[0];
Set-AppveyorBuildVariable -Name "InfomationalVersion" -Value $InfoVer
Set-AppveyorBuildVariable -Name "FileVersion" -Value $Ver
Update-AppveyorBuild -Version $InfoVer

# Open VPN Connection to Test Environment
(New-Object Net.WebClient).DownloadFile("https://swupdate.openvpn.org/community/releases/openvpn-install-2.4.7-I603.exe", "openvpn-install-2.4.7-I603.exe")

# Trust OpenVPN Cert so TAP Driver will silently install
certutil -addstore -f "TrustedPublisher" "$env:APPVEYOR_BUILD_FOLDER\AppVeyor\OpenVPN_Install.cer"
# Install OpenVPN
Start-Process -Wait -FilePath .\openvpn-install-2.4.7-I603.exe -ArgumentList "/S /D=C:\OpenVPN"

# Add config
.\appveyor-tools\secure-file -decrypt "$env:APPVEYOR_BUILD_FOLDER\AppVeyor\AppVeyor.ovpn.enc" -secret "$env:TEST_SECRET" -out C:\OpenVPN\config\AppVeyor.ovpn
.\appveyor-tools\secure-file -decrypt "$env:APPVEYOR_BUILD_FOLDER\AppVeyor\AppVeyor.auth.enc" -secret "$env:TEST_SECRET" -out C:\OpenVPN\config\AppVeyor.auth
.\appveyor-tools\secure-file -decrypt "$env:APPVEYOR_BUILD_FOLDER\AppVeyor\Settings.json.enc" -secret "$env:TEST_SECRET" -out "$env:APPVEYOR_BUILD_FOLDER\run\Settings.json"

$Settings = Get-Content $env:APPVEYOR_BUILD_FOLDER\run\Settings.json | ConvertFrom-Json
$TestIP = $Settings.($Settings.Versions[0]).Management.Server

# Start VPN
$p = Start-Process -FilePath C:\OpenVPN\bin\openvpn.exe -ArgumentList "--config AppVeyor.ovpn --log AppVeyor.log --connect-retry-max 5" -WorkingDirectory C:\OpenVPN\config\ -PassThru
# Wait for it to connect
do { $ping = Test-Connection -ComputerName $TestIP -Count 1 -Quiet -TimeToLive 2 }until($ping -or $p.HasExited)
if (-not $ping) { throw "Unable to establish VPN" }