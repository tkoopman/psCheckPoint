﻿<#
This script will run on debug to execute the Pester tests.
#>

# Make sure we can load the local modules.
$env:PSModulePath = (Resolve-Path .).Path + ";" + $env:PSModulePath

# Optionally; load the module under test:
Import-Module  .\psCheckPoint.psd1

# Load Pester. We assume it can be found in one of the module paths.
Import-Module Pester

# psCheckPoint Test Variables
Set-Variable -Scope Global -Name Settings -Description "psCheckPoint Pester Settings" -Option readonly -Value $(Get-Content "Pester.Settings.json" | ConvertFrom-Json)
$secpasswd = ConvertTo-SecureString $Settings.Management.Password -AsPlainText -Force
$mycreds = New-Object System.Management.Automation.PSCredential ($Settings.Management.User, $secpasswd)
Set-Variable -Scope Global -Name Session -Description "psCheckPoint Pester Session" -Option readonly -Value $(Open-CheckPointSession -NoCertificateValidation -ManagementServer $Settings.Management.Server -Credentials $mycreds -SessionName Pester -SessionDescription "psCheckPoint Pester Testing")

# Run the tests. By default all tests matching *.Tests.ps1 will be executed.
# See https://github.com/pester/Pester for more information.
Invoke-Pester

# Close Testing Session
Reset-CheckPointSession -Session $Session
Close-CheckPointSession -Session $Session

Remove-Variable -Scope Global -Name Settings -Force
Remove-Variable -Scope Global -Name Session -Force