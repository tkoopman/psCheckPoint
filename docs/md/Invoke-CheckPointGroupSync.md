# Invoke-CheckPointGroupSync

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

```
Invoke-CheckPointGroupSync [-Color <Colors>] [-Comments <String>] [-CreateGroup] -GroupName <String>
 [-Ignore <Ignore>] -Input <String[]> -Prefix <String> [-Rename] [-Tags <String[]>] [-Session <Session>]
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

### -Color
{{Fill Color Description}}

```yaml
Type: Colors
Parameter Sets: (All)
Aliases: Colour
Accepted values: Aquamarine, Black, Blue, Brown, Burlywood, Coral, CreteBlue, Cyan, DarkBlue, DarkGold, DarkGray, DarkGreen, DarkOrange, DarkSeaGreen, Firebrick, ForestGreen, Gold, Gray, Khaki, LemonChiffon, LightGreen, Magenta, NavyBlue, Olive, Orange, Orchid, Pink, Purple, Red, SeaGreen, Sienna, SkyBlue, SlateBlue, Turquoise, VioletRed, Yellow

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Comments
{{Fill Comments Description}}

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

### -CreateGroup
{{Fill CreateGroup Description}}

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

### -GroupName
{{Fill GroupName Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: Name

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Ignore
{{Fill Ignore Description}}

```yaml
Type: Ignore
Parameter Sets: (All)
Aliases: 
Accepted values: No, Warnings, Errors

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Input
{{Fill Input Description}}

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: Subnets, IPAddresses

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Prefix
{{Fill Prefix Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Rename
{{Fill Rename Description}}

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

### -Tags
{{Fill Tags Description}}

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### System.String[]


## OUTPUTS

### psCheckPoint.Extra.Sync.InvokeCheckPointGroupSync+SyncOutput


## NOTES

## RELATED LINKS

