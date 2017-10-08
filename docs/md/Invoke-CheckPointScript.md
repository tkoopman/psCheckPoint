# Invoke-CheckPointScript

## SYNOPSIS
Run script on gateways

## SYNTAX

### By Inline Script
```
Invoke-CheckPointScript -ScriptName <String> -Script <String> [-Args <String>] -Targets <PSObject>
 [-Comments <String>] [-Session <CheckPointSession>]
```

### By Script File
```
Invoke-CheckPointScript -ScriptName <String> -ScriptFile <String> [-Args <String>] -Targets <PSObject>
 [-Comments <String>] [-Session <CheckPointSession>]
```

## DESCRIPTION
Run a script on target gateways.
After completing you can get any output from script by looking at the task details.

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
$(Invoke-CheckPointScript -ScriptName "Get Configuration" -Script "clish -c 'Show Configuration'" -Targets fwm-devbtpp001 | Wait-CheckPointTask).TaskDetails.ResponseMessage
```

## PARAMETERS

### -Args
Script arguments.

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

### -Comments
Comments string

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

### -Script
Script Body

```yaml
Type: String
Parameter Sets: By Inline Script
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ScriptFile
Load Script Body from File

```yaml
Type: String
Parameter Sets: By Script File
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ScriptName
Script Name.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
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

### -Targets
On what targets to execute this command.
Targets may be identified by their name, or object unique identifier.

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

## INPUTS

### System.String
Script Body

### System.Management.Automation.PSObject
On what targets to execute this command.
Targets may be identified by their name, or object unique identifier.

## OUTPUTS

## NOTES

## RELATED LINKS

