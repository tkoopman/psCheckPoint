# Get-CheckPointWhereUsed

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### By Object (Default)
```
Get-CheckPointWhereUsed [-DetailsLevel <DetailLevels>] [-Indirect] [-IndirectMaxDepth <Int32>]
 -Object <IObjectSummary> [-Session <Session>]
```

### By Name or UID
```
Get-CheckPointWhereUsed [-DetailsLevel <DetailLevels>] [-Indirect] [-IndirectMaxDepth <Int32>]
 [-Value] <String> [-Session <Session>]
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

### -DetailsLevel
{{Fill DetailsLevel Description}}

```yaml
Type: DetailLevels
Parameter Sets: (All)
Aliases: 
Accepted values: UID, Standard, Full

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Indirect
{{Fill Indirect Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IndirectMaxDepth
{{Fill IndirectMaxDepth Description}}

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Object
{{Fill Object Description}}

```yaml
Type: IObjectSummary
Parameter Sets: By Object
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Session
{{Fill Session Description}}

```yaml
Type: Session
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Value
{{Fill Value Description}}

```yaml
Type: String
Parameter Sets: By Name or UID
Aliases: Name, UID

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

## INPUTS

### Koopman.CheckPoint.IObjectSummary
System.String


## OUTPUTS

### Koopman.CheckPoint.Common.WhereUsed


## NOTES

## RELATED LINKS

