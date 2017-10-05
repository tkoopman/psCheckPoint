Describe "Basic.Host" {
	Context "New-CheckPointHost" {
		It "Add" {
			New-CheckPointHost -Session $Session -Name PesterHost -IPAddress 192.168.1.2 -PassThru | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Add duplicate" {
			{ New-CheckPointHost -Session $Session -Name PesterHost -IPAddress 192.168.1.2 -ErrorAction Stop } | Should Throw
		}
	}

	Context "New-CheckPointHostInterface" {
		It "Add" {
			New-CheckPointHostInterface -Session $Session -Host PesterHost -Name eth0 -Subnet4 192.168.1.2 -MaskLength4 24 | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Add duplicate" {
			{ New-CheckPointHostInterface -Session $Session -Host PesterHost -Name eth0 -Subnet4 192.168.1.2 -MaskLength4 24 -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointHost" {
		It "Get" {
			Get-CheckPointHost -Session $Session -Name PesterHost | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Get non-existing" {
			{ Get-CheckPointHost -Session $Session -Name NotPesterHost -ErrorAction Stop } | Should Throw
		}
	}

	Context "Set-CheckPointHost" {
		It "Set" {
			Set-CheckPointHost -Session $Session -Name PesterHost -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Set non-existing" {
			{ Set-CheckPointHost -Session $Session -Name NotPesterHost -Color Red -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointHosts" {
		It "Get" {
			$(Get-CheckPointHosts -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointHosts -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}
	}

	Context "Remove-CheckPointHost" {
		It "Remove" {
			{ Remove-CheckPointHost -Session $Session -Name PesterHost -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointHost -Session $Session -Name PesterHost -ErrorAction Stop } | Should Throw
		}
	}
}