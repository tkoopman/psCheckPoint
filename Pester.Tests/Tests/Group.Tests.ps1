Describe "Basic.Group" {
	Context "New-CheckPointGroup" {
		It "Add" {
			New-CheckPointGroup -Session $Session -Name PesterGroup | Should BeOfType psCheckPoint.Objects.Group.CheckPointGroup
		}

		It "Add duplicate" {
			New-CheckPointGroup -Session $Session -Name PesterGroup | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Get-CheckPointGroup" {
		It "Get" {
			Get-CheckPointGroup -Session $Session -Name PesterGroup | Should BeOfType psCheckPoint.Objects.Group.CheckPointGroup
		}

		It "Get non-existing" {
			Get-CheckPointGroup -Session $Session -Name NotPesterGroup | Should BeOfType psCheckPoint.CheckPointError
		}
	}

	Context "Set-CheckPointGroup" {
		It "Set" {
			Set-CheckPointGroup -Session $Session -Name PesterGroup -Color Red | Should BeOfType psCheckPoint.Objects.Group.CheckPointGroup
		}

		It "Set non-existing" {
			Set-CheckPointGroup -Session $Session -Name NotPesterGroup -Color Red | Should BeOfType psCheckPoint.CheckPointError
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
			Remove-CheckPointGroup -Session $Session -Name PesterGroup | Should Not BeOfType psCheckPoint.CheckPointError
		}

		It "Remove non-existing" {
			Remove-CheckPointGroup -Session $Session -Name PesterGroup | Should BeOfType psCheckPoint.CheckPointError
		}
	}
}