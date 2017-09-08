# psCheckPoint Example - Export to HTML
This example shows output from using the module psCheckPoint to export rules and all objects used by those rules to a HTML file.

##### Command run to produce this export.html file

```powershell
$(ForEach($rule in 1,2,3,4){$rule | Get-CheckPointAccessRule -Session $Session -Layer ExportNetwork}) |
  Export-CheckPointObjects -Session $Session -Verbose |
  ConvertTo-CheckPointHtml -Open
```

`$Session` is your current logged in session.