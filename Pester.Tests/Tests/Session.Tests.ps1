Describe "Basic.Session" {
	Context "Get-CheckPointSessions" {
		$sessions = Get-CheckPointSessions -Session $Session
		$s = $sessions | where {$_.UID -eq $Session.UID}

		It "Confirm Get-CheckPointSessions returns current session" {
			$s | Should BeOfType psCheckPoint.Objects.Session.CheckPointSession
		}

		It "Confirm Application" {
			$s.Application | Should be "WEB_API"
		}

		It "Confirm Connection Mode" {
			$s.ConnectionMode | Should be "read write"
		}

		It "Confirm Description" {
			$s.Description | Should be "psCheckPoint Pester Testing"
		}
	}

	Context "Get-CheckPointSession" {
		$s = Get-CheckPointSession -Session $Session

		It "Confirm Get-CheckPointSessions returns current session" {
			$s | Should BeOfType psCheckPoint.Objects.Session.CheckPointSession
		}

		It "Confirm Application" {
			$s.Application | Should be "WEB_API"
		}

		It "Confirm Connection Mode" {
			$s.ConnectionMode | Should be "read write"
		}

		It "Confirm Description" {
			$s.Description | Should be "psCheckPoint Pester Testing"
		}
	}

	Context "PS Session Variable" {
		$secpasswd = ConvertTo-SecureString $Settings.Management.Password -AsPlainText -Force
		$mycreds = New-Object System.Management.Automation.PSCredential ($Settings.Management.User, $secpasswd)
		Open-CheckPointSession -NoCertificateValidation -ManagementServer $Settings.Management.Server -Credentials $mycreds -SessionName Pester -SessionDescription "psCheckPoint Pester Testing - Session"

		$s = Get-CheckPointSession

		It "Confirm Get-CheckPointSessions returns current session" {
			$s | Should BeOfType psCheckPoint.Objects.Session.CheckPointSession
		}

		It "Confirm Application" {
			$s.Application | Should be "WEB_API"
		}

		It "Confirm Connection Mode" {
			$s.ConnectionMode | Should be "read write"
		}

		It "Confirm Description" {
			$s.Description | Should be "psCheckPoint Pester Testing - Session"
		}

		Reset-CheckPointSession
		Close-CheckPointSession
	}
}