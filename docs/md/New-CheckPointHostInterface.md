# New-CheckPointHostInterface

## SYNOPSIS
Create new host interface.

## SYNTAX

```
New-CheckPointHostInterface [-Host] <PSObject> [-Name] <String> [-Subnet4 <String>] [-Subnet6 <String>]
 [-MaskLength4 <Int32>] [-MaskLength6 <Int32>] [-Color <String>] [-Session <CheckPointSession>]
```

## DESCRIPTION

## EXAMPLES

### Example 1
```
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

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
Accept pipeline input: False
Accept wildcard characters: False
```

### -Host
Host object (Name, UID or Host Object)

```yaml
Type: PSObject
Parameter Sets: (All)
Aliases: 

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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
Accept pipeline input: False
Accept wildcard characters: False
```

### -MaskLength6
IPv4 network mask length.

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

### -Name
Interface name.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: 2
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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
Accept pipeline input: False
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
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### System.Management.Automation.PSObject
Host object (Name, UID or Host Object)

## OUTPUTS

### psCheckPoint.Objects.Host.CheckPointHost
Details of a Check Point Host

## NOTES

## RELATED LINKS

