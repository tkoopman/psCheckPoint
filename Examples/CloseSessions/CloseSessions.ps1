#
# CloseSessions.ps1
#
(Get-CheckPointSessions).Sessions | where {$_.IsExpiredSession -and $_.Changes -eq 0} | Reset-CheckPointSession