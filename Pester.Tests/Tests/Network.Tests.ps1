Describe "Basic.Network" {
	Context "New-CheckPointNetwork" {
		It "Add" {
			New-CheckPointNetwork -Session $Session -Name PesterNet -Subnet 192.168.100.0 -MaskLength 24 | Should BeOfType psCheckPoint.Objects.Network.CheckPointNetwork
		}

		It "Add duplicate" {
			New-CheckPointNetwork -Session $Session -Name PesterNet -Subnet 192.168.100.0 -MaskLength 24 | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointNetwork" {
		It "Get" {
			Get-CheckPointNetwork -Session $Session -Name PesterNet | Should BeOfType psCheckPoint.Objects.Network.CheckPointNetwork
		}

		It "Get non-existing" {
			Get-CheckPointNetwork -Session $Session -Name NotPesterNet | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointNetwork" {
		It "Set" {
			Set-CheckPointNetwork -Session $Session -Name PesterNet -Color Red | Should BeOfType psCheckPoint.Objects.Network.CheckPointNetwork
		}

		It "Set non-existing" {
			Set-CheckPointNetwork -Session $Session -Name NotPesterNet -Color Red | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointNetworks" {
		It "Get" {
			$(Get-CheckPointNetworks -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointNetworks -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.Network.CheckPointNetwork
		}
	}

	Context "Remove-CheckPointNetwork" {
		It "Remove" {
			Remove-CheckPointNetwork -Session $Session -Name PesterNet | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing" {
			Remove-CheckPointNetwork -Session $Session -Name PesterNet | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}