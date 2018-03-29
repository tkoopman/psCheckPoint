Describe "Basic.ServiceGroup" {
	Context "New-CheckPointServiceGroup" {
		It "Add" -Skip {
			New-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -PassThru | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointServiceGroup" {
		It "Get" -Skip {
			Get-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointServiceGroup -Session $Session -Name NotPesterServiceGroup -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointServiceGroup" {
		It "Set" -Skip {
			Set-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointServiceGroup -Session $Session -Name NotPesterServiceGroup -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointServiceGroups" {
		It "Get" -Skip {
			$(Get-CheckPointServiceGroups -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}

		It "Get full object" -Skip {
			$(Get-CheckPointServiceGroups -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.ServiceGroup
		}
	}

	Context "Remove-CheckPointServiceGroup" {
		It "Remove" -Skip {
			{ Remove-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointServiceGroup -Session $Session -Name PesterServiceGroup -ErrorAction Stop } | Should Throw "not found"
		}
	}
}