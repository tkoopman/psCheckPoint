Describe "Basic.Application" {
	Context "New-CheckPointApplication" {
		It "Add" -Skip {
			New-CheckPointApplication -Session $Session -Name PesterApplication -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com -PassThru  | Should BeOfType Koopman.CheckPoint.Application
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointApplication -Session $Session -Name PesterApplication -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointApplication" {
		It "Get" -Skip {
			Get-CheckPointApplication -Session $Session -Name PesterApplication | Should BeOfType Koopman.CheckPoint.Application
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointApplication -Session $Session -Name NotPesterApplication -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointApplication" {
		It "Set" -Skip {
			Set-CheckPointApplication -Session $Session -Name PesterApplication -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.Application
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointApplication -Session $Session -Name NotPesterApplication -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointApplications" {
		It "Get" -Skip {
			$(Get-CheckPointApplications -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.Application
		}

		It "Get full object" -Skip {
			$(Get-CheckPointApplications -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.Application
		}
	}

	Context "Remove-CheckPointApplication" {
		It "Remove" -Skip {
			{ Remove-CheckPointApplication -Session $Session -Name PesterApplication -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointApplication -Session $Session -Name PesterApplication -ErrorAction Stop } | Should Throw "not found"
		}
	}
}