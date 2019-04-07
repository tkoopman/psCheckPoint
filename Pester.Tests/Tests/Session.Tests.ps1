Describe "Basic.Session" {
	Context "Get-CheckPointSessions" {
		$sessions = Get-CheckPointSessions -Session $Session
		$s = $sessions | where {$_.UID -eq $Session.UID}

		It "Confirm Get-CheckPointSessions returns current session" {
			$s | Should BeOfType Koopman.CheckPoint.SessionInfo
		}

		It "Confirm Application" {
			$s.Application | Should be "WEB_API"
		}

		It "Confirm Connection Mode" {
			$s.ConnectionMode | Should be "ReadWrite"
		}

		It "Confirm Description" {
			$s.Description | Should be "psCheckPoint Pester Testing"
		}
	}

	Context "Get-CheckPointSession" {
		$s = Get-CheckPointSession -Session $Session

		It "Confirm Get-CheckPointSessions returns current session" {
			$s | Should BeOfType Koopman.CheckPoint.SessionInfo
		}

		It "Confirm Application" {
			$s.Application | Should be "WEB_API"
		}

		It "Confirm Connection Mode" {
			$s.ConnectionMode | Should be "ReadWrite"
		}

		It "Confirm Description" {
			$s.Description | Should be "psCheckPoint Pester Testing"
		}
	}

	Context "PS Session Variable" {
		$secpasswd = ConvertTo-SecureString $Settings.($CPVersion).Management.Password -AsPlainText -Force
		$mycreds = New-Object System.Management.Automation.PSCredential ($Settings.($CPVersion).Management.User, $secpasswd)
		Open-CheckPointSession -ManagementServer $Settings.($CPVersion).Management.Server -Credentials $mycreds -CertificateHash $Settings.($CPVersion).Management.Hash -SessionName Pester -SessionDescription "psCheckPoint Pester Testing - Session"

		$s = Get-CheckPointSession

		It "Confirm Get-CheckPointSessions returns current session" {
			$s | Should BeOfType Koopman.CheckPoint.SessionInfo
		}

		It "Confirm Application" {
			$s.Application | Should be "WEB_API"
		}

		It "Confirm Connection Mode" {
			$s.ConnectionMode | Should be "ReadWrite"
		}

		It "Confirm Description" {
			$s.Description | Should be "psCheckPoint Pester Testing - Session"
		}

		Reset-CheckPointSession
		Close-CheckPointSession
	}
}