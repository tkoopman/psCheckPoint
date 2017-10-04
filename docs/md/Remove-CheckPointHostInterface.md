# Remove-CheckPointHostInterface

## SYNOPSIS
Remove host interface.

## SYNTAX

```
Remove-CheckPointHostInterface [-Host] <PSObject> [-Name] <String> [-Session <CheckPointSession>]
```

## DESCRIPTION

## EXAMPLES

### Example 1
```
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

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

## INPUTS

### System.Management.Automation.PSObject
Host object (Name, UID or Host Object)

## OUTPUTS

### psCheckPoint.Objects.Host.CheckPointHost
Details of a Check Point Host

## NOTES

## RELATED LINKS

