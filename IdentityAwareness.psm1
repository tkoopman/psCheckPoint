function AddIdentity {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory=$true)]                                    [string]   $Gateway,
        [Parameter(Mandatory=$true)]                                    [string]   $SharedSecret,
        [Parameter(ValueFromPipelineByPropertyName, Mandatory=$true)]   [string]   $IPAddress,
        [Parameter(ValueFromPipelineByPropertyName)]                    [string]   $User,
        [Parameter(ValueFromPipelineByPropertyName)]                    [string]   $Machine,
        [Parameter(ValueFromPipelineByPropertyName)]                    [string]   $Domain,
        [Parameter(ValueFromPipelineByPropertyName)]                    [int]      $SessionTimeout,
        [Parameter(ValueFromPipelineByPropertyName)]                    [switch]   $FetchUserGroups,
        [Parameter(ValueFromPipelineByPropertyName)]                    [switch]   $FetchMachineGroups,
        [Parameter(ValueFromPipelineByPropertyName)]                    [string[]] $UserGroups,
        [Parameter(ValueFromPipelineByPropertyName)]                    [string[]] $MachineGroups,
        [Parameter(ValueFromPipelineByPropertyName)]                    [switch]   $CalculateRoles,
        [Parameter(ValueFromPipelineByPropertyName)]                    [string[]] $Roles,
        [Parameter(ValueFromPipelineByPropertyName)]                    [string]   $MachineOS,
        [Parameter(ValueFromPipelineByPropertyName)]                    [string]   $HostType,
                                                                        [int]      $BatchSize = 10
    )
    
    Begin {
        $_Batch = New-Object System.Collections.Generic.List[System.Object]
    }
    Process {
        $Payload = @{'ip-address'=$IPAddress}
        AddStringPayload -Payload $Payload -Name user                   -Value $User
        AddStringPayload -Payload $Payload -Name machine                -Value $Machine
        AddStringPayload -Payload $Payload -Name domain                 -Value $Domain
        AddIntPayload    -Payload $Payload -Name 'session-timeout'      -Value $SessionTimeout
        AddSwitchPayload -Payload $Payload -Name 'fetch-user-groups'    -Value $FetchUserGroups
        AddSwitchPayload -Payload $Payload -Name 'fetch-machine-groups' -Value $FetchMachineGroups
        AddArrayPayload  -Payload $Payload -Name 'user-groups'          -Value $UserGroups
        AddArrayPayload  -Payload $Payload -Name 'machine-groups'       -Value $MachineGroups
        AddSwitchPayload -Payload $Payload -Name 'calculate-roles'      -Value $CalculateRoles
        AddArrayPayload  -Payload $Payload -Name roles                  -Value $Roles
        AddStringPayload -Payload $Payload -Name 'machine-os'           -Value $MachineOS
        AddStringPayload -Payload $Payload -Name 'host-type'            -Value $HostType
        
        if ($BatchSize -le 1) {
            AddStringPayload -Payload $Payload -Name 'shared-secret' -Value $SharedSecret
            APICall-AddIdentity -Gateway $Gateway -Payload $Payload
        } else {
            $_Batch.Add($Payload)
            if ($_Batch.Count -ge $BatchSize) {
                $Payload = @{'shared-secret'=$SharedSecret; requests=$($_Batch.ToArray())}
                $Results = APICall-AddIdentity -Gateway $Gateway -Payload $Payload
                ForEach ($Result in $Results.responses) {
                    $Result
                }
                $_Batch = New-Object System.Collections.Generic.List[System.Object]
            }
        }
        
    }
    End {
        if ($_Batch.Count -ge 1) {
            $Payload = @{'shared-secret'=$SharedSecret; requests=$($_Batch.ToArray())}
            $Results = APICall-AddIdentity -Gateway $Gateway -Payload $Payload
            ForEach ($Result in $Results.responses) {
                $Result
            }
        }
    }
}

<# 
 .Synopsis
  Adds switch boolean value to Payload.
  
 .Description
  If Value is True adds parameter to Payload.

 .Parameter Payload
  Current Payload hashtable to add identifier to
  
 .Parameter Name
  Name of parameter to be added
  
 .Parameter Value
  True or False value to set
  
 .Parameter Force
  Forces parameter to be added even if it is false. Use if the Web API default is True.
