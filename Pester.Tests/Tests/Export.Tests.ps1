Describe "Export rule base" {
	$Export = $(Get-CheckPointObjects -Session $Session -Type simple-gateway).Name  | Get-CheckPointWhereUsed -Session $Session -ByName | Export-CheckPointObjects -Session $Session
	It "Export" {
		$Export | Should BeOfType psCheckPoint.Extra.Export.CheckPointExportSet
	}

	It "Convert to HTML" {
		$Export | ConvertTo-CheckPointHtml | Should BeOfType string
	}
}