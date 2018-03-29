Describe "Basic.MulticastAddressRange" {
	Context "New-CheckPointMulticastAddressRange" {
		It "Add" -Skip {
			New-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -IPAddress 224.5.6.7 -PassThru | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -IPAddress 224.5.6.7 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointMulticastAddressRange" {
		It "Get" -Skip {
			Get-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointMulticastAddressRange -Session $Session -Name NotPesterMAR -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointMulticastAddressRange" {
		It "Set" -Skip {
			Set-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointMulticastAddressRange -Session $Session -Name NotPesterMAR -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointMulticastAddressRanges" {
		It "Get" -Skip {
			$(Get-CheckPointMulticastAddressRanges -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}

		It "Get full object" -Skip {
			$(Get-CheckPointMulticastAddressRanges -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.MulticastAddressRange
		}
	}

	Context "Remove-CheckPointMulticastAddressRange" {
		It "Remove" -Skip {
			{ Remove-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointMulticastAddressRange -Session $Session -Name PesterMAR -ErrorAction Stop } | Should Throw "not found"
		}
	}
}