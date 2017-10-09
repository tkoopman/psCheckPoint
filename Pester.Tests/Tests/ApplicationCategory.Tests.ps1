Describe "Basic.ApplicationCategory" {
	Context "New-CheckPointApplicationCategory" {
		It "Add" {
			New-CheckPointApplicationCategory -Session $Session -Name PesterCategory -PassThru  | Should BeOfType psCheckPoint.Objects.ApplicationCategory.CheckPointApplicationCategory
		}

		It "Add duplicate" {
			{ New-CheckPointApplicationCategory -Session $Session -Name PesterCategory -PrimaryCategory "Low Risk" -UrlList www.google.com,www.bing.com -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointApplicationCategory" {
		It "Get" {
			Get-CheckPointApplicationCategory -Session $Session -Name PesterCategory | Should BeOfType psCheckPoint.Objects.ApplicationCategory.CheckPointApplicationCategory
		}

		It "Get non-existing" {
			{ Get-CheckPointApplicationCategory -Session $Session -Name NotPesterCategory -ErrorAction Stop } | Should Throw
		}
	}

	Context "Set-CheckPointApplicationCategory" {
		It "Set" {
			Set-CheckPointApplicationCategory -Session $Session -Name PesterCategory -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.ApplicationCategory.CheckPointApplicationCategory
		}

		It "Set non-existing" {
			{ Set-CheckPointApplicationCategory -Session $Session -Name NotPesterCategory -Color Red -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointApplicationCategories" {
		It "Get" {
			$(Get-CheckPointApplicationCategories -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointApplicationCategories -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.ApplicationCategory.CheckPointApplicationCategory
		}
	}

	Context "Remove-CheckPointApplicationCategory" {
		It "Remove" {
			{ Remove-CheckPointApplicationCategory -Session $Session -Name PesterCategory -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointApplicationCategory -Session $Session -Name PesterCategory -ErrorAction Stop } | Should Throw
		}
	}
}