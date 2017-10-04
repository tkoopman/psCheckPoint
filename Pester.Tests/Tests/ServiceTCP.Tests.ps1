Describe "Basic.ServiceTCP" {
	Context "New-CheckPointServiceTCP" {
		It "Add" {
			New-CheckPointServiceTCP -Session $Session -Name PesterTCP -Port 1227 -PassThru | Should BeOfType psCheckPoint.Objects.ServiceTCP.CheckPointServiceTCP
		}

		It "Add duplicate" {
			New-CheckPointServiceTCP -Session $Session -Name PesterTCP -Port 1227 | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointServiceTCP" {
		It "Get" {
			Get-CheckPointServiceTCP -Session $Session -Name PesterTCP | Should BeOfType psCheckPoint.Objects.ServiceTCP.CheckPointServiceTCP
		}

		It "Get non-existing" {
			Get-CheckPointServiceTCP -Session $Session -Name NotPesterTCP | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointServiceTCP" {
		It "Set" {
			Set-CheckPointServiceTCP -Session $Session -Name PesterTCP -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.ServiceTCP.CheckPointServiceTCP
		}

		It "Set non-existing" {
			Set-CheckPointServiceTCP -Session $Session -Name NotPesterTCP -Color Red | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointServicesTCP" {
		It "Get" {
			$(Get-CheckPointServicesTCP -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.Service.CheckPointService
		}

		It "Get full object" {
			$(Get-CheckPointServicesTCP -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.ServiceTCP.CheckPointServiceTCP
		}
	}

	Context "Remove-CheckPointServiceTCP" {
		It "Remove" {
			Remove-CheckPointServiceTCP -Session $Session -Name PesterTCP | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing" {
			Remove-CheckPointServiceTCP -Session $Session -Name PesterTCP | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}