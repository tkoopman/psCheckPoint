# Get-CheckPointApplication

## SYNOPSIS
Retrieve existing object using object name or uid.

## SYNTAX

### By Application ID
```
Get-CheckPointApplication -ApplicationID <String> [-Session <CheckPointSession>]
```

### By UID
```
Get-CheckPointApplication -UID <String> [-Session <CheckPointSession>]
```

### By Name
```
Get-CheckPointApplication [-Name] <String> [-Session <CheckPointSession>]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```

```

## PARAMETERS

### -ApplicationID
Object application identifier.

```yaml
Type: String
Parameter Sets: By Application ID
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Name
Object name.

```yaml
Type: String
Parameter Sets: By Name
Aliases: 

Required: True
Position: 1
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
Parameter Sets: By UID
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

## INPUTS

### System.String
Object application identifier.

### System.String
Object unique identifier.

### System.String
Object name.

## OUTPUTS

### psCheckPoint.Objects.Application.CheckPointApplication
Details of a Check Point Application/Site

## NOTES

## RELATED LINKS

