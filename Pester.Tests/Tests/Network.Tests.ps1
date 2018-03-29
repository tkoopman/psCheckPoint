Describe "Basic.Network" {
	Context "New-CheckPointNetwork" {
		It "Add" -Skip {
			New-CheckPointNetwork -Session $Session -Name PesterNet -Subnet 192.168.100.0 -MaskLength 24 -PassThru | Should BeOfType Koopman.CheckPoint.Network
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointNetwork -Session $Session -Name PesterNet -Subnet 192.168.100.0 -MaskLength 24 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointNetwork" {
		It "Get" -Skip {
			Get-CheckPointNetwork -Session $Session -Name PesterNet | Should BeOfType Koopman.CheckPoint.Network
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointNetwork -Session $Session -Name NotPesterNet -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointNetwork" {
		It "Set" -Skip {
			Set-CheckPointNetwork -Session $Session -Name PesterNet -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.Network
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointNetwork -Session $Session -Name NotPesterNet -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointNetworks" {
		It "Get" -Skip {
			$(Get-CheckPointNetworks -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.Network
		}

		It "Get full object" -Skip {
			$(Get-CheckPointNetworks -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.Network
		}
	}

	Context "Remove-CheckPointNetwork" {
		It "Remove" -Skip {
			{ Remove-CheckPointNetwork -Session $Session -Name PesterNet -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointNetwork -Session $Session -Name PesterNet -ErrorAction Stop } | Should Throw "not found"
		}
	}
}