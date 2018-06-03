Describe "Basic.Network" {
	Context "New-CheckPointNetwork" {
		It "Add" {
			New-CheckPointNetwork -Session $Session -Name PesterNet -Subnet 192.168.100.0 -MaskLength 24 -PassThru | Should BeOfType Koopman.CheckPoint.Network
		}

		It "Add duplicate" {
			{ New-CheckPointNetwork -Session $Session -Name PesterNet -Subnet 192.168.100.0 -MaskLength 24 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointNetwork" {
		It "Get" {
			Get-CheckPointNetwork -Session $Session -Name PesterNet | Should BeOfType Koopman.CheckPoint.Network
		}

		It "Get non-existing" {
			{ Get-CheckPointNetwork -Session $Session -Name NotPesterNet -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointNetwork" {
		It "Set" {
			Set-CheckPointNetwork -Session $Session -Name PesterNet -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.Network
		}

		It "Set non-existing" {
			{ Set-CheckPointNetwork -Session $Session -Name NotPesterNet -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointNetworks" {
		It "Get" {
			$(Get-CheckPointNetworks -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.Network
		}

		It "Get full object" {
			$(Get-CheckPointNetworks -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.Network
		}
	}

	Context "Remove-CheckPointNetwork" {
		It "Remove" {
			{ Remove-CheckPointNetwork -Session $Session -Name PesterNet -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointNetwork -Session $Session -Name PesterNet -ErrorAction Stop } | Should Throw "not found"
		}
	}
}