Describe "Basic.ServiceTCP" {
	Context "New-CheckPointServiceTCP" {
		It "Add" -Skip {
			New-CheckPointServiceTCP -Session $Session -Name PesterTCP -Port 1227 -PassThru | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointServiceTCP -Session $Session -Name PesterTCP -Port 1227 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointServiceTCP" {
		It "Get" -Skip {
			Get-CheckPointServiceTCP -Session $Session -Name PesterTCP | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointServiceTCP -Session $Session -Name NotPesterTCP -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointServiceTCP" {
		It "Set" -Skip {
			Set-CheckPointServiceTCP -Session $Session -Name PesterTCP -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointServiceTCP -Session $Session -Name NotPesterTCP -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointServicesTCP" {
		It "Get" -Skip {
			$(Get-CheckPointServicesTCP -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}

		It "Get full object" -Skip {
			$(Get-CheckPointServicesTCP -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.ServiceTCP
		}
	}

	Context "Remove-CheckPointServiceTCP" {
		It "Remove" -Skip {
			{ Remove-CheckPointServiceTCP -Session $Session -Name PesterTCP -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointServiceTCP -Session $Session -Name PesterTCP -ErrorAction Stop } | Should Throw "not found"
		}
	}
}