#>
function AddSwitchPayload {
    param (
        [Parameter(Mandatory=$true)] [hashtable] $Payload,
        [Parameter(Mandatory=$true)] [string]    $Name,
        [Parameter(Mandatory=$true)] [bool]      $Value,
        [switch] $Force
    )
    
    if (-not $Value) {
        $Payload.$Name = 0
    } elseif ($Force) {
        $Payload.$Name = 1
    }
}

<# 
 .Synopsis
  Adds array value to Payload.

 .Parameter Payload
  Current Payload hashtable to add identifier to
  
 .Parameter Name
  Name of parameter to be added
  
 .Parameter Values
  Array to add
  
 .Parameter Force
  Forces parameter to be added even if it is false. Use if the Web API default is True.
#>
function AddArrayPayload {
    param (
        [Parameter(Mandatory=$true)] [hashtable] $Payload,
        [Parameter(Mandatory=$true)] [string]    $Name,
                                     [string[]]  $Values,
        [switch] $Force
    )
    
    if ($Values.Count -gt 0) {
        if ($Values.Count -eq 1) {
            $Values = $Values.split(@(",", ";"), [System.StringSplitOptions]::RemoveEmptyEntries)
        }
        $Payload.$Name = $Values
    } elseif ($Force) {
        $Payload.$Name = @()
    }
}

<# 
 .Synopsis
  Adds string value to Payload only if not blank.

 .Parameter Payload
  Current Payload hashtable to add identifier to
  
 .Parameter Name
  Name of parameter to be added
  
 .Parameter Value
  String value to add
#>
function AddStringPayload {
    param (
        [Parameter(Mandatory=$true)] [hashtable] $Payload,
        [Parameter(Mandatory=$true)] [string]    $Name,
                                     [string]    $Value
    )
    
    if ($Value) {
        $Payload.$Name = $Value
    }
}

<# 
 .Synopsis
  Adds string value to Payload only if not blank.

 .Parameter Payload
  Current Payload hashtable to add identifier to
  
 .Parameter Name
  Name of parameter to be added
  
 .Parameter Value
  Int value to add
#>
function AddIntPayload {
    param (
        [Parameter(Mandatory=$true)] [hashtable] $Payload,
        [Parameter(Mandatory=$true)] [string]    $Name,
                                     [int]       $Value
    )
    
    if ($Value) {
        $Payload.$Name = $Value
    }
}

<# 
 .Synopsis
  Runs Check Point Web API Call

 .Description
  Runs Check Point Web API Call.

 .Parameter Session
  Session object returned by Invoke-CPLogin command

 .Parameter Command
  Command you wish to run

 .Parameter Payload
  Hashtable of parameters to pass to the call.
#>
function APICall-AddIdentity {
    param (
        [Parameter(Mandatory=$true)] [string] $Gateway,
        [hashtable] $Payload = @{}
    )

    $jsonPayload = $Payload | ConvertTo-Json -Depth 3 -Compress
        
    Write-Debug @"
Calling "https://$($Gateway)/_IA_API/v1.0/add-identity"
---Payload Start---
$($jsonPayload)
---Payload End---
"@
        
    try {
        $Result = Invoke-RestMethod -Uri "https://$($Gateway)/_IA_API/add-identity" -Method Post -ContentType "application/json" -Body $jsonPayload -Verbose:$false
    } catch [System.Net.WebException] {
        $e = $_
        if ($e.Exception.Response) {
            Write-Error "Result from gateway: $([int]$e.Exception.Response.StatusCode)"
            
            $streamReader = [System.IO.StreamReader]::new($e.Exception.Response.GetResponseStream())
            $Result = $streamReader.ReadToEnd()
            $streamReader.Close()
            
            Write-Host "$($Result)"
        } else {
            if ($e.Exception.Message.Contains("Could not establish trust relationship")) {
                $host.ui.WriteErrorLine.invoke(@"
$($e.Exception.Message)
You may need to run the following command first to allow self-signed certificates:
[System.Net.ServicePointManager]::ServerCertificateValidationCallback = { `$True }
"@)
            } else {
                Write-Error $e.Exception
            }
        }
    } catch {
        Write-Error "$($_.Exception)"
    }
    
    $Result
}

export-modulemember -function "AddIdentity"
