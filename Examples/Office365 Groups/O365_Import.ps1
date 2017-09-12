#
# Download Office365/Azure network Details
#
# (C) 2017, Hugo van der Kooij
#
# Don't forget to run `Install-Module psCheckPoint` (as administrator) once!
#
# WARNING: This script will put a significant load on your SmartCenter!
#

# Download Microsoft Cloud IP Ranges and Names into Object
$O365IPAddresses = "https://support.content.office.net/en-us/static/O365IPAddresses.xml"
[XML]$O365 = Invoke-WebRequest -Uri $O365IPAddresses -DisableKeepAlive

$Updated = $O365.products.updated
$Comments = "Microsoft Office365 updated $Updated"
$MSO365 = "Microsoft_Office365"
$MS = "Microsoft"

# Login to Check Point API to get Session ID
Write-Verbose " *** Log in to Check Point Smart Center API *** "
$Session = Open-CheckPointSession -SessionName $MSO365 -SessionComments "Microsoft Office365 Filler" -SessionTimeOut 1800 -NoCertificateValidation


Write-Verbose "New-CheckPointGroup -Session $Session -Name $MSO365 -Tag $MSO365 -Color Red -Comments $Comments"
$Object = New-CheckPointGroup -Session $Session -Name $MSO365 -Tag $MSO365 -Color Red -Comments "$Comments"

ForEach ($Product in $O365.products.product) {
	$GroupName = $MSO365 + "_" + $Product.Name

	Write-Verbose "New-CheckPointGroup -Session $Session -Name $GroupName -Tag $MSO365 -Groups $MSO365 -Color Red -Comments ""$Comments"""
	$Object = New-CheckPointGroup -Session $Session -Name $GroupName -Tag $MSO365 -Groups $MSO365 -Color Red -Comments "$Comments"

	ForEach ($AddressList in $Product.addresslist) {
		$Type = $AddressList.type

		ForEach ($Address in $AddressList.address) {
			ForEach ($Entry in $Address) {
				If ($Type -eq "IPv4" ) {
					$Name = $GroupName + "_" +$Entry
					$Network = $Entry.split("/")[0]
					$MaskLength = $Entry.split("/")[1]
					If ($MaskLength -eq 32) {
						Write-Verbose "New-CheckPointHost -Session $Session -Name $Name -ipv4Address $Network -Color Red -Groups $GroupName -Tags $MSO365,$GroupName -Comments $Comments"
						$Object = New-CheckPointHost -Session $Session -Name $Name -ipv4Address $Network -Color Red -Groups $GroupName -Tags $MSO365,$GroupName -Comments "$Comments"
					} else {
						Write-Verbose "New-CheckPointNetwork -Session $Session -Name $Name -Subnet4 $Network -MaskLength4 $MaskLength -Color Red -Groups $GroupName -Tags $MSO365,$GroupName -Comments $Comments"
						$Object = New-CheckPointNetwork -Session $Session -Name $Name -Subnet4 $Network -MaskLength4 $MaskLength -Color Red -Groups $GroupName -Tags $MSO365,$GroupName -Comments "$Comments"
					}
				} ElseIf ($Type -eq "IPv6") {
					$Network = $Entry.split("/")[0]
					$MaskLength = $Entry.split("/")[1]
					If ($Entry -notlike "*/") {
						$MaskLength = 128
					}
					$Name = $GroupName + "_" +$Entry
					If ($MaskLength -eq 128) {
						Write-Verbose "New-CheckPointHost -Session $Session -Name $Name -ipv6Address $Network -Color Red -Groups $GroupName -Tags $MSO365,$GroupName -Comments $Comments"
						$Object = New-CheckPointHost -Session $Session -Name $Name -ipv6Address $Network -Color Red -Groups $GroupName -Tags $MSO365,$GroupName -Comments "$Comments"
					} else {
						Write-Verbose "New-CheckPointNetwork -Session $Session -Name $Name -Subnet6 $Network -MaskLength6 $MaskLength -Color Red -Groups $GroupName -Tags $MSO365,$GroupName -Comments $Comments"
						$Object = New-CheckPointNetwork -Session $Session -Name $Name -Subnet6 $Network -MaskLength6 $MaskLength -Color Red -Groups $GroupName -Tags $MSO365,$GroupName -Comments "$Comments"
					}
				} ElseIf ($Type -eq "URL") {
					Write-Host " Hostname ($GroupName) : $Entry"
				}
			}
		}
	}
}

# Publish Changes
#Write-Verbose " *** Publish Session changes *** "
#Publish-CheckPointSession -Session $Session
#Reset-CheckPointSession -Session $Session

# Logout from Check Point API
Write-Verbose " *** Logout Session *** "
Close-CheckPointSession -Session $Session -ContinueSessionInSmartconsole

# DONE!