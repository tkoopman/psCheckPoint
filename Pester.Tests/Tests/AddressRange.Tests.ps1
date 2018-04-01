Describe "Basic.AddressRange" {
	Context "New-CheckPointAddressRange" {
		It "Add" {
			New-CheckPointAddressRange -Session $Session -Name PesterAR -IPAddressFirst 192.168.1.2 -IPAddressLast 192.168.1.5 -PassThru | Should BeOfType Koopman.CheckPoint.AddressRange
		}

		It "Add duplicate" {
			{ New-CheckPointAddressRange -Session $Session -Name PesterAR -IPAddressFirst 192.168.1.2 -IPAddressLast 192.168.1.5 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointAddressRange" {
		It "Get" {
			Get-CheckPointAddressRange -Session $Session -Name PesterAR | Should BeOfType Koopman.CheckPoint.AddressRange
		}

		It "Get non-existing" {
			{ Get-CheckPointAddressRange -Session $Session -Name NotPesterAR -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointAddressRange" {
		It "Set" {
			Set-CheckPointAddressRange -Session $Session -Name PesterAR -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.AddressRange
		}

		It "Set non-existing" {
			{ Set-CheckPointAddressRange -Session $Session -Name NotPesterAR -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointAddressRanges" {
		It "Get" {
			$(Get-CheckPointAddressRanges -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.AddressRange
		}

		It "Get full object" {
			$(Get-CheckPointAddressRanges -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.AddressRange
		}
	}

	Context "Remove-CheckPointAddressRange" {
		It "Remove" {
			{ Remove-CheckPointAddressRange -Session $Session -Name PesterAR -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointAddressRange -Session $Session -Name PesterAR -ErrorAction Stop } | Should Throw "not found"
		}
	}
}