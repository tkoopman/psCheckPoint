# Get-CheckPointAccessRule

## SYNOPSIS

## SYNTAX

### By UID
```
Get-CheckPointAccessRule -UID <String> [-Layer] <String> [-Session] <CheckPointSession>
```

### By Name
```
Get-CheckPointAccessRule [-Name] <String> [-Layer] <String> [-Session] <CheckPointSession>
```

### By Rule Number
```
Get-CheckPointAccessRule -RuleNumber <Int32> [-Layer] <String> [-Session] <CheckPointSession>
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Layer
Layer that the rule belongs to identified by the name or UID.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: 1
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
Position: 2
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -RuleNumber
Rule number.

```yaml
Type: Int32
Parameter Sets: By Rule Number
Aliases: 

Required: True
Position: Named
Default value: 0
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

### System.Int32
Rule number.

### System.String
Layer that the rule belongs to identified by the name or UID.

## OUTPUTS

### psCheckPoint.Objects.AccessRule.CheckPointAccessRule
Details of a Check Point Access Rule

## NOTES

## RELATED LINKS

