# New-CheckPointServiceTCP

## SYNOPSIS
Create new object.

## SYNTAX

```
New-CheckPointServiceTCP [-KeepConnectionsOpenAfterPolicyInstallation] [-MatchByProtocolSignature]
 [-MatchForAny] [-OverrideDefaultSettings] [-Port <String>] [-Protocol <String>] [-SessionTimeout <Int32>]
 [-SourcePort <String>] [-SyncConnectionsOnCluster] [-UseDefaultSessionTimeout] [-Groups <String[]>]
 [-SetIfExists] -Name <String> [-Tags <String[]>] [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors]
 [-Color <String>] [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```

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

### -KeepConnectionsOpenAfterPolicyInstallation
Keep connections open after policy has been installed even if they are not allowed under the new policy.
This overrides the settings in the Connection Persistence page.
If you change this property, the change will not affect open connections, but only future connections.

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

### -MatchByProtocolSignature
A value of true enables matching by the selected protocol's signature - the signature identifies the protocol as genuine.
Select this option to limit the port to the specified protocol.
If the selected protocol does not support matching by signature, this field cannot be set to true.

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

### -MatchForAny
Indicates whether this service is used when 'Any' is set as the rule's service and there are several service objects with the same source port and protocol.

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

### -OverrideDefaultSettings
Indicates whether this service is a Data Domain service which has been overridden.

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

### -Port
The number of the port used to provide this service.
To specify a port range, place a hyphen between the lowest and highest port numbers, for example 44-55.

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

### -Protocol
Select the protocol type associated with the service, and by implication, the management server (if any) that enforces Content Security and Authentication for the service.
Selecting a Protocol Type invokes the specific protocol handlers for each protocol type, thus enabling higher level of security by parsing the protocol, and higher level of connectivity by tracking dynamic actions (such as opening of ports).

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

### -SessionTimeout
Time (in seconds) before the session times out.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 0
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

### -SourcePort
Port number for the client side service.
If specified, only those Source port Numbers will be Accepted, Dropped, or Rejected during packet inspection.
Otherwise, the source port is not inspected.

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

### -SyncConnectionsOnCluster
Enables state-synchronized High Availability or Load Sharing on a ClusterXL or OPSEC-certified cluster.

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

### -UseDefaultSessionTimeout
Use default virtual session timeout.

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

## INPUTS

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

### psCheckPoint.Objects.ServiceTCP.CheckPointServiceTCP
Details of a Check Point TCP Service

## NOTES

## RELATED LINKS

