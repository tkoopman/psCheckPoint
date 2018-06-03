# Get-CheckPointGroups

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### All
```
Get-CheckPointGroups [-All] [-DetailsLevel <DetailLevels>] [-Limit <Int32>] [-Session <Session>]
```

### Limit
```
Get-CheckPointGroups [-DetailsLevel <DetailLevels>] [-Limit <Int32>] [-Offset <Int32>] [-Session <Session>]
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

### -All
{{Fill All Description}}

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Limit
{{Fill Limit Description}}

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

### -Offset
{{Fill Offset Description}}

```yaml
Type: Int32
Parameter Sets: Limit
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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

## INPUTS

### None


## OUTPUTS

### Koopman.CheckPoint.Common.NetworkObjectsPagingResults`1[[Koopman.CheckPoint.Group, CheckPoint.NET, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null]]
Koopman.CheckPoint.Group[]


## NOTES

## RELATED LINKS

