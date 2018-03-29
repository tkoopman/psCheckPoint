Describe "Basic.IA" {
	It "Add Identity" -Skip {
		$(Add-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -NoCertificateValidation -IPAddress 192.168.1.2 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -Machine "PesterMachine" -Roles "PesterRole")[0] | Should BeOfType psCheckPoint.IA.AddIdentityResponse
	}
	It "Get Identity" -Skip {
		$(Get-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -NoCertificateValidation -IPAddress 192.168.1.2)[0] | Should BeOfType psCheckPoint.IA.GetIdentityResponse
	}
	It "Remove Identity" -Skip {
		$(Remove-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -NoCertificateValidation -IPAddress 192.168.1.2)[0] | Should BeOfType psCheckPoint.IA.RemoveIdentityResponse
	}
}