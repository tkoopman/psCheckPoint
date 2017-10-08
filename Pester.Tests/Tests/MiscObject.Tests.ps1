Describe "Basic.MiscObject" {
	$PesterHost = New-CheckPointHost -Session $Session -Name PesterObject -IPAddress 192.168.3.2 -PassThru
	Context "Get-CheckPointObject" {
		It "Get" {
			Get-CheckPointObject -Session $Session -UID $PesterHost.UID | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Get non-existing" {
			{ Get-CheckPointObject -Session $Session -UID "12345678-1234-1234-1234-123456789abc" -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointObjects" {
		It "Get" {
			$(Get-CheckPointObjects -Session $Session -Filter PesterObject).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.Host.CheckPointHost
		}

		It "Get Unused" {
			Get-CheckPointObjects -Session $Session -Unused | Should BeOfType psCheckPoint.Objects.CheckPointObjects
		}
	}
}