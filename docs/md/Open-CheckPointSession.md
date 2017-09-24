# Open-CheckPointSession

## SYNOPSIS
Log in to the server with user name and password.

## SYNTAX

```
Open-CheckPointSession [-ManagementServer] <String> [-ManagementPort <Int32>] [-Credentials] <PSCredential>
 [-ReadOnly] [-ContinueLastSession] [-Domain <String>] [-EnterLastPublishedSession] [-SessionComments <String>]
 [-SessionDescription <String>] [-SessionName <String>] [-SessionTimeout <Int32>] [-NoCertificateValidation]
 [-NoCompression]
```

## DESCRIPTION

## EXAMPLES

### ----------  EXAMPLE 1  ----------
```
$Session = Open-CheckPointSession -ManagementServer 192.168.1.1
```

## PARAMETERS

### -ContinueLastSession
The new session would continue where the last session was stopped.

This option is available when the administrator has only one session that can be continued.

If there is more than one session, see 'switch-session' API.

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

### -Credentials
PSCredential containing User name and Password.
If not provided you will be prompted.

```yaml
Type: PSCredential
Parameter Sets: (All)
Aliases: 

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Domain
Use domain to login to specific domain.
Domain can be identified by name or UID.

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

### -EnterLastPublishedSession
Login to the last published session.
Such login is done with the Read Only permissions.

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

### -ManagementPort
Port Web API running on.
Default: 443

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: 443
Accept pipeline input: False
Accept wildcard characters: False
```

### -ManagementServer
IP or Hostname of the Check point Management Server

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoCertificateValidation
Do NOT verify server's certificate.

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

### -NoCompression
Do not enable HTTP compression.

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

### -ReadOnly
Login with Read Only permissions.
This parameter is not considered in case continue-last-session is true.

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

### -SessionComments
Session comments.

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

### -SessionDescription
Session description.

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

### -SessionName
Session unique name.

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

### -SessionTimeout
Session expiration timeout in seconds.
Default 600 seconds.

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

## INPUTS

## OUTPUTS

### psCheckPoint.Session.CheckPointSession
Used across other Check Point Web API Calls

## NOTES

## RELATED LINKS

