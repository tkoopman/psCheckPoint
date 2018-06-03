# Add-CheckPointIdentity

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

```
Add-CheckPointIdentity [-Domain <String>] [-HostType <String>] -IPAddress <String> [-Machine <String>]
 [-MachineGroups <String[]>] [-MachineOS <String>] [-NoCalculateRoles] [-NoFetchMachineGroups]
 [-NoFetchUserGroups] [-Roles <String[]>] [-SessionTimeout <Int32>] [-User <String>] [-UserGroups <String[]>]
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

### -Domain
{{Fill Domain Description}}

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

### -HostType
{{Fill HostType Description}}

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
{{Fill IPAddress Description}}

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
{{Fill Machine Description}}

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
{{Fill MachineGroups Description}}

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
{{Fill MachineOS Description}}

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
{{Fill NoCalculateRoles Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoFetchMachineGroups
{{Fill NoFetchMachineGroups Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoFetchUserGroups
{{Fill NoFetchUserGroups Description}}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Roles
{{Fill Roles Description}}

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
{{Fill SessionTimeout Description}}

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
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

### -User
{{Fill User Description}}

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
{{Fill UserGroups Description}}

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
System.String[]
System.Int32


## OUTPUTS

### Koopman.CheckPoint.IA.AddIdentityResponse


## NOTES

## RELATED LINKS

