<#

.SYNOPSIS
One way sync of AWS networks into Check Point groups.

.DESCRIPTION
This script will create/update Check Point groups for each AWS Service & Region, with the list of networks AWS publish.

.PARAMETER ManagementServer
IP or Hostname of the Check point Management Server

.PARAMETER Credentials
PSCredential containing User name and Password. If not provided you will be prompted.

.PARAMETER CertificateHash
The server's SSL certificate hash

.PARAMETER ManagementPort
Port Web API running on.

.PARAMETER NoIPv4
Do not include IPv4 addresses.

.PARAMETER NoIPv6
Do not include IPv6 addresses.

.PARAMETER Publish
If any changes made publish them automatically. By default session will just be closed pending you to manually open session in SmartConsole and publish the changes.
Publish will only happen if no errors during sync.

.PARAMETER Ignore
Weather Check Point warnings or errors should be ignored.

.PARAMETER Rename
If existing object not found by name, first search by IP/Subnet and if matching object found rename it and add to group.

.PARAMETER Color
Check Point color to set on created objects.

.PARAMETER Prefix
Prefix used on host/network objects.

.PARAMETER GroupPrefix
Prefix used on group objects.

.PARAMETER CommentPrefix
Prefix used on comments (Groups, Session, Created Hosts & Networks).

.PARAMETER Tag
Tag set when creating objects.

.PARAMETER CertificateValidation
Which certificate validation method(s) to use.

.PARAMETER Domain
Specifies the Check Point MDS Domain to connect to.

.PARAMETER Region
If set, contains one or more region identifiers (e.g. "us-east-1", "global") to filter the output to. This parameter can be used in conjunction with the ServiceKey parameter to filter by region and service key.

.PARAMETER ServiceKey
If set, contains one or more service keys to filter the output to. This parameter can be used in conjunction with the Region parameter to filter by region and service key.

.EXAMPLE
./AWS_Group_Sync.ps1 -NoIPv6 -Rename -Verbose

.NOTES
Requires psCheckPoint v1.0.0+.
Requires AWSPowerShell.

.LINK
https://github.com/tkoopman/psCheckPoint

.LINK
https://aws.amazon.com/documentation/powershell/

#>
[CmdletBinding()]
param(
	[Parameter(Mandatory = $true, ParameterSetName='Standard')]
    [string]$ManagementServer,
	[Parameter(Mandatory = $true, ParameterSetName='Standard')]
	[PSCredential]$Credentials,
	[Parameter(ParameterSetName='Standard')]
	[string]$CertificateHash,
	[Parameter(ParameterSetName='Standard')]
    [int]$ManagementPort = 443,
	[Parameter(ParameterSetName='Standard')]
	[switch]$NoIPv4,
	[Parameter(ParameterSetName='Standard')]
	[switch]$NoIPv6,
	[Parameter(ParameterSetName='Standard')]
	[switch]$Publish,
	[Parameter(ParameterSetName='Standard')]
	[ValidateSet("No", "Warnings", "Errors")]
	[string]$Ignore = "No",
	[Parameter(ParameterSetName='Standard')]
	[switch]$Rename,
	[Parameter(ParameterSetName='Standard')]
	[string]$Color = "red",
	[Parameter(ParameterSetName='Standard')]
	[string]$Prefix = "AWS",
	[Parameter(ParameterSetName='Standard')]
	[string]$GroupPrefix = "AWS",
	[Parameter(ParameterSetName='Standard')]
	[string]$CommentPrefix = "AWS",
	[Parameter(ParameterSetName='Standard')]
	[string]$Tag = "AWS",
	[Parameter(ParameterSetName='Standard')]
	[ValidateSet("All", "Auto", "CertificatePinning", "None", "ValidCertificate")]
	[string]$CertificateValidation = "Auto",
	[Parameter(ParameterSetName='Standard')]
	[string]$Domain = "",
	[string[]]$Region = $null,
	[string[]]$ServiceKey = $null,
	[Parameter(Mandatory = $true, ParameterSetName='Print Service Regions')]
	[switch]$PrintServiceRegions
)
$AWSPIAR = Get-AWSPublicIpAddressRange -Region $Region -ServiceKey $ServiceKey | Where-Object { ($_.IpAddressFormat -eq "Ipv4" -and -not $NoIPv4.IsPresent) -or ($_.IpAddressFormat -eq "Ipv6" -and -not $NoIPv6.IsPresent) };
$Services = $AWSPIAR | Select-Object -ExpandProperty Service -Unique | Sort-Object;

