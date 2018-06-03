# Set-CheckPointAddressRange

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### IPv4 and IPv6 (Default)
```
Set-CheckPointAddressRange -AddressRange <PSObject> [-GroupAction <MembershipActions>] [-Groups <String[]>]
 [-IPv4AddressFirst <IPAddress>] [-IPv4AddressLast <IPAddress>] [-IPv6AddressFirst <IPAddress>]
 [-IPv6AddressLast <IPAddress>] [-NewName <String>] [-TagAction <MembershipActions>] [-Color <Colors>]
 [-Comments <String>] [-Ignore <Ignore>] [-PassThru] [-Tags <String[]>] [-Session <Session>]
```

### IPv4 or IPv6
```
Set-CheckPointAddressRange -AddressRange <PSObject> [-GroupAction <MembershipActions>] [-Groups <String[]>]
 [-IPAddressFirst <IPAddress>] [-IPAddressLast <IPAddress>] [-NewName <String>]
 [-TagAction <MembershipActions>] [-Color <Colors>] [-Comments <String>] [-Ignore <Ignore>] [-PassThru]
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

### -AddressRange
{{Fill AddressRange Description}}

```yaml
Type: PSObject
Parameter Sets: (All)
Aliases: Name, UID

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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

### -GroupAction
{{Fill GroupAction Description}}

```yaml
Type: MembershipActions
Parameter Sets: (All)
Aliases: 
Accepted values: Replace, Add, Remove

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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

### -IPAddressFirst
{{Fill IPAddressFirst Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 or IPv6
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IPAddressLast
{{Fill IPAddressLast Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 or IPv6
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IPv4AddressFirst
{{Fill IPv4AddressFirst Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 and IPv6
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IPv4AddressLast
{{Fill IPv4AddressLast Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 and IPv6
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IPv6AddressFirst
{{Fill IPv6AddressFirst Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 and IPv6
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IPv6AddressLast
{{Fill IPv6AddressLast Description}}

```yaml
Type: IPAddress
Parameter Sets: IPv4 and IPv6
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -NewName
{{Fill NewName Description}}

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

### -TagAction
{{Fill TagAction Description}}

```yaml
Type: MembershipActions
Parameter Sets: (All)
Aliases: 
Accepted values: Replace, Add, Remove

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
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

## INPUTS

### System.Management.Automation.PSObject
System.String[]
System.Net.IPAddress
System.String
Koopman.CheckPoint.Colors


## OUTPUTS

### Koopman.CheckPoint.AddressRange


## NOTES

## RELATED LINKS

