# Set-CheckPointGroup

## SYNOPSIS
Edit existing object using object name or uid.

## SYNTAX

### By UID
```
Set-CheckPointGroup [-MemberAction <MembershipActions>] [-Members <String[]>]
 [-GroupAction <MembershipActions>] [-Groups <String[]>] -UID <String> [-NewName <String>] [-Tags <String[]>]
 [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors] [-Color <String>] [-Session] <CheckPointSession>
```

### By Name
```
Set-CheckPointGroup [-MemberAction <MembershipActions>] [-Members <String[]>]
 [-GroupAction <MembershipActions>] [-Groups <String[]>] [-Name] <String> [-NewName <String>]
 [-Tags <String[]>] [-Comments <String>] [-IgnoreWarnings] [-IgnoreErrors] [-Color <String>]
 [-Session] <CheckPointSession>
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
$cpGroup = Set-CheckPointGroup -Session $Session -Name Test1 -NewName Test2 -Tags TestTag
```

## PARAMETERS

### -Color
Color of the object.
Should be one of existing colors.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Colour

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Comments
Comments string.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -GroupAction
Action to take with groups.

Possible values: Replace, Add, Remove

```yaml
Type: MembershipActions
Parameter Sets: (All)
Aliases: 
Accepted values: Replace, Add, Remove

Required: False
Position: Named
Default value: Replace
Accept pipeline input: False
Accept wildcard characters: False
```

### -Groups
Collection of group identifiers.

Groups listed will be either Added, Removed or replace the current list of group membership based on GroupAction parameter.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -IgnoreErrors
Apply changes ignoring errors.
You won't be able to publish such a changes.
If ignore-warnings flag was omitted - warnings will also be ignored.

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
Apply changes ignoring warnings.

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

### -MemberAction
Action to take with members.

Possible values: Replace, Add, Remove

```yaml
Type: MembershipActions
Parameter Sets: (All)
Aliases: 
Accepted values: Replace, Add, Remove

Required: False
Position: Named
Default value: Replace
Accept pipeline input: False
Accept wildcard characters: False
```

### -Members
Collection of Network objects identified by the name or UID.

Members listed will be either Added, Removed or replace the current list of members based on MemberAction parameter.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Name
Object name.

```yaml
Type: String
Parameter Sets: By Name
Aliases: 

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -NewName
New name of the object.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
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

### -Tags
Collection of tag identifiers.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -UID
Object unique identifier.

```yaml
Type: String
Parameter Sets: By UID
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

## INPUTS

### System.String[]
Collection of Network objects identified by the name or UID.

Members listed will be either Added, Removed or replace the current list of members based on MemberAction parameter.

### System.String[]
Collection of group identifiers.

Groups listed will be either Added, Removed or replace the current list of group membership based on GroupAction parameter.

### System.String
Object unique identifier.

### System.String
Object name.

### System.String
New name of the object.

### System.String[]
Collection of tag identifiers.

### System.String
Comments string.

### System.String
Color of the object.
Should be one of existing colors.

## OUTPUTS

### psCheckPoint.Objects.Group.CheckPointGroup
Details of a Check Point Group

## NOTES

## RELATED LINKS

