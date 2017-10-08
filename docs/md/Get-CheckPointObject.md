# Get-CheckPointObject

## SYNOPSIS
Get object by UID.

## SYNTAX

```
Get-CheckPointObject -UID <String> [-Session <CheckPointSession>]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Get-CheckPointObject -UID "12345678-1234-1234-1234-123456789abc" | Get-CheckPointFullObject
```

## PARAMETERS

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

### -UID
Object unique identifier.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

## INPUTS

### System.String
Object unique identifier.

## OUTPUTS

### psCheckPoint.Objects.CheckPointObject
Base summary details of Check Point Objects

## NOTES

## RELATED LINKS

