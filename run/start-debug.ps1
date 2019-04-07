. $env:APPVEYOR_BUILD_FOLDER\run\init.ps1;
Set-Variable -Scope Global -Name CPVersion -Description "psCheckPoint Test Run Version" -Option readonly -Value "R80.20";

$secpasswd = ConvertTo-SecureString $Settings.($CPVersion).Management.Password -AsPlainText -Force;
Set-Variable -Scope Global -Name CPLogin -Description "psCheckPoint Login Credentials" -Option readonly -Value $(New-Object System.Management.Automation.PSCredential ($Settings.($CPVersion).Management.User, $secpasswd));

Write-Host -ForegroundColor Yellow 'Open-CheckPointSession -ManagementServer $Settings.($CPVersion).Management.Server -Credentials $CPLogin -CertificateHash $Settings.Management.Hash -SessionName Testing -SessionDescription "psCheckPoint Testing"';
Open-CheckPointSession -ManagementServer $Settings.($CPVersion).Management.Server -Credentials $CPLogin -CertificateHash $Settings.($CPVersion).Management.Hash -SessionName Testing -SessionDescription "psCheckPoint Testing" -ErrorAction Stop;

Write-Host "";
Write-Host -ForegroundColor Cyan "Testing session established. Don't forget to close it off before exiting this window by running:";
Write-Host -ForegroundColor Cyan "Reset-CheckPointSession;Close-CheckPointSession";