#
# Import From Excel to Check Point
#
$Session = Open-CheckPointSession -SessionName ImportExcel -SessionDescription "Imported objects from Excel file" -NoCertificateValidation
$global:Groups = Import-Excel -WorkSheetname Groups   -Path .\Import.xlsx  | New-CheckPointGroup -Session $Session -Verbose
$global:GroupsWithExclusion = Import-Excel -WorkSheetname "Groups with Exclusion"   -Path .\Import.xlsx  | New-CheckPointGroupWithExclusion -Session $Session -Verbose
$global:Hosts = Import-Excel -WorkSheetname Hosts    -Path .\Import.xlsx  | New-CheckPointHost    -Session $Session -Verbose
$global:Networks = Import-Excel -WorkSheetname Networks -Path .\Import.xlsx  | New-CheckPointNetwork -Session $Session -Verbose
$global:AddressRanges = Import-Excel -WorkSheetname "Address Ranges" -Path .\Import.xlsx  | New-CheckPointAddressRange -Session $Session -Verbose
$global:MulticastAddressRanges = Import-Excel -WorkSheetname "Multicast Address Ranges" -Path .\Import.xlsx  | New-CheckPointMulticastAddressRange -Session $Session -Verbose
$global:SecurityZones = Import-Excel -WorkSheetname "Security Zones"   -Path .\Import.xlsx  | New-CheckPointSecurityZone -Session $Session -Verbose
$global:AccessLayers = Import-Excel -WorkSheetname "Access Layers"   -Path .\Import.xlsx  | New-CheckPointAccessLayer -Session $Session -Verbose

Close-CheckPointSession -Session $Session -ContinueSessionInSmartconsole