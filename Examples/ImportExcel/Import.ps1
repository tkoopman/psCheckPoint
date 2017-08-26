#
# Import From Excel to Check Point
#
$Session = Open-CheckPointSession -SessionName ImportExcel -SessionDescription "Imported objects from Excel file" -NoCertificateValidation
$global:Groups = Import-Excel -WorkSheetname Groups   -Path .\Import.xlsx  | New-CheckPointGroup -Session $Session -Verbose
$global:Hosts = Import-Excel -WorkSheetname Hosts    -Path .\Import.xlsx  | New-CheckPointHost    -Session $Session -Verbose
$global:Networks = Import-Excel -WorkSheetname Networks -Path .\Import.xlsx  | New-CheckPointNetwork -Session $Session -Verbose

Close-CheckPointSession -Session $Session -ContinueSessionInSmartconsole