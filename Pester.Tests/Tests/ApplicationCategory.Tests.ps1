Describe "Basic.ApplicationCategory" {
	Context "New-CheckPointApplicationCategory" {
		It "Add" -Skip {
			New-CheckPointApplicationCategory -Session $Session -Name PesterCategory -PassThru  | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointApplicationCategory -Session $Session -Name PesterCategory -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointApplicationCategory" {
		It "Get" -Skip {
			Get-CheckPointApplicationCategory -Session $Session -Name PesterCategory | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointApplicationCategory -Session $Session -Name NotPesterCategory -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointApplicationCategory" {
		It "Set" -Skip {
			Set-CheckPointApplicationCategory -Session $Session -Name PesterCategory -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointApplicationCategory -Session $Session -Name NotPesterCategory -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointApplicationCategories" {
		It "Get" -Skip {
			$(Get-CheckPointApplicationCategories -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}

		It "Get full object" -Skip {
			$(Get-CheckPointApplicationCategories -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.ApplicationCategory
		}
	}

	Context "Remove-CheckPointApplicationCategory" {
		It "Remove" -Skip {
			{ Remove-CheckPointApplicationCategory -Session $Session -Name PesterCategory -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointApplicationCategory -Session $Session -Name PesterCategory -ErrorAction Stop } | Should Throw "not found"
		}
	}
}