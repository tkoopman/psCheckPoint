Describe "Basic.IA" {
	It "Add Identity" {
		$(Add-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -CertificateHash $Settings.IAGateway.Hash -IPAddress 192.168.1.2 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -Machine "PesterMachine" -Roles "TestIARole")[0] | Should BeOfType Koopman.CheckPoint.IA.AddIdentityResponse
	}
	It "Get Identity" {
		$(Get-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -CertificateHash $Settings.IAGateway.Hash -IPAddress 192.168.1.2)[0] | Should BeOfType Koopman.CheckPoint.IA.ShowIdentityResponse
	}
	It "Remove Identity" {
		$(Remove-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -CertificateHash $Settings.IAGateway.Hash -IPAddress 192.168.1.2)[0] | Should BeOfType Koopman.CheckPoint.IA.DeleteIdentityResponse
	}
}