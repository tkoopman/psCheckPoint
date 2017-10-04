Describe "Basic.MulticastAddressRange" {
	Context "New-CheckPointMulticastAddressRange" {
		It "Add" {
			New-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -IPAddress 224.5.6.7 -PassThru | Should BeOfType psCheckPoint.Objects.MulticastAddressRange.CheckPointMulticastAddressRange
		}

		It "Add duplicate" {
			New-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -IPAddress 224.5.6.7 | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointMulticastAddressRange" {
		It "Get" {
			Get-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR | Should BeOfType psCheckPoint.Objects.MulticastAddressRange.CheckPointMulticastAddressRange
		}

		It "Get non-existing" {
			Get-CheckPointMulticastAddressRange -Session $Session -Name NotPesterMAR | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointMulticastAddressRange" {
		It "Set" {
			Set-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.MulticastAddressRange.CheckPointMulticastAddressRange
		}

		It "Set non-existing" {
			Set-CheckPointMulticastAddressRange -Session $Session -Name NotPesterMAR -Color Red | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointMulticastAddressRanges" {
		It "Get" {
			$(Get-CheckPointMulticastAddressRanges -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointMulticastAddressRanges -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.MulticastAddressRange.CheckPointMulticastAddressRange
		}
	}

	Context "Remove-CheckPointMulticastAddressRange" {
		It "Remove" {
			Remove-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing" {
			Remove-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}