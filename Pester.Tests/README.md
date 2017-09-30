# psCheckPoint Tests

## Testing Management Server & Gateway

You will need a Check Point management server and gateway for running these tests against. It is recommened you stand up a All-in-One management server & gateway on evaluation licenses for running these tests against.

A default install is almost enough. Just need to create an Access Role called "PesterRole" for the IA tests to be run against. Make sure you install policy after creating the role so the Gateway knows about it.

## Config file

You must create the config file Pester.Settings.json which should contain the details of your testing management server and gateway. This file is included in the .gitignore file so it will not get pushed to Github.

You can copy the example configuration Pester.Settings.json.Example to create your config file.