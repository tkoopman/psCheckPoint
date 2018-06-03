Describe "Basic.SimpleGateway" {
	Context "Get-CheckPointSimpleGateways" {
		It "Get" {
			$(Get-CheckPointSimpleGateways -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.SimpleGateway
		}

		It "Get full object" {
			$(Get-CheckPointSimpleGateways -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.SimpleGateway
		}
	}
}