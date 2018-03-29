Describe "Basic.AddressRange" {
	Context "New-CheckPointAddressRange" {
		It "Add" -Skip {
			New-CheckPointAddressRange -Session $Session -Name PesterAR -IPAddressFirst 192.168.1.2 -IPAddressLast 192.168.1.5 -PassThru | Should BeOfType Koopman.CheckPoint.AddressRange
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointAddressRange -Session $Session -Name PesterAR -IPAddressFirst 192.168.1.2 -IPAddressLast 192.168.1.5 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointAddressRange" {
		It "Get" -Skip {
			Get-CheckPointAddressRange -Session $Session -Name PesterAR | Should BeOfType Koopman.CheckPoint.AddressRange
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointAddressRange -Session $Session -Name NotPesterAR -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointAddressRange" {
		It "Set" -Skip {
			Set-CheckPointAddressRange -Session $Session -Name PesterAR -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.AddressRange
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointAddressRange -Session $Session -Name NotPesterAR -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointAddressRanges" {
		It "Get" -Skip {
			$(Get-CheckPointAddressRanges -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.AddressRange
		}

		It "Get full object" -Skip {
			$(Get-CheckPointAddressRanges -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.AddressRange
		}
	}

	Context "Remove-CheckPointAddressRange" {
		It "Remove" -Skip {
			{ Remove-CheckPointAddressRange -Session $Session -Name PesterAR -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointAddressRange -Session $Session -Name PesterAR -ErrorAction Stop } | Should Throw "not found"
		}
	}
}