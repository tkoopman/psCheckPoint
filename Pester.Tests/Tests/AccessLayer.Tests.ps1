Describe "Basic.AccessLayer" {
	Context "New-CheckPointAccessLayer" {
		It "Add" -Skip {
			New-CheckPointAccessLayer -Session $Session -Name PesterAccessLayer -PassThru  | Should BeOfType Koopman.CheckPoint.AccessLayer
		}

		# Seems Check Point allows Access Layers with duplicate names
		#It "Add duplicate" {
		#	{ New-CheckPointAccessLayer -Session $Session -Name PesterAccessLayer -ErrorAction Stop } | Should Throw
		#}
	}

	Context "Get-CheckPointAccessLayer" {
		It "Get" -Skip {
			Get-CheckPointAccessLayer -Session $Session -Name PesterAccessLayer | Should BeOfType Koopman.CheckPoint.AccessLayer
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointAccessLayer -Session $Session -Name NotPesterAccessLayer -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointAccessLayer" {
		It "Set" -Skip {
			Set-CheckPointAccessLayer -Session $Session -Name PesterAccessLayer -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.AccessLayer
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointAccessLayer -Session $Session -Name NotPesterAccessLayer -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointAccessLayers" {
		It "Get" -Skip {
			$(Get-CheckPointAccessLayers -Session $Session).AccessLayers[0] | Should BeOfType Koopman.CheckPoint.AccessLayer
		}

		It "Get full object" -Skip {
			$(Get-CheckPointAccessLayers -Session $Session).AccessLayers[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.AccessLayer
		}
	}

	Context "Remove-CheckPointAccessLayer" {
		It "Remove" -Skip {
			{ Remove-CheckPointAccessLayer -Session $Session -Name PesterAccessLayer -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointAccessLayer -Session $Session -Name PesterAccessLayer -ErrorAction Stop } | Should Throw "not found"
		}
	}
}