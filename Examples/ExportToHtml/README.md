# psCheckPoint Example - Export to HTML
This example shows output from using the module psCheckPoint to export rules and all objects used by those rules to a HTML file.

##### Command run to produce this [export.html](http://htmlpreview.github.io/?https://github.com/tkoopman/psCheckPoint/blob/master/Examples/ExportToHtml/Export.html) file

```powershell
$(ForEach($rule in 1,2,3,4){$rule | Get-CheckPointAccessRule -Layer ExportNetwork}) |
  Export-CheckPointObjects -Verbose |
  ConvertTo-CheckPointHtml -Open
```