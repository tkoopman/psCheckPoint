Describe "Basic.SecurityZone" {
	Context "New-CheckPointSecurityZone" {
		It "Add" {
			New-CheckPointSecurityZone -Session $Session -Name PesterSZ -PassThru | Should BeOfType Koopman.CheckPoint.SecurityZone
		}

		It "Add duplicate" {
			{ New-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointSecurityZone" {
		It "Get" {
			Get-CheckPointSecurityZone -Session $Session -Name PesterSZ | Should BeOfType Koopman.CheckPoint.SecurityZone
		}

		It "Get non-existing" {
			{ Get-CheckPointSecurityZone -Session $Session -Name NotPesterSZ -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointSecurityZone" {
		It "Set" {
			Set-CheckPointSecurityZone -Session $Session -Name PesterSZ -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.SecurityZone
		}

		It "Set non-existing" {
			{ Set-CheckPointSecurityZone -Session $Session -Name NotPesterSZ -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointSecurityZones" {
		It "Get" {
			$(Get-CheckPointSecurityZones -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.SecurityZone
		}

		It "Get full object" {
			$(Get-CheckPointSecurityZones -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.SecurityZone
		}
	}

	Context "Remove-CheckPointSecurityZone" {
		It "Remove" {
			{ Remove-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Throw "not found"
		}
	}
}