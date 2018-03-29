Describe "Basic.ServiceUDP" {
	Context "New-CheckPointServiceUDP" {
		It "Add" -Skip {
			New-CheckPointServiceUDP -Session $Session -Name PesterUDP -Port 1227 -PassThru | Should BeOfType Koopman.CheckPoint.ServiceUDP
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointServiceUDP -Session $Session -Name PesterUDP -Port 1227 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointServiceUDP" {
		It "Get" -Skip {
			Get-CheckPointServiceUDP -Session $Session -Name PesterUDP | Should BeOfType Koopman.CheckPoint.ServiceUDP
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointServiceUDP -Session $Session -Name NotPesterUDP -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointServiceUDP" {
		It "Set" -Skip {
			Set-CheckPointServiceUDP -Session $Session -Name PesterUDP -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.ServiceUDP
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointServiceUDP -Session $Session -Name NotPesterUDP -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointServicesUDP" {
		It "Get" -Skip {
			$(Get-CheckPointServicesUDP -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.ServiceUDP
		}

		It "Get full object" -Skip {
			$(Get-CheckPointServicesUDP -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.ServiceUDP
		}
	}

	Context "Remove-CheckPointServiceUDP" {
		It "Remove" -Skip {
			{ Remove-CheckPointServiceUDP -Session $Session -Name PesterUDP -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointServiceUDP -Session $Session -Name PesterUDP -ErrorAction Stop } | Should Throw "not found"
		}
	}
}