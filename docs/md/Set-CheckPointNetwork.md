# Set-CheckPointNetwork

## SYNOPSIS
Edit existing object using object name or uid.

## SYNTAX

### By UID
```
Set-CheckPointNetwork [-Subnet <String>] [-Subnet4 <String>] [-Subnet6 <String>] [-MaskLength <Int32>]
 [-MaskLength4 <Int32>] [-MaskLength6 <Int32>] [-SubnetMask <String>] [-Broadcast <String>]
 [-GroupAction <MembershipActions>] [-Groups <String[]>] -UID <String> [-NewName <String>] [-Tags <String[]>]
 [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors] [-Color <String>] [-Session] <CheckPointSession>
```

### By Name
```
Set-CheckPointNetwork [-Subnet <String>] [-Subnet4 <String>] [-Subnet6 <String>] [-MaskLength <Int32>]
 [-MaskLength4 <Int32>] [-MaskLength6 <Int32>] [-SubnetMask <String>] [-Broadcast <String>]
 [-GroupAction <MembershipActions>] [-Groups <String[]>] [-Name] <String> [-NewName <String>]
 [-Tags <String[]>] [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors] [-Color <String>]
 [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
$cpNetwork = Set-CheckPointNetwork -Session $Session -Name Test1 -NewName Test2 -Tags TestTag
```

## PARAMETERS

### -Broadcast
Allow broadcast address inclusion.

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

### -GroupAction
Action to take with groups.

Possible values: Replace, Add, Remove

```yaml
Type: MembershipActions
Parameter Sets: (All)
Aliases: 
Accepted values: Replace, Add, Remove

Required: False
Position: Named
Default value: Replace
Accept pipeline input: False
Accept wildcard characters: False
```

### -Groups
Collection of group identifiers.

Groups listed will be either Added, Removed or replace the current list of group membership based on GroupAction parameter.

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

### -MaskLength
IPv4 or IPv6 network mask length.
If both masks are required use mask-length4 and mask-length6 fields explicitly.
Instead of IPv4 mask length it is possible to specify IPv4 mask itself in subnet-mask field.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 0
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -MaskLength4
IPv4 network mask length.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 0
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -MaskLength6
IPv6 network mask length.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 0
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

### -Subnet
IPv4 or IPv6 network address.
If both addresses are required use subnet4 and subnet6 fields explicitly.

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

### -Subnet4
IPv4 network address.

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

### -Subnet6
IPv6 network address.

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

### -SubnetMask
IPv4 network address.

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

### System.String
IPv4 or IPv6 network address.
If both addresses are required use subnet4 and subnet6 fields explicitly.

### System.String
IPv4 network address.

### System.String
IPv6 network address.

### System.Int32
IPv4 or IPv6 network mask length.
If both masks are required use mask-length4 and mask-length6 fields explicitly.
Instead of IPv4 mask length it is possible to specify IPv4 mask itself in subnet-mask field.

### System.Int32
IPv4 network mask length.

### System.Int32
IPv6 network mask length.

### System.String
IPv4 network address.

### System.String
Allow broadcast address inclusion.

### System.String[]
Collection of group identifiers.

Groups listed will be either Added, Removed or replace the current list of group membership based on GroupAction parameter.

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

### psCheckPoint.Objects.Network.CheckPointNetwork
Details of a Check Point Network

## NOTES

## RELATED LINKS

