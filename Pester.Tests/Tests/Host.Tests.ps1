Describe "Basic.Host" {
	Context "New-CheckPointHost" {
		It "Add" {
			New-CheckPointHost -Session $Session -Name PesterHost -IPAddress 192.168.1.2 -PassThru | Should BeOfType Koopman.CheckPoint.Host
		}

		It "Add duplicate" {
			{ New-CheckPointHost -Session $Session -Name PesterHost -IPAddress 192.168.1.2 -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointHost" {
		It "Get" {
			Get-CheckPointHost -Session $Session -Name PesterHost | Should BeOfType Koopman.CheckPoint.Host
		}

		It "Get non-existing" {
			{ Get-CheckPointHost -Session $Session -Name NotPesterHost -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointHost" {
		It "Set" {
			Set-CheckPointHost -Session $Session -Name PesterHost -Color Red -IPAddress 10.1.1.100 -PassThru | Should BeOfType Koopman.CheckPoint.Host
		}

		It "Set non-existing" {
			{ Set-CheckPointHost -Session $Session -Name NotPesterHost -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointHosts" {
		It "Get" {
			$(Get-CheckPointHosts -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.Host
		}

		It "Get full object" {
			$(Get-CheckPointHosts -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.Host
		}
	}

	Context "Remove-CheckPointHost" {
		It "Remove" {
			{ Remove-CheckPointHost -Session $Session -Name PesterHost -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointHost -Session $Session -Name PesterHost -ErrorAction Stop } | Should Throw "not found"
		}
	}
}