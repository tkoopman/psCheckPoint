# Remove-CheckPointIdentity

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### ip
```
Remove-CheckPointIdentity [-ClientType <ClientTypes>] -IPAddress <String> [-BatchSize <Int32>]
 [-CertificateHash <String>] [-CertificateValidation <CertificateValidation>] -Gateway <String>
 -SharedSecret <String>
```

### range
```
Remove-CheckPointIdentity [-ClientType <ClientTypes>] -IPAddressFirst <String> -IPAddressLast <String>
 [-BatchSize <Int32>] [-CertificateHash <String>] [-CertificateValidation <CertificateValidation>]
 -Gateway <String> -SharedSecret <String>
```

### mask
```
Remove-CheckPointIdentity [-ClientType <ClientTypes>] -Subnet <String> -SubnetMask <String>
 [-BatchSize <Int32>] [-CertificateHash <String>] [-CertificateValidation <CertificateValidation>]
 -Gateway <String> -SharedSecret <String>
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

### -BatchSize
{{Fill BatchSize Description}}

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

### -CertificateHash
{{Fill CertificateHash Description}}

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

### -CertificateValidation
{{Fill CertificateValidation Description}}

```yaml
Type: CertificateValidation
Parameter Sets: (All)
Aliases: 
Accepted values: None, ValidCertificate, CertificatePinning, All, Auto

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientType
{{Fill ClientType Description}}

```yaml
Type: ClientTypes
Parameter Sets: (All)
Aliases: 
Accepted values: Any, CaptivePortal, IdaAgent, VPN, AdQuery, MultihostAgent, Radius, IdaApi, IdentityCollector

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Gateway
{{Fill Gateway Description}}

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
{{Fill IPAddress Description}}

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
{{Fill IPAddressFirst Description}}

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
{{Fill IPAddressLast Description}}

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

### -SharedSecret
{{Fill SharedSecret Description}}

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
{{Fill Subnet Description}}

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
{{Fill SubnetMask Description}}

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

### Koopman.CheckPoint.IA.ClientTypes
System.String


## OUTPUTS

### Koopman.CheckPoint.IA.DeleteIdentityResponse


## NOTES

## RELATED LINKS

