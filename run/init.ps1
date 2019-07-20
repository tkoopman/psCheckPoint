Set-Variable -Scope Global -Name SolutionDir -Option readonly -Value ($env:APPVEYOR_BUILD_FOLDER).TrimEnd('\');
Set-Variable -Scope Global -Name ConfigurationName -Option readonly -Value $env:Configuration;
Set-Variable -Scope Global -Name PackagesDir -Option readonly -Value "$($env:USERPROFILE)\.nuget\packages";

# psCheckPoint Test Variables
Set-Variable -Scope Global -Name Settings -Description "psCheckPoint Pester Settings" -Option readonly -Value $(Get-Content -ErrorAction Stop "$env:APPVEYOR_BUILD_FOLDER\run\Settings.json" | ConvertFrom-Json);

Write-Host -ForegroundColor Yellow "Import-Module $env:APPVEYOR_BUILD_FOLDER\psCheckPoint\bin\$ConfigurationName\psCheckPoint.psd1 -Force";
Import-Module $env:APPVEYOR_BUILD_FOLDER\psCheckPoint\bin\$ConfigurationName\psCheckPoint.psd1 -Force;