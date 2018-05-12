#
# Import From Excel to Check Point
#
Import-Excel -WorkSheetname Groups   -Path .\Import.xlsx  | New-CheckPointGroup -Verbose;
Import-Excel -WorkSheetname "Groups with Exclusion"   -Path .\Import.xlsx  | New-CheckPointGroupWithExclusion -Verbose;
Import-Excel -WorkSheetname Hosts    -Path .\Import.xlsx  | New-CheckPointHost -Verbose;
Import-Excel -WorkSheetname Networks -Path .\Import.xlsx  | New-CheckPointNetwork -Verbose;
Import-Excel -WorkSheetname "Address Ranges" -Path .\Import.xlsx  | New-CheckPointAddressRange -Verbose;
Import-Excel -WorkSheetname "Multicast Address Ranges" -Path .\Import.xlsx  | New-CheckPointMulticastAddressRange -Verbose;
Import-Excel -WorkSheetname "Security Zones"   -Path .\Import.xlsx  | New-CheckPointSecurityZone -Verbose;
Import-Excel -WorkSheetname "Access Layers"   -Path .\Import.xlsx  | New-CheckPointAccessLayer -Verbose;