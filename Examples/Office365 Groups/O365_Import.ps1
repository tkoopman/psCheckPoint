#
# Download Office365/Azure network Details
#
# (C) 2017, Hugo van der Kooij
#
# Don't forget to run `Install-Module psCheckPoint` (as administrator) once!
# v0.4.1 or higher required of psCheckPoint module
#
# WARNING: This script will put a significant load on your SmartCenter!
#
[CmdletBinding(SupportsShouldProcess=$true)]
param(
	[switch]$NoIPv4,
	[switch]$NoIPv6,
	[switch]$PrintURLs,
	[switch]$Publish,
	[switch]$IgnoreWarnings,
	[string]$Color = "red"
)

# Download Microsoft Cloud IP Ranges and Names into Object
$O365IPAddresses = "https://support.content.office.net/en-us/static/O365IPAddresses.xml"
[XML]$O365 = Invoke-WebRequest -Uri $O365IPAddresses -DisableKeepAlive

$Updated = ([datetime]::parseexact($O365.products.updated,"M/d/yyyy",[System.Globalization.CultureInfo]::InvariantCulture)).ToShortDateString()
$Comments = "Microsoft Office365 added $Updated"
$GroupComments = "Microsoft Office365 updated $Updated"
$MSO365 = "Microsoft_Office365"

# Login to Check Point API to get Session ID
Write-Verbose " *** Log in to Check Point Smart Center API *** "
$Session = Open-CheckPointSession -SessionName $MSO365 -SessionComments "Microsoft Office365 Filler" -SessionTimeOut 1800 -NoCertificateValidation -PassThru
if (-not $Session -or $Session.Code) {
	# Failed login
	Write-Error "Failed to login to SmartConsole. $Session"
	exit
}

$Group = Get-CheckPointGroup -Session $Session -Name $MSO365 -Verbose:$false
if ($Group.Code) {
	# Main group does not exist. Create it
	Write-Verbose "Creating main group $MSO365"
	$Group = New-CheckPointGroup -Session $Session -Name $MSO365 -Tag $MSO365 -Color $Color -Comments "$GroupComments" -PassThru
	if ($Group.Code) {
		Write-Error "Failed to create group $MSO365. $Group"
		exit
	}
} else {
	if ($Group.Comments -ne $GroupComments) {
		Write-Verbose "Updating $MSO365 group's comment"
		$Group = Set-CheckPointGroup -Session $Session -Name $MSO365 -Comments "$GroupComments" -Verbose:$false -PassThru
	}
}

# Will keep track of hosts removed from groups.
#After processing we can check these to see if any should be deleted.
$Removed = New-Object System.Collections.ArrayList

