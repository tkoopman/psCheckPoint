# Remove-CheckPointMulticastAddressRange

## SYNOPSIS
Delete existing object using object name or uid.

## SYNTAX

### By UID
```
Remove-CheckPointMulticastAddressRange -UID <String> [-IgnoreWarnings] [-IgnoreErrors]
 [-Session] <CheckPointSession>
```

### By Name
```
Remove-CheckPointMulticastAddressRange [-Name] <String> [-IgnoreWarnings] [-IgnoreErrors]
 [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```

```

## PARAMETERS

### -IgnoreErrors
Apply changes ignoring errors.
You won't be able to publish such a changes.
If ignore-warnings flag was omitted - warnings will also be ignored.

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

### -IgnoreWarnings
Apply changes ignoring warnings.

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

Required: True
Position: 0
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
Object unique identifier.

### System.String
Object name.

## OUTPUTS

## NOTES

## RELATED LINKS

