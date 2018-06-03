# New-CheckPointNetwork

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### IPv4 or IPv6
```
New-CheckPointNetwork [-Broadcast <String>] [-Groups <String[]>] -MaskLength <Int32> -Subnet <IPAddress>
 [-SetIfExists] -Name <String> [-Color <Colors>] [-Comments <String>] [-Ignore <Ignore>] [-PassThru]
 [-Tags <String[]>] [-Session <Session>]
```

### IPv4 and IPv6
```
New-CheckPointNetwork [-Broadcast <String>] [-Groups <String[]>] -MaskLength4 <Int32> -MaskLength6 <Int32>
 -Subnet4 <IPAddress> -Subnet6 <IPAddress> [-SetIfExists] -Name <String> [-Color <Colors>] [-Comments <String>]
 [-Ignore <Ignore>] [-PassThru] [-Tags <String[]>] [-Session <Session>]
```

### IPv4
```
New-CheckPointNetwork [-Broadcast <String>] [-Groups <String[]>] -MaskLength4 <Int32> -Subnet4 <IPAddress>
 [-SetIfExists] -Name <String> [-Color <Colors>] [-Comments <String>] [-Ignore <Ignore>] [-PassThru]
 [-Tags <String[]>] [-Session <Session>]
```

### IPv4 and IPv6 with subnet mask
```
New-CheckPointNetwork [-Broadcast <String>] [-Groups <String[]>] -MaskLength6 <Int32> -Subnet4 <IPAddress>
 -Subnet6 <IPAddress> -SubnetMask <IPAddress> [-SetIfExists] -Name <String> [-Color <Colors>]
 [-Comments <String>] [-Ignore <Ignore>] [-PassThru] [-Tags <String[]>] [-Session <Session>]
```

### IPv6
```
New-CheckPointNetwork [-Broadcast <String>] [-Groups <String[]>] -MaskLength6 <Int32> -Subnet6 <IPAddress>
 [-SetIfExists] -Name <String> [-Color <Colors>] [-Comments <String>] [-Ignore <Ignore>] [-PassThru]
 [-Tags <String[]>] [-Session <Session>]
```

### IPv4 with subnet mask
```
New-CheckPointNetwork [-Broadcast <String>] [-Groups <String[]>] -Subnet <IPAddress> -SubnetMask <IPAddress>
 [-SetIfExists] -Name <String> [-Color <Colors>] [-Comments <String>] [-Ignore <Ignore>] [-PassThru]
 [-Tags <String[]>] [-Session <Session>]
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

### -Broadcast
{{Fill Broadcast Description}}

```yaml
Type: String
Parameter Sets: (All)
Aliases: 
Accepted values: disallow, allow

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

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
Accept pipeline input: True (ByPropertyName)
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
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Groups
{{Fill Groups Description}}

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
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

### -MaskLength
{{Fill MaskLength Description}}

```yaml
Type: Int32
Parameter Sets: IPv4 or IPv6
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -MaskLength4
{{Fill MaskLength4 Description}}

```yaml
Type: Int32
Parameter Sets: IPv4 and IPv6, IPv4
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -MaskLength6
{{Fill MaskLength6 Description}}

```yaml
Type: Int32
Parameter Sets: IPv4 and IPv6, IPv4 and IPv6 with subnet mask, IPv6
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Name
{{Fill Name Description}}

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

### -PassThru
{{Fill PassThru Description}}

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

### -SetIfExists
{{Fill SetIfExists Description}}

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

### -Subnet
{{Fill Subnet Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 or IPv6, IPv4 with subnet mask
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Subnet4
{{Fill Subnet4 Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 and IPv6, IPv4, IPv4 and IPv6 with subnet mask
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Subnet6
{{Fill Subnet6 Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 and IPv6, IPv4 and IPv6 with subnet mask, IPv6
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SubnetMask
{{Fill SubnetMask Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 and IPv6 with subnet mask, IPv4 with subnet mask
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
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
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

## INPUTS

### System.String
System.String[]
System.Int32
System.Net.IPAddress
Koopman.CheckPoint.Colors


## OUTPUTS

### Koopman.CheckPoint.Network


## NOTES

## RELATED LINKS

