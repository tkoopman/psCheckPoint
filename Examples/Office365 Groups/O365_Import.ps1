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
	[switch]$NoApplications,
	[switch]$IncludeNonMSURLs,
	[switch]$PrintURLs,
	[switch]$Publish,
	[switch]$IgnoreWarnings,
	[string]$Color = "red",
	[string[]]$MSNS = @(
		"azure-dns.com",
		"azure-dns.info",
		"azure-dns.net",
		"azure-dns.org",
		"azuredns-cloud.net",
		"microsoftonline.com",
		"msedge.net",
		"msft.net",
		"o365filtering.com",
		"s-msedge.net"
	),
	[string[]]$IncludeURLs = @(
		"*.edgekey.net",
		"*.edgesuite.net",
		"*.onedrive.com",
		"*.onenote.com",
		"*.outlook.dev",
		"*.sway.com",
		"*.sway-cdn.com"
		"*.sway-extensions.com",
		"*.tific.com",
		"*.uservoice.com",
		"*.yammerusercontent.com",
		"aka.ms",
		"sway.com"
	)
)

function like([string]$str,[string[]]$patterns){
    foreach($pattern in $patterns) { if($str -like $pattern) { return $true; } }
    return $false;
}

# Download Microsoft Cloud IP Ranges and Names into Object
$O365IPAddresses = "https://support.content.office.net/en-us/static/O365IPAddresses.xml"
[XML]$O365 = Invoke-WebRequest -Uri $O365IPAddresses -DisableKeepAlive

$Updated = ([datetime]::parseexact($O365.products.updated,"M/d/yyyy",[System.Globalization.CultureInfo]::InvariantCulture)).ToShortDateString()
$Comments = "Microsoft Office365 added $Updated"
$GroupComments = "Microsoft Office365 updated $Updated"
$MSO365 = "Microsoft_Office365"
$MSO365APP = "Microsoft_Office365_URLS"
$MSO365CATEGORY = "Microsoft Office365"

# Login to Check Point API to get Session ID
Write-Verbose " *** Log in to Check Point Smart Center API *** "
$Session = Open-CheckPointSession -SessionName $MSO365 -SessionComments "Microsoft Office365 Filler" -SessionTimeOut 1800 -NoCertificateValidation -PassThru
if (-not $Session) {
	# Failed login
	exit
}

$Group = Get-CheckPointGroup -Session $Session -Name $MSO365 -Verbose:$false -ErrorAction SilentlyContinue
if (-not $Group) {
	# Main group does not exist. Create it
	Write-Verbose "Creating main group $MSO365"
	$Group = New-CheckPointGroup -Session $Session -Name $MSO365 -Tag $MSO365 -Color $Color -Comments "$GroupComments" -PassThru
	if (-not $Group) {
		exit
	}
} else {
	if ($Group.Comments -ne $GroupComments) {
		Write-Verbose "Updating $MSO365 group's comment"
		$Group = Set-CheckPointGroup -Session $Session -Name $MSO365 -Comments "$GroupComments" -Verbose:$false -PassThru
	}
}

if (-not $NoApplications.IsPresent) {
	$Category = Get-CheckPointApplicationCategory -Session $Session -Name $MSO365CATEGORY -Verbose:$false -ErrorAction SilentlyContinue
	if (-not $Category) {
		Write-Verbose "Creating O365 category $MSO365CATEGORY"
		$Category = New-CheckPointApplicationCategory -Session $Session -Name $MSO365CATEGORY -Tag $MSO365 -Color $Color -Comments "Microsoft Office365 URLs" -PassThru
		if (-not $Category) {
			exit
		}
	}
}

# Will keep track of hosts removed from groups.
#After processing we can check these to see if any should be deleted.
$Removed = New-Object System.Collections.ArrayList

