#
# SaveConfigs.ps1
#
# Make sure you already have an open session to management then run this to download configs from each simple gateway
#
Get-CheckPointSimpleGateways -All |
  ForEach-Object {
	  $(Invoke-CheckPointScript -ScriptName "Get Configuration" -Script "clish -c 'Show Configuration'" -Targets $_.Name | Wait-CheckPointTask).TaskDetails.ResponseMessage | Out-File "$($_.Name).conf"
  }