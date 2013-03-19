shTwiX
======

shTwiX is a OAUTH/*XAUTH Twitter API Proxy customized and tested to work seamlessly on Twitter for iOS.
It uses Twitterizer to communicate with Twitter.com and handling OAUTH requests. The whole job is being done in shTwiXHandler.ashx. It handles incomming requests from client, re-signs it with the key pair of the user on shTwiX system, forwards the request to api.twitter.com and returns the data directly to the client.



http://twittr.me is a live service that uses shTwiX as it's core.


*XAUTH in shTwiX is simulated. This will make Twitter for iOS to work with shTwiX.