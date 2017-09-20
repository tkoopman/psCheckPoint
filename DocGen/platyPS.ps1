[CmdletBinding()]
param(
	[string]$path = '..\docs\md',
	[string]$maml = '..\psCheckPoint\bin\Release\psCheckPoint.dll-Help.xml'
)

New-Item -ItemType Directory -Force -Path $path

Import-Module ..\psCheckPoint\bin\Release\psCheckPoint.psd1
New-MarkdownHelp -Module psCheckPoint -OutputFolder $path -Force -WithModulePage -AlphabeticParamsOrder -NoMetadata
Remove-Module psCheckPoint