ForEach ($Product in $O365.products.product) {
	$GroupName = $MSO365 + "_" + $Product.Name

	# Check if group exists and get existing members
	$Group = Get-CheckPointGroup -Session $Session -Name $GroupName -Verbose:$false

	if ($Group.Code) {
		#Group not found
		Write-Verbose "Creating product group $GroupName"
		$Group = New-CheckPointGroup -Session $Session -Name $GroupName -Tag $MSO365 -Groups $MSO365 -Color $Color -Comments "$GroupComments" -Verbose:$false -PassThru
		$Existing = @()
		if ($Group.Code) {
			Write-Error "Failed to create group $GroupName. $Group"
			exit
		}
	} else {
		$Existing = $Group.Members.Name
		$Count = $Existing.Count
		if ($Count -eq 0) {
			$Existing = @()
		}
		Write-Verbose "Group $GroupName already exists with $Count members"

		if ($Group.Comments -ne $GroupComments) {
			Write-Verbose "Updating $GroupName group's comment"
			$Group = Set-CheckPointGroup -Session $Session -Name $GroupName -Comments "$GroupComments" -Verbose:$false -PassThru
		}
	}

	$MSList = New-Object System.Collections.ArrayList
	ForEach ($AddressList in $Product.addresslist) {
		$Type = $AddressList.type
		if (($Type -eq "IPv4" -and $NoIPv4.IsPresent) -or ($Type -eq "IPv6" -and $NoIPv6.IsPresent)) {
			Write-Verbose "Skipping $Type for $GroupName"
		} else {
			ForEach ($Address in $AddressList.address) {
				ForEach ($Entry in $Address) {
					If ($Type -eq "IPv4" ) {
						$Network = $Entry.split("/")[0]
						$MaskLength = $Entry.split("/")[1]

						# MS sometimes puts /32 and sometimes just no mask length #Consistency
						If (-not $MaskLength -or $MaskLength -eq 32) {
							$i = $MSList.Add("$($MSO365)_$($Network)")
						} else {
							$i = $MSList.Add("$($MSO365)_$($Entry)")
						}
					} ElseIf ($Type -eq "IPv6") {
						$Network = $Entry.split("/")[0]
						$MaskLength = $Entry.split("/")[1]

						# MS sometimes puts /128 and sometimes just no mask length #Consistency
						If (-not $MaskLength -or $MaskLength -eq 128) {
							$i = $MSList.Add("$($MSO365)_$($Network)")
						} else {
							$i = $MSList.Add("$($MSO365)_$($Entry)")
						}
					} ElseIf ($Type -eq "URL") {
						if ($PrintURLs.IsPresent) {
							Write-Host " Hostname ($GroupName) : $Entry"
						}
					}
				}
			}
		}
	}

	# MS sometimes enters the same entry twice #Consistency
	$MSList = $MSList | Select -Unique
	if ($MSList.Count -eq 0) {
		$MSList = @()
	}

	$Diff = Compare-Object -ReferenceObject $MSList -DifferenceObject $Existing -IncludeEqual -Verbose:$false
	$ToAdd = New-Object System.Collections.ArrayList
	$ToRemove = New-Object System.Collections.ArrayList
	ForEach($Entry in $Diff) {
		$ObjName = $Entry.InputObject
		$Split = $Entry.InputObject.split("_")
		$ObjIP = $Split[-1]
		$IsNetwork = ($ObjIP -like "*/*")

		switch ($Entry.SideIndicator)
		{
			"<=" {
				# New entry. Add to group
				# Check if object exists already
				if ($IsNetwork) {
					$Obj = Get-CheckPointNetwork -Session $Session -Name $ObjName -Verbose:$false
				} else {
					$Obj = Get-CheckPointHost -Session $Session -Name $ObjName -Verbose:$false
				}
				if ($Obj.Code) {
					# Create New Object
					if ($IsNetwork) {
						$Network = $ObjIP.split("/")[0]
						$MaskLength = $ObjIP.split("/")[1]
						Write-Verbose "Creating network $ObjName in $GroupName"
						$Obj = New-CheckPointNetwork -Session $Session -Name $ObjName -Subnet $Network -MaskLength $MaskLength -Groups $GroupName -Tags $MSO365 -Color $Color -Comments "$Comments" -IgnoreWarnings:$IgnoreWarnings.IsPresent -Verbose:$false -PassThru
						if ($Obj.Code) {
							Write-Warning "Failed to create network $ObjName in $GroupName. $Obj"
						}
					} else {
						Write-Verbose "Creating host $ObjName in $GroupName"
						$Obj = New-CheckPointHost -Session $Session -Name $ObjName -ipAddress $ObjIP -Groups $GroupName -Tags $MSO365 -Comments "$Comments" -Color $Color -IgnoreWarnings:$IgnoreWarnings.IsPresent -Verbose:$false -PassThru
						if ($Obj.Code) {
							Write-Warning "Failed to create host $ObjName in $GroupName. $Obj"
						}
					}
				} else {
					# Add existing object to group
					Write-Verbose "Adding $ObjName to $GroupName"
					$i = $ToAdd.Add($ObjName)
				}
			}
			"=>" {
				# No longer required. Remove from group
				Write-Verbose "Removing $ObjName from $GroupName"
				$i = $ToRemove.Add($ObjName)
				$i = $Removed.Add($ObjName)
			}
			"==" {
				# Already in group. No change needed
				#Write-Verbose "Leaving $ObjName in $GroupName"
			}
		}
	}
	if ($ToAdd.Count -gt 0) {
		$Obj = Set-CheckPointGroup -Session $Session -Name $GroupName -Members $ToAdd -MemberAction Add -Verbose:$false -PassThru
		if ($Obj.Code) {
			Write-Warning "Failed to add new group members to $GroupName. $Obj"
		}
	}
	if ($ToRemove.Count -gt 0) {
		$Obj = Set-CheckPointGroup -Session $Session -Name $GroupName -Members $ToRemove -MemberAction Remove -Verbose:$false -PassThru
		if ($Obj.Code) {
			Write-Warning "Failed to remove old group members from $GroupName. $Obj"
		}
	}
}

ForEach($Name in $Removed) {
	$used = Get-CheckPointWhereUsed -Session $Session -Name $Name
	if ($used.UsedDirectly.Total -eq 0) {
		Write-Verbose "Deleting $Name"
		if ($Name -like "*/*") {
			$Msg = Remove-CheckPointNetwork -Session $Session -Name $Name -Verbose:$false
			if ($Msg.Code) {
				Write-Warning "Failed to remove unused network $Name. $Msg"
			}
		} else {
			$Msg = Remove-CheckPointHost -Session $Session -Name $Name -Verbose:$false
			if ($Msg.Code) {
				Write-Warning "Failed to remove unused host $Name. $Msg"
			}
		}
	}
}

$Stats = Get-CheckPointSession -Session $Session -UID $Session.UID
if ($Stats.Changes -eq 0) {
	Write-Host "No changes made. Closing session."
	Reset-CheckPointSession -Session $Session -Verbose:$false
	Close-CheckPointSession -Session $Session -Verbose:$false
} elseif ($Publish.IsPresent) {
	# Publish Changes
	Write-Host "Publishing $($Stats.Changes) changes."
	Publish-CheckPointSession -Session $Session -Verbose:$false
	Close-CheckPointSession -Session $Session -Verbose:$false
} else {
	# Logout from Check Point API
	Write-Host "View $($Stats.Changes) changes in SmartConsole to publish."
	Close-CheckPointSession -Session $Session -ContinueSessionInSmartconsole -Verbose:$false
}
# DONE!