Describe "Basic.Group" {
	Context "New-CheckPointGroup" {
		It "Add" {
			New-CheckPointGroup -Session $Session -Name PesterGroup -PassThru | Should BeOfType Koopman.CheckPoint.Group
		}

		It "Add duplicate" {
			{ New-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointGroup" {
		It "Get" {
			Get-CheckPointGroup -Session $Session -Name PesterGroup | Should BeOfType Koopman.CheckPoint.Group
		}

		It "Get non-existing" {
			{ Get-CheckPointGroup -Session $Session -Name NotPesterGroup -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointGroup" {
		It "Set" {
			Set-CheckPointGroup -Session $Session -Name PesterGroup -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.Group
		}

		It "Set non-existing" {
			{ Set-CheckPointGroup -Session $Session -Name NotPesterGroup -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointGroups" {
		It "Get" {
			$(Get-CheckPointGroups -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.Group
		}

		It "Get full object" {
			$(Get-CheckPointGroups -Session $Session).Objects[0] | Get-CheckPointFullObject | Should BeOfType Koopman.CheckPoint.Group
		}
	}

	Context "Remove-CheckPointGroup" {
		It "Remove" {
			{ Remove-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Throw "not found"
		}
	}
}