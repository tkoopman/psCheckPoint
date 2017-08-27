# psCheckPointIA Example - Tor
This example reads a list of IPs known to participate in the Tor network and adds them to a IA Role.
You can then use this Tor Role to block access to or from the Tor nodes.

## Tor Nodes List
This script references a list of Tor nodes from (https://www.dan.me.uk/tornodes).

I have no affiliation with this site, I just found it to be a good source for Tor node IP lists.
He does have enforced limits so you can only download a list from his site once every 30 minutes. 
Due to this I do cache the download and reuse it in this example script.