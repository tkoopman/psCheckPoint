Describe "Basic.SimpleGateway" {
	Context "Get-CheckPointSimpleGateways" {
		It "Get" -Skip {
			$(Get-CheckPointSimpleGateways -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.SimpleGateway
		}

		It "Get full object" -Skip {
			$(Get-CheckPointSimpleGateways -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.SimpleGateway
		}
	}
}