if ($PrintServiceRegions.IsPresent) {
    ForEach($Service in $Services) {
		$Regions = $AWSPIAR | Where-Object {$_.Service -eq $Service} | Select-Object -ExpandProperty Region -Unique | Sort-Object;
		ForEach($R in $Regions) {
			[PSCustomObject]@{
				Service = $Service
				Region = $R
			}
		}
	}

	exit;
}

# Set variables
$Updated = (Get-AWSPublicIpAddressRange -OutputPublicationDate).ToShortDateString();
$Comments = "Amazon AWS added $Updated";
$GroupComments = "updated $Updated";
$Errors = 0;

# Login to Check Point API to get Session ID
Write-Verbose " *** Log in to Check Point Smart Center API *** ";
$Session = Open-CheckPointSession -SessionName $CommentPrefix -SessionComments "$CommentPrefix Group Sync" -ManagementServer $ManagementServer -ManagementPort $ManagementPort -Domain $Domain -Credentials $Credentials -CertificateValidation $CertificateValidation -CertificateHash $CertificateHash -PassThru;
if (-not $Session) {
	# Failed login
	exit;
}

ForEach($Service in $Services) {
	$Regions = $AWSPIAR | Where-Object {$_.Service -eq $Service} | Select-Object -ExpandProperty Region -Unique | Sort-Object;
	ForEach($R in $Regions) {
		if ($Service -eq "AMAZON") {
			$GroupName = $GroupPrefix + "_" + $R;
		} else {
			$GroupName = $GroupPrefix + "_" + $Service + "_" + $R;
		}

		Write-Verbose "Processing $GroupName";

		$AWSPIAR | Where-Object {$_.Service -eq $Service -and $_.Region -eq $R} |
			Select-Object -ExpandProperty IpPrefix |
			Invoke-CheckPointGroupSync -Session $Session -GroupName $GroupName -Prefix "${Prefix}_" -Rename:$Rename.IsPresent -Color $Color -Comments $Comments -Tags $Tag -CreateGroup -Ignore $Ignore |
			Tee-Object -Variable output;

		if (($output | Where-Object {$_.Actions -ne 0 -and -not $_.Error} | Measure-Object).Count -ne 0) {
			# Updates made
			Write-Verbose "Updating $GroupName group's comment";
			$Desc = (Get-AWSRegion -SystemName $R).Name;
			$Group = Set-CheckPointGroup -Session $Session -Name $GroupName -Comments "$CommentPrefix $Desc $GroupComments" -Verbose:$false -PassThru;
		}
		$Errors = $Errors + ($output | Where-Object {$_.Error} | Measure-Object).Count;
	}
}

$Stats = Get-CheckPointSession -Session $Session -UID $Session.UID;
Write-Verbose "Total Errors: $Errors";
if ($Stats.Changes -eq 0) {
	Write-Host "No changes made. Closing session.";
	Reset-CheckPointSession -Session $Session -Verbose:$false;
	Close-CheckPointSession -Session $Session -Verbose:$false;
} elseif ($Publish.IsPresent -and $Errors -eq 0) {
	# Publish Changes
	Write-Host "Publishing $($Stats.Changes) changes.";
	Publish-CheckPointSession -Session $Session -Verbose:$false;
	Close-CheckPointSession -Session $Session -Verbose:$false;
} else {
	# Logout from Check Point API
	Write-Host "View $($Stats.Changes) changes in SmartConsole to publish.";
	Close-CheckPointSession -Session $Session -ContinueSessionInSmartconsole -Verbose:$false;
}
# DONE!
