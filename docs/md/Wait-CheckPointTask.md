# Wait-CheckPointTask

## SYNOPSIS
Waits for task to complete.

## SYNTAX

```
Wait-CheckPointTask [-SleepTime <Int32>] [-Timeout <Int32>] -TaskID <String> [-Session <CheckPointSession>]
```

## DESCRIPTION
Waits for task to complete then returns the completed task details.

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Install-CheckPointPolicy -PolicyPackage Standard -Targets MyGateway | Wait-CheckPointTask -Verbose
```

## PARAMETERS

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

### -SleepTime
Time in seconds to sleep in-between checking task status

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 5
Accept pipeline input: False
Accept wildcard characters: False
```

### -TaskID
Unique identifier of task

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Timeout
Timeout in seconds.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 300
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### System.String
Unique identifier of task

## OUTPUTS

## NOTES

## RELATED LINKS

