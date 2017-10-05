Describe "Basic.ServiceUDP" {
	Context "New-CheckPointServiceUDP" {
		It "Add" {
			New-CheckPointServiceUDP -Session $Session -Name PesterUDP -Port 1227 -PassThru | Should BeOfType psCheckPoint.Objects.ServiceUDP.CheckPointServiceUDP
		}

		It "Add duplicate" {
			{ New-CheckPointServiceUDP -Session $Session -Name PesterUDP -Port 1227 -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointServiceUDP" {
		It "Get" {
			Get-CheckPointServiceUDP -Session $Session -Name PesterUDP | Should BeOfType psCheckPoint.Objects.ServiceUDP.CheckPointServiceUDP
		}

		It "Get non-existing" {
			{ Get-CheckPointServiceUDP -Session $Session -Name NotPesterUDP -ErrorAction Stop } | Should Throw
		}
	}

	Context "Set-CheckPointServiceUDP" {
		It "Set" {
			Set-CheckPointServiceUDP -Session $Session -Name PesterUDP -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.ServiceUDP.CheckPointServiceUDP
		}

		It "Set non-existing" {
			{ Set-CheckPointServiceUDP -Session $Session -Name NotPesterUDP -Color Red -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointServicesUDP" {
		It "Get" {
			$(Get-CheckPointServicesUDP -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.Service.CheckPointService
		}

		It "Get full object" {
			$(Get-CheckPointServicesUDP -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.ServiceUDP.CheckPointServiceUDP
		}
	}

	Context "Remove-CheckPointServiceUDP" {
		It "Remove" {
			{ Remove-CheckPointServiceUDP -Session $Session -Name PesterUDP -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointServiceUDP -Session $Session -Name PesterUDP -ErrorAction Stop } | Should Throw
		}
	}
}