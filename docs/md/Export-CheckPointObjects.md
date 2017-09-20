# Export-CheckPointObjects

## SYNOPSIS
Export input objects and any other object used by input objects.

## SYNTAX

```
Export-CheckPointObjects [-Session] <CheckPointSession> -Object <PSObject> [-Depth <Int32>]
 [-ExcludeByName <String[]>] [-ExcludeByType <String[]>] [-ExcludeDetailsByName <String[]>]
 [-ExcludeDetailsByType <String[]>] [-SkipWhereUsed] [-IndirectWhereUsed]
```

## DESCRIPTION
Performs an export of input objects and any object used by an input object.

Input objects could be of the following types:

* Any Check Point Object like Host, Network, Rule, etc
* Output from Get-CheckPointWhereUsed
* Output from Get-CheckPointObjects
* An array or list of objects of any mixture of above

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Export-CheckPointObjects -Session $Session -Verbose $InputObject1 $InputObject2 ... $InputObjectX | ConvertTo-CheckPointHtml -Open
```

## PARAMETERS

### -Depth
Max depth to look for objects used by input objects

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 3
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeByName
Enter names of objects to exclude from export

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeByType
Enter types of objects to exclude from export

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeDetailsByName
Enter names of objects you do not want export to search for children of

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExcludeDetailsByType
Enter types of objects you do not want export to search for children of

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IndirectWhereUsed
When passing Check Point objects as input perform a indirect where used instead of the standard direct only.

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

### -Object
Input objects to start export from.

```yaml
Type: PSObject
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

### -SkipWhereUsed
Even if input object is not a rule do not perform a where used

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

## INPUTS

### System.Management.Automation.PSObject
Input objects to start export from.

## OUTPUTS

### psCheckPoint.Extra.Export.CheckPointExportSet
Contains arrays of all exported objects including rules, groups, hosts, etc

Any unknown exported object will have summary in "Other" array

Pipe this to other commands like ConvertTo-CheckPointHtml or ConvertToJson for final export results

## NOTES

## RELATED LINKS

