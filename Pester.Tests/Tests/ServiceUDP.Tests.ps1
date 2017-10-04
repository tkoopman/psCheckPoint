Describe "Basic.ServiceUDP" {
	Context "New-CheckPointServiceUDP" {
		It "Add" {
			New-CheckPointServiceUDP -Session $Session -Name PesterUDP -Port 1227 -PassThru | Should BeOfType psCheckPoint.Objects.ServiceUDP.CheckPointServiceUDP
		}

		It "Add duplicate" {
			New-CheckPointServiceUDP -Session $Session -Name PesterUDP -Port 1227 | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointServiceUDP" {
		It "Get" {
			Get-CheckPointServiceUDP -Session $Session -Name PesterUDP | Should BeOfType psCheckPoint.Objects.ServiceUDP.CheckPointServiceUDP
		}

		It "Get non-existing" {
			Get-CheckPointServiceUDP -Session $Session -Name NotPesterUDP | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointServiceUDP" {
		It "Set" {
			Set-CheckPointServiceUDP -Session $Session -Name PesterUDP -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.ServiceUDP.CheckPointServiceUDP
		}

		It "Set non-existing" {
			Set-CheckPointServiceUDP -Session $Session -Name NotPesterUDP -Color Red | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointServicesUDP" {
		It "Get" {
			$(Get-CheckPointServicesUDP -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.Service.CheckPointService
		}

		It "Get full object" {
			$(Get-CheckPointServicesUDP -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.ServiceUDP.CheckPointServiceUDP
		}
	}

	Context "Remove-CheckPointServiceUDP" {
		It "Remove" {
			Remove-CheckPointServiceUDP -Session $Session -Name PesterUDP | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing" {
			Remove-CheckPointServiceUDP -Session $Session -Name PesterUDP | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}