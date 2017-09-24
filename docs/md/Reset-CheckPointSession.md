# Reset-CheckPointSession

## SYNOPSIS
Log out of a session.

## SYNTAX

```
Reset-CheckPointSession [-ResetSession <CheckPointSession>] [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Close-CheckPointSession -Session $Session
```

## PARAMETERS

### -ResetSession
Reset none active session

```yaml
Type: CheckPointSession
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Session
Session object from Open-CheckPointSession

```yaml
Type: CheckPointSession
Parameter Sets: (All)
Aliases: 

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### psCheckPoint.Objects.Session.CheckPointSession
Reset none active session

## OUTPUTS

## NOTES

## RELATED LINKS

