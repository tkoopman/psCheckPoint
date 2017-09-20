# Set-CheckPointAccessLayer

## SYNOPSIS
Edit existing object using object name or uid.

## SYNTAX

### By UID
```
Set-CheckPointAccessLayer [-ApplicationsAndUrlFiltering <Boolean>] [-ContentAwareness <Boolean>]
 [-DetectUsingXForwardFor <Boolean>] [-Firewall <Boolean>] [-MobileAccess <Boolean>] [-Shared <Boolean>]
 -UID <String> [-NewName <String>] [-Tags <String[]>] [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors]
 [-Color <String>] [-Session] <CheckPointSession>
```

### By Name
```
Set-CheckPointAccessLayer [-ApplicationsAndUrlFiltering <Boolean>] [-ContentAwareness <Boolean>]
 [-DetectUsingXForwardFor <Boolean>] [-Firewall <Boolean>] [-MobileAccess <Boolean>] [-Shared <Boolean>]
 [-Name] <String> [-NewName <String>] [-Tags <String[]>] [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors]
 [-Color <String>] [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```

```

## PARAMETERS

### -ApplicationsAndUrlFiltering
Whether to enable Applications and URL Filtering blade on the layer.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

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

### -ContentAwareness
Whether to enable Content Awareness blade on the layer.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -DetectUsingXForwardFor
Whether to use X-Forward-For HTTP header, which is added by the proxy server to keep track of the original source IP.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Firewall
Whether to enable Firewall blade on the layer.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: True
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

### -MobileAccess
Whether to enable Mobile Access blade on the layer.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: False
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
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -NewName
New name of the object.

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

### -Shared
Whether this layer is shared.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: True (ByPropertyName)
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

### System.Boolean
Whether to enable Applications and URL Filtering blade on the layer.

### System.Boolean
Whether to enable Content Awareness blade on the layer.

### System.Boolean
Whether to use X-Forward-For HTTP header, which is added by the proxy server to keep track of the original source IP.

### System.Boolean
Whether to enable Firewall blade on the layer.

### System.Boolean
Whether to enable Mobile Access blade on the layer.

### System.Boolean
Whether this layer is shared.

### System.String
Object unique identifier.

### System.String
Object name.

### System.String
New name of the object.

### System.String[]
Collection of tag identifiers.

### System.String
Comments string.

### System.String
Color of the object.
Should be one of existing colors.

## OUTPUTS

### psCheckPoint.Objects.AccessLayer.CheckPointAccessLayer
Details of a Check Point Access Layer

## NOTES

## RELATED LINKS

