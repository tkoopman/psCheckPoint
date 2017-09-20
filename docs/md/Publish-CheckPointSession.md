# Publish-CheckPointSession

## SYNOPSIS
All the changes done by this user will be seen by all users only after publish is called.

## SYNTAX

```
Publish-CheckPointSession [-PublishSession <CheckPointSession>] [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Publish-CheckPointSession -Session $Session
```

## PARAMETERS

### -PublishSession
Publish none active session

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
Publish none active session

## OUTPUTS

## NOTES

## RELATED LINKS

