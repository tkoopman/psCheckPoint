. $env:APPVEYOR_BUILD_FOLDER\run\init.ps1;

Write-Host -ForegroundColor Yellow 'Open-CheckPointSession -NoCertificateValidation -ManagementServer $Settings.Management.Server -Credentials $CPLogin -SessionName Testing -SessionDescription "psCheckPoint Testing"';
Open-CheckPointSession -NoCertificateValidation -ManagementServer $Settings.Management.Server -Credentials $CPLogin -SessionName Testing -SessionDescription "psCheckPoint Testing" -ErrorAction Stop;

Write-Host "";
Write-Host -ForegroundColor Cyan "Testing session established. Don't forget to close it off before exiting this window by running:";
Write-Host -ForegroundColor Cyan "Reset-CheckPointSession;Close-CheckPointSession";
