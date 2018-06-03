Describe "Export rule base" {
	It "Export" {
		$Export = Export-CheckPointObjects -Session $Session "domain-udp"
		$Export | Should BeOfType string
	}
}