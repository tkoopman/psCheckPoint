# Get-CheckPointAccessRuleBase

## SYNOPSIS
Shows the entire Access Rules layer.

## SYNTAX

### By UID
```
Get-CheckPointAccessRuleBase -UID <String> [-Filter <String>] [-Session <CheckPointSession>]
```

### By Name
```
Get-CheckPointAccessRuleBase [-Name] <String> [-Filter <String>] [-Session <CheckPointSession>]
```

## DESCRIPTION
Shows the entire Access Rules layer.
This layer is divided into sections.
An Access Rule may be within a section, or independent of a section (in which case it is said to be under the "global" section).
The reply features a list of objects.
Each object may be a section of the layer, with all its rules in, or a rule itself, for the case of rules which are under the global section.
An optional "filter" field may be added in order to filter out only those rules that match a search criteria.

## EXAMPLES

### Example 1
```
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Filter
Search expression to filter the rulebase.
The provided text should be exactly the same as it would be given in Smart Console.
The logical operators in the expression ('AND', 'OR') should be provided in capital letters.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
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
Position: 2
Default value: None
Accept pipeline input: True (ByPropertyName)
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
Object unique identifier.

### System.String
Object name.

## OUTPUTS

### psCheckPoint.Objects.AccessRule.CheckPointAccessRule
Details of a Check Point Access Rule

## NOTES

## RELATED LINKS

