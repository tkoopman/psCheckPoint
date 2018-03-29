Describe "Basic.Group" {
	Context "New-CheckPointGroup" {
		It "Add" -Skip {
			New-CheckPointGroup -Session $Session -Name PesterGroup -PassThru | Should BeOfType Koopman.CheckPoint.Group
		}

		It "Add duplicate" -Skip {
			{ New-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Throw "Validation failed"
		}
	}

	Context "Get-CheckPointGroup" {
		It "Get" -Skip {
			Get-CheckPointGroup -Session $Session -Name PesterGroup | Should BeOfType Koopman.CheckPoint.Group
		}

		It "Get non-existing" -Skip {
			{ Get-CheckPointGroup -Session $Session -Name NotPesterGroup -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Set-CheckPointGroup" {
		It "Set" -Skip {
			Set-CheckPointGroup -Session $Session -Name PesterGroup -Color Red -PassThru | Should BeOfType Koopman.CheckPoint.Group
		}

		It "Set non-existing" -Skip {
			{ Set-CheckPointGroup -Session $Session -Name NotPesterGroup -Color Red -ErrorAction Stop } | Should Throw "not found"
		}
	}

	Context "Get-CheckPointGroups" {
		It "Get" -Skip {
			$(Get-CheckPointGroups -Session $Session).Objects[0] | Should BeOfType Koopman.CheckPoint.Group
		}

		It "Get full object" -Skip {
			$(Get-CheckPointGroups -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType Koopman.CheckPoint.Group
		}
	}

	Context "Remove-CheckPointGroup" {
		It "Remove" -Skip {
			{ Remove-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" -Skip {
			{ Remove-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Throw "not found"
		}
	}
}