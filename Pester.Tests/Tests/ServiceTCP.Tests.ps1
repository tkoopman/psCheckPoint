Describe "Basic.ServiceTCP" {
	Context "New-CheckPointServiceTCP" {
		It "Add" {
			New-CheckPointServiceTCP -Session $Session -Name PesterTCP -Port 1227 -PassThru | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}

		It "Add duplicate" {
			{ New-CheckPointServiceTCP -Session $Session -Name PesterTCP -Port 1227 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointServiceTCP" {
		It "Get" {
			Get-CheckPointServiceTCP -Session $Session -Name PesterTCP | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}

		It "Get non-existing" {
			{ Get-CheckPointServiceTCP -Session $Session -Name NotPesterTCP -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointServiceTCP" {
		It "Set" {
			Set-CheckPointServiceTCP -Session $Session -Name PesterTCP -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}

		It "Set non-existing" {
			{ Set-CheckPointServiceTCP -Session $Session -Name NotPesterTCP -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointServicesTCP" {
		It "Get" {
			$(Get-CheckPointServicesTCP -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}

		It "Get full object" {
			$(Get-CheckPointServicesTCP -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}
	}

	Context "Remove-CheckPointServiceTCP" {
		It "Remove" {
			{ Remove-CheckPointServiceTCP -Session $Session -Name PesterTCP -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointServiceTCP -Session $Session -Name PesterTCP -ErrorAction Stop } | Should Throw "not found"
		}
	}
}