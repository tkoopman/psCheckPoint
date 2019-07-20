Describe "Basic.IA" {
	It "Add Identity" {
		$(Add-CheckPointIdentity -Gateway $Settings.($CPVersion).IAGateway.Server -SharedSecret $Settings.($CPVersion).IAGateway.SharedSecret -CertificateHash $Settings.($CPVersion).IAGateway.Hash -IPAddress 192.168.1.2 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -Machine "PesterMachine" -Roles "TestIARole")[0] | Should BeOfType Koopman.CheckPoint.IA.AddIdentityResponse
	}
	It "Get Identity" {
		$(Get-CheckPointIdentity -Gateway $Settings.($CPVersion).IAGateway.Server -SharedSecret $Settings.($CPVersion).IAGateway.SharedSecret -CertificateHash $Settings.($CPVersion).IAGateway.Hash -IPAddress 192.168.1.2)[0] | Should BeOfType Koopman.CheckPoint.IA.ShowIdentityResponse
	}
	It "Remove Identity" {
		$(Remove-CheckPointIdentity -Gateway $Settings.($CPVersion).IAGateway.Server -SharedSecret $Settings.($CPVersion).IAGateway.SharedSecret -CertificateHash $Settings.($CPVersion).IAGateway.Hash -IPAddress 192.168.1.2)[0] | Should BeOfType Koopman.CheckPoint.IA.DeleteIdentityResponse
	}
}