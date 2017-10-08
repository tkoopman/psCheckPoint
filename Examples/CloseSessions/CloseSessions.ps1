#
# CloseSessions.ps1
#
(Get-CheckPointSessions).Sessions | where {$_.ExpiredSession -and $_.Changes -eq 0} | Reset-CheckPointSession