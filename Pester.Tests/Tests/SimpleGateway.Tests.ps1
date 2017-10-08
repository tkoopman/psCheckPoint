Describe "Basic.SimpleGateway" {
	Context "Get-CheckPointSimpleGateways" {
		It "Get" {
			$(Get-CheckPointSimpleGateways -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointSimpleGateways -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.SimpleGateway.CheckPointSimpleGateway
		}
	}
}