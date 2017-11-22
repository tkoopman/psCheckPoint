# New-SyncMember

## SYNOPSIS
Creates a member object used for pipelining into Invoke-CheckPointGroupSync detailing list of source members to sync.

## SYNTAX

### Prefix (Default)
```
New-SyncMember -Prefix <String> -Address <String>
```

### Fixed Name
```
New-SyncMember -Name <String> -Address <String>
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
{ ... } | New-SyncMember -Prefix "Prefix_" | Invoke-CheckPointGroupSync -Name MyGroup
```

## PARAMETERS

### -Address
IPv4 or IPv6 Host IP or Subnet CIDR.

```yaml
Type: String
Parameter Sets: (All)
Aliases: IPAddress, IP, Subnet

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Name
Object name.
Should be unique in the domain.

```yaml
Type: String
Parameter Sets: Fixed Name
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Prefix
Object name.
Should be unique in the domain.

```yaml
Type: String
Parameter Sets: Prefix
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### System.String
Object name.
Should be unique in the domain.

### System.String
IPv4 or IPv6 Host IP or Subnet CIDR.

## OUTPUTS

### psCheckPoint.Extra.Sync.Member
Details a single source member objects to sync with Check Point group

## NOTES

## RELATED LINKS

