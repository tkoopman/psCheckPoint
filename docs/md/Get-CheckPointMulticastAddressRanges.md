# Get-CheckPointMulticastAddressRanges

## SYNOPSIS
Retrieve all objects.

## SYNTAX

```
Get-CheckPointMulticastAddressRanges [-UID <Int32>] [-Name <Int32>] [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```

```

## PARAMETERS

### -Name
Skip that many results before beginning to return them.

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

### -Session
Session object from Open-CheckPointSession

```yaml
Type: CheckPointSession
Parameter Sets: (All)
Aliases: 

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UID
No more than that many results will be returned.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 50
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

## OUTPUTS

### psCheckPoint.Objects.CheckPointObjects`1[[psCheckPoint.Objects.CheckPointObject, psCheckPoint, Version=0.5.2.0, Culture=neutral, PublicKeyToken=null]]
Result from commands that return multiple objects.

## NOTES

## RELATED LINKS

