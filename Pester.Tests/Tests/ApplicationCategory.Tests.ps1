Describe "Basic.ApplicationCategory" {
	Context "New-CheckPointApplicationCategory" {
		It "Add" {
			New-CheckPointApplicationCategory -Session $Session -Name PesterCategory -PassThru  | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}

		It "Add duplicate" {
			{ New-CheckPointApplicationCategory -Session $Session -Name PesterCategory -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointApplicationCategory" {
		It "Get" {
			Get-CheckPointApplicationCategory -Session $Session -Name PesterCategory | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}

		It "Get non-existing" {
			{ Get-CheckPointApplicationCategory -Session $Session -Name NotPesterCategory -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointApplicationCategory" {
		It "Set" {
			Set-CheckPointApplicationCategory -Session $Session -Name PesterCategory -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}

		It "Set non-existing" {
			{ Set-CheckPointApplicationCategory -Session $Session -Name NotPesterCategory -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointApplicationCategories" {
		It "Get" {
			$(Get-CheckPointApplicationCategories -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}

		It "Get full object" {
			$(Get-CheckPointApplicationCategories -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}
	}

	Context "Remove-CheckPointApplicationCategory" {
		It "Remove" {
			{ Remove-CheckPointApplicationCategory -Session $Session -Name PesterCategory -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointApplicationCategory -Session $Session -Name PesterCategory -ErrorAction Stop } | Should Throw "not found"
		}
	}
}