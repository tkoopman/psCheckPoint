Describe "Basic.AddressRange" {
	Context "New-CheckPointAddressRange" {
		It "Add" {
			New-CheckPointAddressRange -Session $Session -Name PesterAR -IPAddressFirst 192.168.1.2 -IPAddressLast 192.168.1.5 -PassThru | Should BeOfType psCheckPoint.Objects.AddressRange.CheckPointAddressRange
		}

		It "Add duplicate" {
			New-CheckPointAddressRange -Session $Session -Name PesterAR -IPAddressFirst 192.168.1.2 -IPAddressLast 192.168.1.5 | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointAddressRange" {
		It "Get" {
			Get-CheckPointAddressRange -Session $Session -Name PesterAR | Should BeOfType psCheckPoint.Objects.AddressRange.CheckPointAddressRange
		}

		It "Get non-existing" {
			Get-CheckPointAddressRange -Session $Session -Name NotPesterAR | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointAddressRange" {
		It "Set" {
			Set-CheckPointAddressRange -Session $Session -Name PesterAR -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.AddressRange.CheckPointAddressRange
		}

		It "Set non-existing" {
			Set-CheckPointAddressRange -Session $Session -Name NotPesterAR -Color Red | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointAddressRanges" {
		It "Get" {
			$(Get-CheckPointAddressRanges -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointAddressRanges -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.AddressRange.CheckPointAddressRange
		}
	}

	Context "Remove-CheckPointAddressRange" {
		It "Remove" {
			Remove-CheckPointAddressRange -Session $Session -Name PesterAR | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing" {
			Remove-CheckPointAddressRange -Session $Session -Name PesterAR | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}