﻿Describe "Basic.SecurityZone" {
	Context "New-CheckPointSecurityZone" {
		It "Add" {
			New-CheckPointSecurityZone -Session $Session -Name PesterSZ -PassThru | Should BeOfType psCheckPoint.Objects.SecurityZone.CheckPointSecurityZone
		}

		It "Add duplicate" {
			{ New-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointSecurityZone" {
		It "Get" {
			Get-CheckPointSecurityZone -Session $Session -Name PesterSZ | Should BeOfType psCheckPoint.Objects.SecurityZone.CheckPointSecurityZone
		}

		It "Get non-existing" {
			{ Get-CheckPointSecurityZone -Session $Session -Name NotPesterSZ -ErrorAction Stop } | Should Throw
		}
	}

	Context "Set-CheckPointSecurityZone" {
		It "Set" {
			Set-CheckPointSecurityZone -Session $Session -Name PesterSZ -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.SecurityZone.CheckPointSecurityZone
		}

		It "Set non-existing" {
			{ Set-CheckPointSecurityZone -Session $Session -Name NotPesterSZ -Color Red -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointSecurityZones" {
		It "Get" {
			$(Get-CheckPointSecurityZones -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointSecurityZones -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.SecurityZone.CheckPointSecurityZone
		}
	}

	Context "Remove-CheckPointSecurityZone" {
		It "Remove" {
			{ Remove-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointSecurityZone -Session $Session -Name PesterSZ -ErrorAction Stop } | Should Throw
		}
	}
}