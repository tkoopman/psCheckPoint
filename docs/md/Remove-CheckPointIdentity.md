# Remove-CheckPointIdentity

## SYNOPSIS
Queries the Identity Awareness associations of a given IP.

## SYNTAX

### ip
```
Remove-CheckPointIdentity -IPAddress <String> [-ClientType <String>] -Gateway <String> -SharedSecret <String>
 [-BatchSize <Int32>] [-NoCertificateValidation]
```

### mask
```
Remove-CheckPointIdentity -Subnet <String> -SubnetMask <String> [-ClientType <String>] -Gateway <String>
 -SharedSecret <String> [-BatchSize <Int32>] [-NoCertificateValidation]
```

### range
```
Remove-CheckPointIdentity -IPAddressFirst <String> -IPAddressLast <String> [-ClientType <String>]
 -Gateway <String> -SharedSecret <String> [-BatchSize <Int32>] [-NoCertificateValidation]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Remove-CheckPointIdentity -Gateway 192.168.1.1 -SharedSecret *** -NoCertificateValidation -IPAddress 192.168.1.2
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

### -ClientType
Deletes only associations created by the specified identity source.If no value is set for the client-type parameter, or if it is set to any, the gateway deletes all identities associated with the given IP(or IPs)

Note - When theclient-type is set to vpn(remote access), the gateway deletes all the identities associated with the given IP(or IPs).
This is because when you delete an identity associated with an office mode IP, this usually means that this office mode IP is no longer valid.

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

### -IPAddress
Association IP.
Required when you revoke a single IP.

```yaml
Type: String
Parameter Sets: ip
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -IPAddressFirst
First IP in the range.Required when the revoke method is range.

```yaml
Type: String
Parameter Sets: range
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IPAddressLast
Last IP in the range.
Required when the revoke method is range.

```yaml
Type: String
Parameter Sets: range
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
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

### -Subnet
Subnet.
Required when the revoke method is mask.

```yaml
Type: String
Parameter Sets: mask
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -SubnetMask
Subnet mask.
Required when the revoke method is mask.

```yaml
Type: String
Parameter Sets: mask
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

## INPUTS

### System.String
Association IP.
Required when you revoke a single IP.

### System.String
Subnet.
Required when the revoke method is mask.

### System.String
Subnet mask.
Required when the revoke method is mask.

### System.String
First IP in the range.Required when the revoke method is range.

### System.String
Last IP in the range.
Required when the revoke method is range.

### System.String
Deletes only associations created by the specified identity source.If no value is set for the client-type parameter, or if it is set to any, the gateway deletes all identities associated with the given IP(or IPs)

Note - When theclient-type is set to vpn(remote access), the gateway deletes all the identities associated with the given IP(or IPs).
This is because when you delete an identity associated with an office mode IP, this usually means that this office mode IP is no longer valid.

## OUTPUTS

### psCheckPoint.IA.RemoveIdentityResponse

## NOTES

## RELATED LINKS

