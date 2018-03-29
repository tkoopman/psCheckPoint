param(
	[Parameter()] $TargetDir,
	[Parameter()] $SolutionDir,
	[Parameter()] $ConfigurationName
)

cd $TargetDir

#Copy psCheckPoint module
Copy-Item -Force -Recurse $SolutionDir\psCheckPoint\bin\$ConfigurationName\* .\

# Uncomment the following line to disable running the tests after each build.
#..\..\Invoke-Tests.ps1