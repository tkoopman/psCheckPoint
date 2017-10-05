Describe "Basic.Group" {
	Context "New-CheckPointGroup" {
		It "Add" {
			New-CheckPointGroup -Session $Session -Name PesterGroup -PassThru | Should BeOfType psCheckPoint.Objects.Group.CheckPointGroup
		}

		It "Add duplicate" {
			{ New-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointGroup" {
		It "Get" {
			Get-CheckPointGroup -Session $Session -Name PesterGroup | Should BeOfType psCheckPoint.Objects.Group.CheckPointGroup
		}

		It "Get non-existing" {
			{ Get-CheckPointGroup -Session $Session -Name NotPesterGroup -ErrorAction Stop } | Should Throw
		}
	}

	Context "Set-CheckPointGroup" {
		It "Set" {
			Set-CheckPointGroup -Session $Session -Name PesterGroup -Color Red -PassThru | Should BeOfType psCheckPoint.Objects.Group.CheckPointGroup
		}

		It "Set non-existing" {
			{ Set-CheckPointGroup -Session $Session -Name NotPesterGroup -Color Red -ErrorAction Stop } | Should Throw
		}
	}

	Context "Get-CheckPointGroups" {
		It "Get" {
			$(Get-CheckPointGroups -Session $Session).Objects[0] | Should BeOfType psCheckPoint.Objects.CheckPointObject
		}

		It "Get full object" {
			$(Get-CheckPointGroups -Session $Session).Objects[0] | Get-CheckPointFullObject -Session $Session | Should BeOfType psCheckPoint.Objects.Group.CheckPointGroup
		}
	}

	Context "Remove-CheckPointGroup" {
		It "Remove" {
			{ Remove-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Not Throw
		}

		It "Remove non-existing" {
			{ Remove-CheckPointGroup -Session $Session -Name PesterGroup -ErrorAction Stop } | Should Throw
		}
	}
}