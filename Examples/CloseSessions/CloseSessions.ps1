#
# CloseSessions.ps1
#
(Get-CheckPointSessions).Objects | where {$_.ExpiredSession -and $_.Changes -eq 0} | Reset-CheckPointSession