#
# CloseSessions.ps1
#
Get-CheckPointSessions -All | where {$_.IsExpiredSession -and $_.Changes -eq 0} | Reset-CheckPointSession