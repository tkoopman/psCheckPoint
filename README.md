[![Build status](https://ci.appveyor.com/api/projects/status/ok4ig34od02a87pj/branch/master?svg=true)](https://ci.appveyor.com/project/tkoopman/pscheckpoint/branch/master)

# psCheckPoint
Powershell Module for Check Point R80 Web API and R80.10 Firewall Identity Awareness Web API.
Module will help you use PowerShell to manage your Check Point firewall environment.

Documentation can be found at https://tkoopman.github.io/psCheckPoint/

psCheckPoint works in both the full desktop verion of PowerShell, as well as PowerShell Core (Win, Linux, OSX).

## Installation
`Install-Module -Name psCheckPoint [-Scope CurrentUser]`

## Update
`Update-Module -Name psCheckPoint`

### psCheckPointIA Deprecated
NOTE: If you have psCheckPointIA module installed, uninstall before installing or updating to psCheckPoint v0.5.0+ as they have been merged into the one module, and psCheckPointIA is now deprecated. 

`Uninstall-Module psCheckPointIA`
