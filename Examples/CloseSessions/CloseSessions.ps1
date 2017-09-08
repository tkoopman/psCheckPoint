#
# CloseSessions.ps1
#
(Get-CheckPointSessions -Session $Session).Objects | where {$_.ExpiredSession -and $_.Changes -eq 0} | Reset-CheckPointSession -Session $Session