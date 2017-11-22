# Invoke-CheckPointGroupSync

## SYNOPSIS
Performs a one way sync of external list of group members to a Check Point group.

## SYNTAX

```
Invoke-CheckPointGroupSync -Members <Member[]> -Name <String> [-Rename] [-IgnoreWarnings] [-CreateGroup]
 [-Color <String>] [-Comments <String>] [-Tags <String[]>] [-Session <CheckPointSession>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
{ ... } | New-SyncMember -Prefix "Prefix_" | Invoke-CheckPointGroupSync -Name MyGroup
```

## PARAMETERS

### -Color
Color assigned to created objects

```yaml
Type: String
Parameter Sets: (All)
Aliases: Colour

Required: False
Position: Named
Default value: Red
Accept pipeline input: False
Accept wildcard characters: False
```

### -Comments
Comments for created objects

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

### -CreateGroup
If group doesn't already exist create it.

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

### -IgnoreWarnings
When creating a new object passes IgnoreWarnings switch.

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

### -Members
@{Text=}

```yaml
Type: Member[]
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
Name of group to be synced to list of members

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

### -Rename
If object already exists but with different name, rename it.

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

### -Tags
Tags assigned to created objects

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

## INPUTS

### psCheckPoint.Extra.Sync.Member[]

## OUTPUTS

### psCheckPoint.Extra.Sync.SyncOutput
Detailed results of group sync actions taken.

## NOTES

## RELATED LINKS

