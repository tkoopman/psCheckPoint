Describe "Basic.ServiceGroup" {
	Context "New-CheckPointServiceGroup" {
		It "Add" {
			New-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -PassThru | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}

		It "Add duplicate" {
			{ New-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointServiceGroup" {
		It "Get" {
			Get-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}

		It "Get non-existing" {
			{ Get-CheckPointServiceGroup -Session $Session -Name NotPesterServiceGroup -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointServiceGroup" {
		It "Set" {
			Set-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}

		It "Set non-existing" {
			{ Set-CheckPointServiceGroup -Session $Session -Name NotPesterServiceGroup -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointServiceGroups" {
		It "Get" {
			$(Get-CheckPointServiceGroups -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}

		It "Get full object" {
			$(Get-CheckPointServiceGroups -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}
	}

	Context "Remove-CheckPointServiceGroup" {
		It "Remove" {
			{ Remove-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -ErrorAction Stop } | Should Throw "not found"
		}
	}
}