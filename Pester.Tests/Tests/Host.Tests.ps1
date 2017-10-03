Describe "Basic.Host" {
	Context "New-CheckPointHost" {
		It "Add" {
			New-CheckPointHost -Session $Session -Name PesterHost -IPAddress 192.168.1.2 | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Add duplicate" {
			New-CheckPointHost -Session $Session -Name PesterHost -IPAddress 192.168.1.2 | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointHost" {
		It "Get" {
			Get-CheckPointHost -Session $Session -Name PesterHost | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Get non-existing" {
			Get-CheckPointHost -Session $Session -Name NotPesterHost | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointHost" {
		It "Set" {
			Set-CheckPointHost -Session $Session -Name PesterHost -Color Red | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Set non-existing" {
			Set-CheckPointHost -Session $Session -Name NotPesterHost -Color Red | Should BeOfType psCheckPoint.CheckPointError
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
			Remove-CheckPointHost -Session $Session -Name PesterHost | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing" {
			Remove-CheckPointHost -Session $Session -Name PesterHost | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}