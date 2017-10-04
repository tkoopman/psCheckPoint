# Get-CheckPointWhereUsed

## SYNOPSIS
Searches for usage of the target object in other objects and rules.

## SYNTAX

### By Object (Default)
```
Get-CheckPointWhereUsed -Object <CheckPointObject> [-Indirect] [-IndirectMaxDepth <Int32>]
 [-Session <CheckPointSession>]
```

### By UID
```
Get-CheckPointWhereUsed -UID <String> [-ByUID] [-Indirect] [-IndirectMaxDepth <Int32>]
 [-Session <CheckPointSession>]
```

### By Name
```
Get-CheckPointWhereUsed -Name <String> [-ByName] [-Indirect] [-IndirectMaxDepth <Int32>]
 [-Session <CheckPointSession>]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Get-CheckPointWhereUsed -Name http
```

## PARAMETERS

### -ByName
Force by name.
Used if pipelining in list of names.

```yaml
Type: SwitchParameter
Parameter Sets: By Name
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ByUID
Force by UID.
Used if pipelining in list of UIDs.

```yaml
Type: SwitchParameter
Parameter Sets: By UID
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Indirect
Search for indirect usage.

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

### -IndirectMaxDepth
Maximum nesting level during indirect usage search.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 5
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
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Object
Check Point Object.

```yaml
Type: CheckPointObject
Parameter Sets: By Object
Aliases: 

Required: True
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
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

## INPUTS

### System.String
Object unique identifier.

### System.String
Object name.

### psCheckPoint.Objects.CheckPointObject
Check Point Object.

## OUTPUTS

### psCheckPoint.Objects.Misc.CheckPointWhereUsed
Summary of Where Used results

## NOTES

## RELATED LINKS

