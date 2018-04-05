param(
	[Parameter()] $TargetDir,
	[Parameter()] $SolutionDir
)

$PackagesDir = "$($env:USERPROFILE)\.nuget\packages";

$xml = [xml](Get-Content -ErrorAction Stop $SolutionDir\psCheckPoint\psCheckPoint.csproj);
$CheckPointNETVer = $($xml.Project.ItemGroup.PackageReference| where {$_.Include -eq "CheckPoint.NET"}).Version;
$CheckPointNETDir = "$PackagesDir\checkpoint.net\$CheckPointNETVer\lib";

Write-Host -ForegroundColor Yellow "Copy-Item -Force $CheckPointNETDir\netstandard1.6\CheckPoint.NET.dll $TargetDir";
Copy-Item -ErrorAction Stop -Force $CheckPointNETDir\netstandard1.6\CheckPoint.NET.dll $TargetDir

Write-Host -ForegroundColor Yellow "Copy-Item -ErrorAction Stop -Force $TargetDir\..\net45\psCheckPoint.dll-Help.xml $TargetDir";
Copy-Item -ErrorAction Stop -Force $TargetDir\..\net45\psCheckPoint.dll-Help.xml $TargetDir