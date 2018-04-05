Describe "Basic.MulticastAddressRange" {
	Context "New-CheckPointMulticastAddressRange" {
		It "Add" {
			New-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -IPAddress 224.5.6.7 -PassThru | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}

		It "Add duplicate" {
			{ New-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -IPAddress 224.5.6.7 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointMulticastAddressRange" {
		It "Get" {
			Get-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}

		It "Get non-existing" {
			{ Get-CheckPointMulticastAddressRange -Session $Session -Name NotPesterMAR -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointMulticastAddressRange" {
		It "Set" {
			Set-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}

		It "Set non-existing" {
			{ Set-CheckPointMulticastAddressRange -Session $Session -Name NotPesterMAR -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointMulticastAddressRanges" {
		It "Get" {
			$(Get-CheckPointMulticastAddressRanges -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}

		It "Get full object" {
			$(Get-CheckPointMulticastAddressRanges -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}
	}

	Context "Remove-CheckPointMulticastAddressRange" {
		It "Remove" {
			{ Remove-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -ErrorAction Stop } | Should Throw "not found"
		}
	}
}