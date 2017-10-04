# Close-CheckPointSession

## SYNOPSIS
Log out of a session.

## SYNTAX

```
Close-CheckPointSession [-ContinueSessionInSmartconsole] [-Session <CheckPointSession>]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Close-CheckPointSession
```

## PARAMETERS

### -ContinueSessionInSmartconsole
The session will be continued next time your open SmartConsole.
In case 'uid' is not provided, use current session.
In order for the session to pass successfully to SmartConsole, make sure you don't have any other active GUI sessions.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Session
Session object from Open-CheckPointSession

```yaml
Type: CheckPointSession
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

