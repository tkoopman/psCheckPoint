# Get-CheckPointObjects

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### Limit + Filter (Default)
```
Get-CheckPointObjects [-DetailsLevel <DetailLevels>] [-Filter <String>] [-IPOnly] [-Limit <Int32>]
 [-Offset <Int32>] [-Type <String>] [-Session <Session>]
```

### All + Filter
```
Get-CheckPointObjects [-All] [-DetailsLevel <DetailLevels>] [-Filter <String>] [-IPOnly] [-Limit <Int32>]
 [-Type <String>] [-Session <Session>]
```

### All + Unused
```
Get-CheckPointObjects [-All] [-DetailsLevel <DetailLevels>] [-Limit <Int32>] [-Unused] [-Session <Session>]
```

### Limit + Unused
```
Get-CheckPointObjects [-DetailsLevel <DetailLevels>] [-Limit <Int32>] [-Offset <Int32>] [-Unused]
 [-Session <Session>]
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

### -All
{{Fill All Description}}

```yaml
Type: SwitchParameter
Parameter Sets: All + Filter, All + Unused
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DetailsLevel
{{Fill DetailsLevel Description}}

```yaml
Type: DetailLevels
Parameter Sets: (All)
Aliases: 
Accepted values: UID, Standard, Full

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filter
{{Fill Filter Description}}

```yaml
Type: String
Parameter Sets: Limit + Filter, All + Filter
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IPOnly
{{Fill IPOnly Description}}

```yaml
Type: SwitchParameter
Parameter Sets: Limit + Filter, All + Filter
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Limit
{{Fill Limit Description}}

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Offset
{{Fill Offset Description}}

```yaml
Type: Int32
Parameter Sets: Limit + Filter, Limit + Unused
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

### -Type
{{Fill Type Description}}

```yaml
Type: String
Parameter Sets: Limit + Filter, All + Filter
Aliases: 
Accepted values: object, host, network, group, address-range, multicast-address-range, group-with-exclusion, simple-gateway, security-zone, time, time-group, access-role, dynamic-object, trusted-client, tag, dns-domain, opsec-application, service-tcp, service-udp, service-icmp, service-icmp6, service-sctp, service-other, service-group

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Unused
{{Fill Unused Description}}

```yaml
Type: SwitchParameter
Parameter Sets: All + Unused, Limit + Unused
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### None


## OUTPUTS

### Koopman.CheckPoint.IObjectSummary
Koopman.CheckPoint.Common.NetworkObjectsPagingResults`1[[Koopman.CheckPoint.IObjectSummary, CheckPoint.NET, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null]]


## NOTES

## RELATED LINKS