ForEach ($Product in $O365.products.product) {
	# Check if group exists and get existing members
	$GroupName = $MSO365 + "_" + $Product.Name
	$Group = Get-CheckPointGroup -Session $Session -Name $GroupName -Verbose:$false -ErrorAction SilentlyContinue

	if (-not $Group) {
		#Group not found
		Write-Verbose "Creating product group $GroupName"
		$Group = New-CheckPointGroup -Session $Session -Name $GroupName -Tag $MSO365 -Groups $MSO365 -Color $Color -Comments "$GroupComments" -Verbose:$false -PassThru
		$Existing = @()
		if (-not $Group) {
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

	# App Group for URLs
	$AppGroupName = $MSO365APP + "_" + $Product.Name
	$AppGroup = Get-CheckPointApplication -Session $Session -Name $AppGroupName -Verbose:$false -ErrorAction SilentlyContinue
	if (-not $AppGroup) {
		$AppExisting = @()
	} else {
		$AppExisting = $AppGroup.UrlList
		$Count = $AppExisting.Count
		if ($Count -eq 0) {
			$AppExisting = @()
		}
	}

	$MSList = New-Object System.Collections.ArrayList
	$MSURLList = New-Object System.Collections.ArrayList
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
						if ($IncludeNonMSURLs.IsPresent) {
							$i = $MSURLList.Add($Entry)
							if ($PrintURLs.IsPresent) {
								New-Object PSObject -Property @{"Product" = $Product.Name; "URL" = $Entry}
							}
						} ElseIf (like $Entry  $IncludeURLs) {
							$i = $MSURLList.Add($Entry)
							if ($PrintURLs.IsPresent) {
								New-Object PSObject -Property @{"Product" = $Product.Name; "URL" = $Entry}
							}
						} else {
							$Split = $Entry.Split(".")
							$DomainName = "$($Split[-2]).$($Split[-1])"
							$DomainNS = $(Resolve-DnsName -Type NS -DnsOnly -Name $DomainName -ErrorAction SilentlyContinue -Verbose:$false).NameHost
							if ($DomainNS.Count -eq 0) {
								#Write-Verbose "Excluding URL $Entry. Could not find NS servers for domain $DomainName."
							} else {
								$Split = $DomainNS[0].Split(".")
								$DomainNS = $DomainNS | ForEach {"$($_.Split(".")[-2]).$($_.Split(".")[-1])"}
								$IsMS = (@(Compare-Object $DomainNS $MSNS -includeequal -excludedifferent).count -gt 0)
								if ($IsMS) {
									$i = $MSURLList.Add($Entry)
									if ($PrintURLs.IsPresent) {
										New-Object PSObject -Property @{"Product" = $Product.Name; "URL" = $Entry}
									}
								} else {
									#Write-Verbose "Excluding URL $Entry as domain $DomainName not Microsoft."
								}
							}
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
	$MSURLList = $MSURLList | Select -Unique
	if ($MSURLList.Count -eq 0) {
		$MSURLList = @()
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
					$Obj = Get-CheckPointNetwork -Session $Session -Name $ObjName -Verbose:$false -ErrorAction SilentlyContinue
				} else {
					$Obj = Get-CheckPointHost -Session $Session -Name $ObjName -Verbose:$false -ErrorAction SilentlyContinue
				}
				if (-not $Obj) {
					# Create New Object
					if ($IsNetwork) {
						$Network = $ObjIP.split("/")[0]
						$MaskLength = $ObjIP.split("/")[1]
						Write-Verbose "Creating network $ObjName in $GroupName"
						$Obj = New-CheckPointNetwork -Session $Session -Name $ObjName -Subnet $Network -MaskLength $MaskLength -Groups $GroupName -Tags $MSO365 -Color $Color -Comments "$Comments" -IgnoreWarnings:$IgnoreWarnings.IsPresent -Verbose:$false -PassThru
						if (-not $Obj) {
							Write-Warning "Failed to create network $ObjName in $GroupName. $Obj"
						}
					} else {
						Write-Verbose "Creating host $ObjName in $GroupName"
						$Obj = New-CheckPointHost -Session $Session -Name $ObjName -ipAddress $ObjIP -Groups $GroupName -Tags $MSO365 -Comments "$Comments" -Color $Color -IgnoreWarnings:$IgnoreWarnings.IsPresent -Verbose:$false -PassThru
						if (-not $Obj) {
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
		if (-not $Obj) {
			Write-Warning "Failed to add new group members to $GroupName. $Obj"
		}
	}
	if ($ToRemove.Count -gt 0) {
		$Obj = Set-CheckPointGroup -Session $Session -Name $GroupName -Members $ToRemove -MemberAction Remove -Verbose:$false -PassThru
		if (-not $Obj) {
			Write-Warning "Failed to remove old group members from $GroupName. $Obj"
		}
	}

	# Process URLs
	if (-not $NoApplications.IsPresent) {
		$ToAdd = New-Object System.Collections.ArrayList
		$ToRemove = New-Object System.Collections.ArrayList

		$Diff = Compare-Object -ReferenceObject $MSURLList -DifferenceObject $AppExisting -IncludeEqual -Verbose:$false

		ForEach($Entry in $Diff) {
			$URL = $Entry.InputObject

			switch ($Entry.SideIndicator)
			{
				"<=" {
					# New URL. Add to application
					Write-Verbose "Adding $URL from $AppGroupName"
					$i = $ToAdd.Add($URL)
				}
				"=>" {
					# No longer required. Remove from application
					Write-Verbose "Removing $URL from $AppGroupName"
					$i = $ToRemove.Add($URL)
				}
				"==" {
					# Already in application. No change needed
					#Write-Verbose "Leaving $ObjName in $AppGroupName"
				}
			}
		}

		if ($ToAdd.Count -gt 0) {
			if ($AppGroup) {
				$Obj = Set-CheckPointApplication -Session $Session -Name $AppGroupName -UrlList $ToAdd -UrlAction Add -Comments $GroupComments -Verbose:$false -PassThru
				if (-not $Obj) {
					Write-Warning "Failed to add new URLs to $AppGroupName."
				}
			} else {
				$Obj = New-CheckPointApplication -Session $Session -Name $AppGroupName -Tag $MSO365 -Color $Color -Comments "$GroupComments" -UrlList $ToAdd -PrimaryCategory $MSO365CATEGORY -Verbose:$false -PassThru
				if (-not $Obj) {
					Write-Warning "Failed to create $AppGroupName."
				}
			}
		}
		if ($ToRemove.Count -gt 0) {
			if ($ToRemove.Count -eq $AppGroup.UrlList.Count) {
				Remove-CheckPointApplication -Session $Session -Name $AppGroupName
			} else {
				$Obj = Set-CheckPointApplication -Session $Session -Name $AppGroupName -UrlList $ToRemove -Comments "$GroupComments" -UrlAction Remove -Verbose:$false -PassThru
				if (-not $Obj) {
					Write-Warning "Failed to remove old URLs from $AppGroupName."
				}
			}
		}
	}
}

$Removed = $Removed | Select -Unique
ForEach($Name in $Removed) {
	$used = Get-CheckPointWhereUsed -Session $Session -Name $Name
	if ($used.UsedDirectly.Total -eq 0) {
		Write-Verbose "Deleting $Name"
		if ($Name -like "*/*") {
			Remove-CheckPointNetwork -Session $Session -Name $Name -Verbose:$false
		} else {
			Remove-CheckPointHost -Session $Session -Name $Name -Verbose:$false
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