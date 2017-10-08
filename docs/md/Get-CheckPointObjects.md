# Get-CheckPointObjects

## SYNOPSIS
Find objects by Filter.

## SYNTAX

### Filter (Default)
```
Get-CheckPointObjects [-Filter <String>] [-IPOnly] [-Type <String>] [-Limit <Int32>] [-Offset <Int32>]
 [-Session <CheckPointSession>]
```

### Unused
```
Get-CheckPointObjects [-Unused] [-Limit <Int32>] [-Offset <Int32>] [-Session <CheckPointSession>]
```

## DESCRIPTION
Can find many different types of objects based on a filter.
Filters are same as what can be used in Smart Console

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Get-CheckPointObjects -Filter "O365 OR Office365"
```

### ----------  EXAMPLE 2  ----------
```
Get-CheckPointObjects -Unused
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
Parameter Sets: Filter
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
Parameter Sets: Filter
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Limit
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

### -Offset
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
The objects' type

```yaml
Type: String
Parameter Sets: Filter
Aliases: 

Required: False
Position: Named
Default value: Object
Accept pipeline input: False
Accept wildcard characters: False
```

### -Unused
Retrieve all unused objects.

```yaml
Type: SwitchParameter
Parameter Sets: Unused
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

## OUTPUTS

### psCheckPoint.Objects.CheckPointObjects
Result from commands that return multiple objects.

## NOTES

## RELATED LINKS

