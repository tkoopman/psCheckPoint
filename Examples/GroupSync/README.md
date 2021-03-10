# psCheckPoint Example - Group Sync
These examples show how to do a one way sync of host IPs & networks into Check Point groups. 

Cloud examples below are good for either:
* Reporting on all traffic to and from cloud providers
* For when you cannot use Application Control / URL Filtering but need to create IP base firewall rules

## Office365_Group_Sync.ps1
This script will create/sync all Office 365 product groups with the lists Microsoft maintains.

## Azure_Group_Sync.ps1
This script will create/sync all region groups with the lists Microsoft maintains.

## AWS_Group_Sync.ps1
This script will create/sync all region groups with the output of Get-AWSPublicIpAddressRange from Amazon AWS Powershell toolkit.

# Credits
The inital Office365 script is from Hugo van der Kooij that he submitted on [Check Point CHECKMATES](https://community.checkpoint.com/thread/5610-powershell-microsoft-office365).

Has now been re-written by Tim Koopman.
