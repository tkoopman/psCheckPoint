Describe "Basic.Session" {
	It "Checks session API version" {
		$Session.APIServerVersion | Should Be "1.1"
	}
}