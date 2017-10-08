#
# SaveConfigs.ps1
#
$(Get-CheckPointSimpleGateways).Objects | 
  ForEach-Object {
	  $(Invoke-CheckPointScript -ScriptName "Get Configuration" -Script "clish -c 'Show Configuration'" -Targets $_.Name | Wait-CheckPointTask).TaskDetails.ResponseMessage | Out-File "$($_.Name).conf"
  }