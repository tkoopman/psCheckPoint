# Get-CheckPointApplications

## SYNOPSIS
Retrieve all objects.

## SYNTAX

### Limit
```
Get-CheckPointApplications [-Limit <Int32>] [-Offset <Int32>] [-Session <CheckPointSession>]
```

### All
```
Get-CheckPointApplications [-Limit <Int32>] [-All] [-Session <CheckPointSession>]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```

```

## PARAMETERS

### -All
Get All Records

```yaml
Type: SwitchParameter
Parameter Sets: All
Aliases: 

Required: True
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
Parameter Sets: Limit
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

## INPUTS

## OUTPUTS

### psCheckPoint.Objects.CheckPointObjects
Result from commands that return multiple objects.

## NOTES

## RELATED LINKS

