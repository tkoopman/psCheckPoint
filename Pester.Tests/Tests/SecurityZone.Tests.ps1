Describe "Basic.SecurityZone" {
	Context "New-CheckPointSecurityZone" {
		It "Add" -Skip {
			New-CheckPointSecurityZone -Session $Session -Name PesterSZ -PassThru | Should BeOfType Koopman.CheckPoint.SecurityZone
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointSecurityZone" {
		It "Get" -Skip {
			Get-CheckPointSecurityZone -Session $Session -Name PesterSZ | Should BeOfType Koopman.CheckPoint.SecurityZone
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointSecurityZone -Session $Session -Name NotPesterSZ -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointSecurityZone" {
		It "Set" -Skip {
			Set-CheckPointSecurityZone -Session $Session -Name PesterSZ -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.SecurityZone
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointSecurityZone -Session $Session -Name NotPesterSZ -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointSecurityZones" {
		It "Get" -Skip {
			$(Get-CheckPointSecurityZones -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.SecurityZone
		}

		It "Get full object" -Skip {
			$(Get-CheckPointSecurityZones -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.SecurityZone
		}
	}

	Context "Remove-CheckPointSecurityZone" {
		It "Remove" -Skip {
			{ Remove-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Throw "not found"
		}
	}
}