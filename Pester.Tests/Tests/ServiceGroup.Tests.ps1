Describe "Basic.ServiceGroup" {
	Context "New-CheckPointServiceGroup" {
		It "Add" {
			New-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -PassThru | Should BeOfType psCheckPoint.Objects.ServiceGroup.CheckPointServiceGroup
		}

		It "Add duplicate" {
			New-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointServiceGroup" {
		It "Get" {
			Get-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup | Should BeOfType psCheckPoint.Objects.ServiceGroup.CheckPointServiceGroup
		}

		It "Get non-existing" {
			Get-CheckPointServiceGroup -Session $Session -Name NotPesterServiceGroup | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointServiceGroup" {
		It "Set" {
			Set-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.ServiceGroup.CheckPointServiceGroup
		}

		It "Set non-existing" {
			Set-CheckPointServiceGroup -Session $Session -Name NotPesterServiceGroup -Color Red | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointServiceGroups" {
		It "Get" {
			$(Get-CheckPointServiceGroups -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointServiceGroups -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.ServiceGroup.CheckPointServiceGroup
		}
	}

	Context "Remove-CheckPointServiceGroup" {
		It "Remove" {
			Remove-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing" {
			Remove-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}