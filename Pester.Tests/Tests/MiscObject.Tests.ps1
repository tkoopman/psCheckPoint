Describe "Basic.MiscObject" {
	$PesterHost = New-CheckPointHost -Session $Session -Name PesterObject -IPAddress 192.168.3.22 -PassThru
	Context "Get-CheckPointObject" {
		It "Get" -Skip {
			Get-CheckPointObject -Session $Session -UID $PesterHost.UID | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.Host
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointObject -Session $Session -UID "12345678-1234-1234-1234-123456789abc" -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointObjects" {
		It "Get" -Skip {
			$(Get-CheckPointObjects -Session $Session -Filter PesterObject).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.Host
		}

		It "Get Unused" -Skip {
			Get-CheckPointObjects -Session $Session -Unused | Should BeOfType Koopman.CheckPoint.
		}
	}
}