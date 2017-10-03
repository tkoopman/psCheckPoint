# Get-CheckPointIdentity

## SYNOPSIS
Queries the Identity Awareness associations of a given IP.

## SYNTAX

```
Get-CheckPointIdentity -IPAddress <String> -Gateway <String> -SharedSecret <String> [-BatchSize <Int32>]
 [-NoCertificateValidation]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Get-CheckPointIdentity -Gateway 192.168.1.1 -SharedSecret *** -NoCertificateValidation -IPAddress 192.168.1.2
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
Identity IP

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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

## INPUTS

### System.String
Identity IP

## OUTPUTS

### psCheckPoint.IA.GetIdentityResponse

## NOTES

## RELATED LINKS

