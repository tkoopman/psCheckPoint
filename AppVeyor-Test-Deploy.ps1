# Bump Version
	Try {
		$InfoVer = $env:APPVEYOR_REPO_TAG_NAME.TrimStart("v");
		$Ver = $InfoVer.Split("-");

		$ModFileName = "$env:APPVEYOR_BUILD_FOLDER\psCheckPoint\bin\Release\psCheckPoint.psd1"
        $ModManifest = Get-Content -Path $ModFileName
        $BumpedManifest = $ModManifest -replace "'0.0.0'", "'$Ver[0]'"

		$Pre = '';
		if ($Ver[1]) { $Pre = "-$($Ver[1])" }
		$BumpedManifest = $BumpedManifest -replace "'-pre'", "'$Pre'"

        Remove-Item -Path $ModFileName
        Out-File -FilePath $ModFileName -InputObject $BumpedManifest -NoClobber -Encoding utf8 -Force
    }
    Catch {
        $MsgParams = @{
            Message = 'Could not bump current version into module manifest.'
            Category = 'Error'
            Details = $_.Exception.Message
        }
        Add-AppveyorMessage @MsgParams
        Throw $MsgParams.Message
    }

# Run Tests
    cd "$env:APPVEYOR_BUILD_FOLDER\Pester.Tests\bin\Release\"
    ..\..\Invoke-Tests.ps1

# Zip Release
	Add-Type -assembly "system.io.compression.filesystem"

	$source = "$env:APPVEYOR_BUILD_FOLDER\psCheckPoint\bin\Release\"
	$destination = "$env:APPVEYOR_BUILD_FOLDER\psCheckPoint.zip"
	If(Test-path $destination) {Remove-item $destination}
	[io.compression.zipfile]::CreateFromDirectory($Source, $destination)

# Deploy
    Expand-Archive -Path "$env:APPVEYOR_BUILD_FOLDER\psCheckPoint.zip" -DestinationPath 'C:\Users\appveyor\Documents\WindowsPowerShell\Modules\psCheckPoint\' -Verbose
    Import-Module -Name 'psCheckPoint' -Verbose -Force
    Write-Host "Available Package Provider:" -ForegroundColor Yellow
    Get-PackageProvider -ListAvailable
    Write-Host "Available Package Sources:" -ForegroundColor Yellow
    Get-PackageSource
    Try {
        Write-Host "Try to get NuGet Provider:" -ForegroundColor Yellow
        Get-PackageProvider -Name NuGet -ErrorAction Stop
    }
    Catch {
        Write-Host "Installing NuGet..." -ForegroundColor Yellow
        Install-PackageProvider -Name NuGet -MinimumVersion '2.8.5.201' -Force -Verbose
        Import-PackageProvider NuGet -MinimumVersion '2.8.5.201' -Force
    }
    Try {
        Write-Host "try to publish module" -ForegroundColor Yellow
        Publish-Module -Name 'psCheckPoint' -NuGetApiKey $env:NuGetToken -Verbose -Force
    }
    Catch {
        $MsgParams = @{
            Message = 'Could not deploy module to PSGallery.'
            Category = 'Error'
            Details = $_.Exception.Message
        }
        Add-AppveyorMessage @MsgParams
        Throw $MsgParams.Message
    }