Describe "Export rule base" {
	
	It "Export" -Skip {
		$Export = $(Get-CheckPointObjects -Session $Session -Type simple-gateway).Name  | Get-CheckPointWhereUsed -Session $Session -ByName | Export-CheckPointObjects -Session $Session
		$Export | Should BeOfType psCheckPoint.Extra.Export.CheckPointExportSet
	}

	It "Convert to HTML" -Skip {
		$Export | ConvertTo-CheckPointHtml | Should BeOfType string
	}
}