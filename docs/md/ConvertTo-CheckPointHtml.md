# ConvertTo-CheckPointHtml

## SYNOPSIS
Convert output from Export-CheckPointObjects to a HTML file.

## SYNTAX

### ES0 (Default)
```
ConvertTo-CheckPointHtml [-Export] <CheckPointExportSet> [-Out <String>] [-Open] [-Template <String>]
```

### ES6
```
ConvertTo-CheckPointHtml [-Export] <CheckPointExportSet> [-Out <String>] [-Open] [-Template <String>]
 [-IndentedJson]
```

### ES5
```
ConvertTo-CheckPointHtml [-Export] <CheckPointExportSet> [-Out <String>] [-Open] [-Template <String>]
 [-EscapeJson]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
Export-CheckPointObjects -Session $Session -Verbose $InputObject | ConvertTo-CheckPointHtml -Open
```

## PARAMETERS

### -EscapeJson
Escape JSON text before inserting into HTML file.

Automatically turned on if templated uses double quotes to define location for JSON.
("{JSON}")

```yaml
Type: SwitchParameter
Parameter Sets: ES5
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Export
Export set from Export-CheckPointObjects

```yaml
Type: CheckPointExportSet
Parameter Sets: (All)
Aliases: 

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -IndentedJson
Indent the JSON data in the HTML output.

This will force the use of "Template Literals" which requires an ES6 compatibile browser.
(\`{JSON}\`)

```yaml
Type: SwitchParameter
Parameter Sets: ES6
Aliases: 

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Open
Open file afterwards in default browser.

If no filename provides then temp file will be created.

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

### -Out
Filename to write HTML file to.

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

### -Template
Filename to HTML file to use as template.

{JSON} will be replaced with JSON data.

Leave blank to use default template.

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

## INPUTS

### psCheckPoint.Extra.Export.CheckPointExportSet
Export set from Export-CheckPointObjects

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

