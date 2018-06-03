# Get-CheckPointIdentity

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

```
Get-CheckPointIdentity -IPAddress <String> [-BatchSize <Int32>] [-CertificateHash <String>]
 [-CertificateValidation <CertificateValidation>] -Gateway <String> -SharedSecret <String>
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
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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

## INPUTS

### System.String


## OUTPUTS

### Koopman.CheckPoint.IA.ShowIdentityResponse


## NOTES

## RELATED LINKS

