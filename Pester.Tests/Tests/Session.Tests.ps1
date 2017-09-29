Describe "Basic.Session" {
	Context "Get-CheckPointSessions" {
		$sessions = Get-CheckPointSessions -Session $Session
		$s = $sessions | where {$_.UID -eq $Session.UID}

		It "Confirm Get-CheckPointSessions returns current session" {
			$s | Should Not BeNullOrEmpty
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
		$s = Get-CheckPointSession -Session $Session -UID $Session.UID

		It "Confirm Get-CheckPointSessions returns current session" {
			$s | Should Not BeNullOrEmpty
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
}