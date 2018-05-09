Describe "Basic.IA" {
	It "Add Identity" {
		$(Add-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -CertificateHash $Settings.IAGateway.Hash -IPAddress 192.168.1.2 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -Machine "PesterMachine" -Roles "TestIARole")[0]
	}
	It "Get Identity" {
		$(Get-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -CertificateHash $Settings.IAGateway.Hash -IPAddress 192.168.1.2)[0]
	}
	It "Remove Identity" {
		$(Remove-CheckPointIdentity -Gateway $Settings.IAGateway.Server -SharedSecret $Settings.IAGateway.SharedSecret -CertificateHash $Settings.IAGateway.Hash -IPAddress 192.168.1.2)[0]
	}
}