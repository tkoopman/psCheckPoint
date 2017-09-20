# Get-CheckPointSession

## SYNOPSIS
Retrieve existing object using object name or uid.

## SYNTAX

```
Get-CheckPointSession -UID <String> [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```

```

## PARAMETERS

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

### -UID
Session unique identifier.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

## INPUTS

### System.String
Session unique identifier.

## OUTPUTS

### psCheckPoint.Objects.Session.CheckPointSession
Details of a Check Point Session

This session object does NOT include login details so cannot be used as Session parameter in other commands

## NOTES

## RELATED LINKS

