# Used to load modules for debugging
if($PSEdition -eq 'Core') {
	Import-Module $env:USERPROFILE\.nuget\packages\checkpoint.net\0.1.6\lib\netstandard1.6\CheckPoint.NET.dll -Force
}
Import-Module .\psCheckPoint.psd1 -Force