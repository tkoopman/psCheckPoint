# Add-CheckPointIdentity

## SYNOPSIS
Creates a new Identity Awareness association for a specified IP address.

## SYNTAX

```
Add-CheckPointIdentity -IPAddress <String> [-User <String>] [-Machine <String>] [-Domain <String>]
 [-SessionTimeout <Int32>] [-NoFetchUserGroups] [-NoFetchMachineGroups] [-UserGroups <String[]>]
 [-MachineGroups <String[]>] [-NoCalculateRoles] [-Roles <String[]>] [-MachineOS <String>] [-HostType <String>]
 -Gateway <String> -SharedSecret <String> [-BatchSize <Int32>] [-NoCertificateValidation]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Add-CheckPointIdentity -Gateway 192.168.1.1 -SharedSecret *** -NoCertificateValidation -IPAddress 192.168.1.2 -NoFetchUserGroups -NoFetchMachineGroups -NoCalculateRoles -User "Test User" -Machine "Test Machine" -Roles "Test Role"
```

## PARAMETERS

### -BatchSize
When using pipeline to send multiple requests at once, how many to batch together and send as single request.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 10
Accept pipeline input: False
Accept wildcard characters: False
```

### -Domain
Domain name

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

### -Gateway
IP or Hostname of the gateway server.

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

### -HostType
Type of host device.
For example: Apple iOS device.

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

### -IPAddress
Association IP.
Supports either IPv4 or IPv6, but not both.

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

### -Machine
Computer name

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

### -MachineGroups
List of groups to which the computer belongs(when Identity Awareness does not fetch computer groups).

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

### -MachineOS
Host operating system.
For example: Windows 7.

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

### -NoCalculateRoles
Defines whether Identity Awareness calculates the identity's access roles.

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

### -NoCertificateValidation
Do NOT verify server's certificate.

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

### -NoFetchMachineGroups
Defines whether Identity Awareness fetches the machine's groups from the user directories defined in SmartConsole.

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

### -NoFetchUserGroups
Defines whether Identity Awareness fetches the user's groups from the user directories defined in SmartConsole.

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

### -Roles
List of roles to assign to this identity (when Identity Awareness does not calculate roles).

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

### -SessionTimeout
Timeout (in seconds) for this Identity Awareness association.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 43200
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SharedSecret
Shared secret.

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

### -User
User name

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

### -UserGroups
List of groups to which the user belongs (when Identity Awareness does not fetch user groups).

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
Association IP.
Supports either IPv4 or IPv6, but not both.

### System.String
User name

### System.String
Computer name

### System.String
Domain name

### System.Int32
Timeout (in seconds) for this Identity Awareness association.

### System.String[]
List of groups to which the user belongs (when Identity Awareness does not fetch user groups).

### System.String[]
List of groups to which the computer belongs(when Identity Awareness does not fetch computer groups).

### System.String[]
List of roles to assign to this identity (when Identity Awareness does not calculate roles).

### System.String
Host operating system.
For example: Windows 7.

### System.String
Type of host device.
For example: Apple iOS device.

## OUTPUTS

### psCheckPoint.IA.AddIdentityResponse

## NOTES

## RELATED LINKS

