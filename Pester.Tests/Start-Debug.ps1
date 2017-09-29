<#
This script will run on debug and start the pester tests.
It will load in a PowerShell command shell and run the tests developed in the project. To end debug, exit this shell.
#>

# Write a reminder on how to end testing and debugging.
$message = "| Exit this shell to end the test and debug session! |"
$line = "-" * $message.Length
$color = "Cyan"
Write-Host -ForegroundColor $color $line
Write-Host -ForegroundColor $color $message
Write-Host -ForegroundColor $color $line
Write-Host

# Happy testing and debugging :-)

..\..\Invoke-Tests.ps1
