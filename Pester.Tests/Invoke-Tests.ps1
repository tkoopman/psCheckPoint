<#
This script will run on debug to execute the Pester tests.
#>
. $env:APPVEYOR_BUILD_FOLDER\run\init.ps1;

# Load Pester.
$xml = [xml](Get-Content $SolutionDir\Pester.Tests\Pester.Tests.csproj);
$PesterVer = $($xml.Project.ItemGroup.PackageReference| where {$_.Include -eq "Pester"}).Version;
$PesterDir = "$PackagesDir\pester\$PesterVer\tools";
Write-Host -ForegroundColor Yellow "Import-Module $PesterDir\Pester.psd1";
Import-Module $PesterDir\Pester.psd1;

cd $SolutionDir\Pester.Tests\;
foreach ($v in @( "R80.20", "R80.10" )) {
	Set-Variable -Scope Global -Name CPVersion -Description "psCheckPoint Test Run Version" -Option readonly -Value $v;

	$secpasswd = ConvertTo-SecureString $Settings.($CPVersion).Management.Password -AsPlainText -Force;
	Set-Variable -Scope Global -Name CPLogin -Description "psCheckPoint Login Credentials" -Option readonly -Value $(New-Object System.Management.Automation.PSCredential ($Settings.($CPVersion).Management.User, $secpasswd));

	Set-Variable -Scope Global -Name Session -Description "psCheckPoint Pester Session" -Option readonly -Value $(Open-CheckPointSession -ManagementServer $Settings.($CPVersion).Management.Server -Credentials $CPLogin -CertificateHash $Settings.($CPVersion).Management.Hash -SessionName Pester -SessionDescription "psCheckPoint Pester Testing" -PassThru);

	# Run the tests. By default all tests matching *.Tests.ps1 will be executed.
	# See https://github.com/pester/Pester for more information.
	if ($env:APPVEYOR) {
		$res = Invoke-Pester -OutputFormat NUnitXml -OutputFile ..\..\TestsResults.xml -PassThru;
		(New-Object 'System.Net.WebClient').UploadFile("https://ci.appveyor.com/api/testresults/nunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path ..\..\TestsResults.xml));
	} else {
		Invoke-Pester;
	}

	# Close Testing Session
	Reset-CheckPointSession -Session $Session;
	Close-CheckPointSession -Session $Session;

	Remove-Variable -Scope Global -Name Session -Force;
	Remove-Variable -Scope Global -Name CPLogin -Force;
	Remove-Variable -Scope Global -Name CPVersion -Force;

	if ($env:APPVEYOR) {
		if ($res.FailedCount -gt 0) { throw "$($res.FailedCount) tests failed." }
	}
}