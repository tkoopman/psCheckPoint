# Get-CheckPointFullObject

## SYNOPSIS
Retrieve full object details from object summary.

## SYNTAX

```
Get-CheckPointFullObject [-Session] <CheckPointSession> -Object <PSObject>
```

## DESCRIPTION
Many commands return lists of object summarys.

Use this to return the full objects for each summary.

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Get-CheckPointGroups -Session $Session | Get-CheckPointFullObject -Session $Session
```

## PARAMETERS

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

## INPUTS

### System.Management.Automation.PSObject
Input objects to start export from.

## OUTPUTS

### psCheckPoint.Objects.CheckPointObject
Base summary details of Check Point Objects

## NOTES

## RELATED LINKS

