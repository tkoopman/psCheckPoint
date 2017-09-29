Describe "Basic.Application" {
	Context "New-CheckPointApplication" {
		It "Add testing application" {
			New-CheckPointApplication -Session $Session -Name PesterApplication -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com  | Should BeOfType psCheckPoint.Objects.Application.CheckPointApplication
		}

		It "Add application again - duplicate name & ip" {
			New-CheckPointApplication -Session $Session -Name PesterApplication -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com   | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointApplication" {
		It "Get testing application" {
			Get-CheckPointApplication -Session $Session -Name PesterApplication | Should BeOfType psCheckPoint.Objects.Application.CheckPointApplication
		}

		It "Get non-existing application" {
			Get-CheckPointApplication -Session $Session -Name NotPesterApplication | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointApplication" {
		It "Set testing application" {
			Set-CheckPointApplication -Session $Session -Name PesterApplication -Color Red | Should BeOfType psCheckPoint.Objects.Application.CheckPointApplication
		}

		It "Set non-existing application" {
			Set-CheckPointApplication -Session $Session -Name NotPesterApplication -Color Red | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointApplications" {
		It "Get applications" {
			$(Get-CheckPointApplications -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get applications full object" {
			$(Get-CheckPointApplications -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.Application.CheckPointApplication
		}
	}

	Context "Remove-CheckPointApplication" {
		It "Remove testing application" {
			Remove-CheckPointApplication -Session $Session -Name PesterApplication | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing application" {
			Remove-CheckPointApplication -Session $Session -Name PesterApplication | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}