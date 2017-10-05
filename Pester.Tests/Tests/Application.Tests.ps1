﻿Describe "Basic.Application" {
	Context "New-CheckPointApplication" {
		It "Add" {
			New-CheckPointApplication -Session $Session -Name PesterApplication -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com -PassThru  | Should BeOfType psCheckPoint.Objects.Application.CheckPointApplication
		}

		It "Add duplicate" {
			{ New-CheckPointApplication -Session $Session -Name PesterApplication -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointApplication" {
		It "Get" {
			Get-CheckPointApplication -Session $Session -Name PesterApplication | Should BeOfType psCheckPoint.Objects.Application.CheckPointApplication
		}

		It "Get non-existing" {
			{ Get-CheckPointApplication -Session $Session -Name NotPesterApplication -ErrorAction Stop } | Should Throw
		}
	}

	Context "Set-CheckPointApplication" {
		It "Set" {
			Set-CheckPointApplication -Session $Session -Name PesterApplication -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.Application.CheckPointApplication
		}

		It "Set non-existing" {
			{ Set-CheckPointApplication -Session $Session -Name NotPesterApplication -Color Red -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointApplications" {
		It "Get" {
			$(Get-CheckPointApplications -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointApplications -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.Application.CheckPointApplication
		}
	}

	Context "Remove-CheckPointApplication" {
		It "Remove" {
			{ Remove-CheckPointApplication -Session $Session -Name PesterApplication -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointApplication -Session $Session -Name PesterApplication -ErrorAction Stop } | Should Throw
		}
	}
}