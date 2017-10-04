# New-CheckPointHost

## SYNOPSIS
Create new object.

## SYNTAX

### IPv4 or IPv6
```
New-CheckPointHost -IPAddress <String> [-Groups <String[]>] [-SetIfExists] -Name <String> [-Tags <String[]>]
 [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors] [-PassThru] [-Color <String>]
 [-Session <CheckPointSession>]
```

### IPv4 & IPv6
```
New-CheckPointHost -IPv4Address <String> -IPv6Address <String> [-Groups <String[]>] [-SetIfExists]
 -Name <String> [-Tags <String[]>] [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors] [-PassThru]
 [-Color <String>] [-Session <CheckPointSession>]
```

### IPv4
```
New-CheckPointHost -IPv4Address <String> [-Groups <String[]>] [-SetIfExists] -Name <String> [-Tags <String[]>]
 [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors] [-PassThru] [-Color <String>]
 [-Session <CheckPointSession>]
```

### IPv6
```
New-CheckPointHost -IPv6Address <String> [-Groups <String[]>] [-SetIfExists] -Name <String> [-Tags <String[]>]
 [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors] [-PassThru] [-Color <String>]
 [-Session <CheckPointSession>]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
New-CheckPointHost -Name Test1 -ipAddress 1.2.3.4
```

## PARAMETERS

### -Color
Color of the object.
Should be one of existing colors.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Colour

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Comments
Comments string.

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
Collection of group identifiers.

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

### -IPAddress
IPv4 or IPv6 address.
If both addresses are required use ipv4-address and ipv6-address fields explicitly.

```yaml
Type: String
Parameter Sets: IPv4 or IPv6
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IPv4Address
IPv4 address.

```yaml
Type: String
Parameter Sets: IPv4 & IPv6, IPv4
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IPv6Address
IPv6 address.

```yaml
Type: String
Parameter Sets: IPv4 & IPv6, IPv6
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Name
Object name.
Should be unique in the domain.

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
Return the updated object.

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

### -SetIfExists
If another object with the same identifier already exists, it will be updated.
The command behaviour will be the same as if originally a set command was called.
Pay attention that original object's fields will be overwritten by the fields provided in the request payload!

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

### -Tags
Collection of tag identifiers.

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
IPv4 or IPv6 address.
If both addresses are required use ipv4-address and ipv6-address fields explicitly.

### System.String
IPv4 address.

### System.String
IPv6 address.

### System.String[]
Collection of group identifiers.

### System.String
Object name.
Should be unique in the domain.

### System.String[]
Collection of tag identifiers.

### System.String
Comments string.

### System.String
Color of the object.
Should be one of existing colors.

## OUTPUTS

### psCheckPoint.Objects.Host.CheckPointHost
Details of a Check Point Host

## NOTES

## RELATED LINKS

