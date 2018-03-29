Set-Variable -Scope Global -Name SolutionDir -Option readonly -Value ($env:APPVEYOR_BUILD_FOLDER).TrimEnd('\');
Set-Variable -Scope Global -Name ConfigurationName -Option readonly -Value $env:Configuration;
Set-Variable -Scope Global -Name PackagesDir -Option readonly -Value "$($env:USERPROFILE)\.nuget\packages";

# psCheckPoint Test Variables
if ($env:Settings_json) {
	Set-Variable -Scope Global -Name Settings -Description "psCheckPoint Pester Settings" -Option readonly -Value $($env:Settings_json | ConvertFrom-Json);
} else {
	Set-Variable -Scope Global -Name Settings -Description "psCheckPoint Pester Settings" -Option readonly -Value $(Get-Content -ErrorAction Stop "$SolutionDir\run\Settings.json" | ConvertFrom-Json);
}

$secpasswd = ConvertTo-SecureString $Settings.Management.Password -AsPlainText -Force;
Set-Variable -Scope Global -Name CPLogin -Description "psCheckPoint Login Credentials" -Option readonly -Value $(New-Object System.Management.Automation.PSCredential ($Settings.Management.User, $secpasswd));

if($PSEdition -eq 'Core') {
	$xml = [xml](Get-Content $SolutionDir\psCheckPoint\psCheckPoint.csproj);
	$CheckPointNETVer = $($xml.Project.ItemGroup.PackageReference| where {$_.Include -eq "CheckPoint.NET"}).Version;
	$CheckPointNETDir = "$PackagesDir\checkpoint.net\$CheckPointNETVer\lib\";
	
	Write-Host -ForegroundColor Yellow "Import-Module $CheckPointNETDir\netstandard1.6\CheckPoint.NET.dll -Force";
	Import-Module $CheckPointNETDir\netstandard1.6\CheckPoint.NET.dll -Force;
}

Write-Host -ForegroundColor Yellow "Import-Module $SolutionDir\psCheckPoint\bin\$ConfigurationName\psCheckPoint.psd1 -Force";
Import-Module $SolutionDir\psCheckPoint\bin\$ConfigurationName\psCheckPoint.psd1 -Force;