Describe "Basic.Application" {
	Context "New-CheckPointApplication" {
		It "Add" {
			New-CheckPointApplication -Session $Session -Name PesterApplication -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com -PassThru  | Should BeOfType Koopman.CheckPoint.ApplicationSite
		}

		It "Add duplicate" {
			{ New-CheckPointApplication -Session $Session -Name PesterApplication -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointApplication" {
		It "Get" {
			Get-CheckPointApplication -Session $Session -Name PesterApplication | Should BeOfType Koopman.CheckPoint.ApplicationSite
		}

		It "Get non-existing" {
			{ Get-CheckPointApplication -Session $Session -Name NotPesterApplication -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointApplication" {
		It "Set" {
			Set-CheckPointApplication -Session $Session -Name PesterApplication -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.ApplicationSite
		}

		It "Set non-existing" {
			{ Set-CheckPointApplication -Session $Session -Name NotPesterApplication -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointApplications" {
		It "Get" {
			$(Get-CheckPointApplications -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.ApplicationSite
		}

		It "Get full object" {
			$(Get-CheckPointApplications -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.ApplicationSite
		}
	}

	Context "Remove-CheckPointApplication" {
		It "Remove" {
			{ Remove-CheckPointApplication -Session $Session -Name PesterApplication -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointApplication -Session $Session -Name PesterApplication -ErrorAction Stop } | Should Throw "not found"
		}
	}
}