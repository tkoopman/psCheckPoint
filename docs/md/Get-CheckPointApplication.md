# Get-CheckPointApplication

## SYNOPSIS
{{Fill in the Synopsis}}

## SYNTAX

### By Name or UID (Default)
```
Get-CheckPointApplication [-DetailsLevel <DetailLevels>] [-Value] <String> [-Session <Session>]
```

### By Application ID
```
Get-CheckPointApplication -ApplicationID <Int32> [-DetailsLevel <DetailLevels>] [-Session <Session>]
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

### -ApplicationID
{{Fill ApplicationID Description}}

```yaml
Type: Int32
Parameter Sets: By Application ID
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
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

### -Value
{{Fill Value Description}}

```yaml
Type: String
Parameter Sets: By Name or UID
Aliases: Name, UID

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

## INPUTS

### System.Int32
System.String


## OUTPUTS

### Koopman.CheckPoint.ApplicationSite


## NOTES

## RELATED LINKS

