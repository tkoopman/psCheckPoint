# Get-CheckPointObjects

## SYNOPSIS
Find objects by Filter.

## SYNTAX

```
Get-CheckPointObjects [-Filter <String>] [-IPOnly] [-Type <String>] [-UID <Int32>] [-Name <Int32>]
 [-Session] <CheckPointSession>
```

## DESCRIPTION
Can find many different types of objects based on a filter.
Filters are same as what can be used in Smart Console

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Get-CheckPointObjects -Session $Session -Filter "O365 OR Office365"
```

## PARAMETERS

### -Filter
Search expression to filter objects by.
The provided text should be exactly the same as it would be given in Smart Console.
The logical operators in the expression ('AND', 'OR') should be provided in capital letters.
By default, the search involves both a textual search and a IP search.
To use IP search only, set the "ip-only" parameter to true.

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

### -IPOnly
If using "filter", use this field to search objects by their IP address only, without involving the textual search.

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

### -Type
The objects' type

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: O, b, j, e, c, t
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

### psCheckPoint.Objects.CheckPointObjects`1[[psCheckPoint.Objects.CheckPointObject, psCheckPoint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null]]
Result from commands that return multiple objects.

## NOTES

## RELATED LINKS

