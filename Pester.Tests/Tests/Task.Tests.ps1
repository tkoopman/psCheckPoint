Describe "Basic.Tasks" {
	Context "Invoke-CheckPointScript" {
		$task = Invoke-CheckPointScript -ScriptName "Get Configuration" -Script "clish -c 'show hostname'" -Targets $Settings.Management.Name -Session $Session

		It "Confirm Invoke-CheckPointScript returns task" {
			$task[$Settings.Management.Name] | Should BeOfType System.String
		}

		$t = $task[$Settings.Management.Name] | Wait-CheckPointTask -Session $Session

		It "Confirm task completes" {
			$t.Status | Should be "Succeeded"
		}

		It "Confirm task includes script response" {
			$t.TaskDetails.ResponseMessage | Should -Match $Settings.Management.Name
		}
	}
}