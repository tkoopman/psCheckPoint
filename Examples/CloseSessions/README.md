# psCheckPoint Example - Close Sessions
If like me, you "sometimes" forget to reset & close sessions, this one liner will close all disconnected sessions that have no pending changes.

```powershell
(Get-CheckPointSessions).Sessions | 
   where {$_.ExpiredSession -and $_.Changes -eq 0} | 
   Reset-CheckPointSession
```
