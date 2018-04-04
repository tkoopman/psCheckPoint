Describe "Basic.Tasks" {
	Context "Invoke-CheckPointScript" {
		$task = Invoke-CheckPointScript -ScriptName "Get Configuration" -Script "clish -c 'show hostname'" -Targets mgmt -Session $Session

		It "Confirm Invoke-CheckPointScript returns task" {
			$task["mgmt"] | Should BeOfType System.String
		}

		$t = $task["mgmt"] | Wait-CheckPointTask -Session $Session

		It "Confirm task completes" {
			$t.Status | Should be "Succeeded"
		}

		It "Confirm task includes script response" {
			$t.TaskDetails.ResponseMessage | Should -Match "mgmt"
		}
	}
}