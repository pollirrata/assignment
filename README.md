# Assignment for Scalable Web Developer
### Author: Jorge Gaona (@pollirrata) 

## Setup and Usage

- The endpoints can be executed locally or remotely (Deployed to Azure in West Europe Region to reduce latency for your tests)

### App settings
To run locally, _Gaona.Assignment.exe_ needs to have the following app settings defined
- _StorageConnString_ in case you don't want to use the Azure Storage Emulator
- _sgapikey_ A SendGrid API key for email sending ([refer to improvements list](https://github.com/pollirrata/assignment/wiki/Improvements))
- _errormail_ The email address to send the errors

_Gaona.Assignment.Client.exe_ is already set up to call the remote endpoint address. In case you want to run it locally set the _server_ setting to the desired value.

### Using the client app
- Start _Gaona.Assignment.exe_ file
- Start _Gaona.Assignment.Client.exe_ file

The client app will execute 5 tests, based on the binary files included in the project

1. Send a payload bigger than the limit (1MB)
1. Check if data is equal using an image file
1. Check if data has different lenght using two image files
1. Check if data has equal length but has differeces using two text files
1. Check if data has equal length but has differences using two binary files



### Using REST API tester
- Start Assignment application
- Make the corresponding calls for the diff process

An example of the calls I used is on Postman exported file



## Documentation
API documentation (automated using swagger and XML comments) can be found at
- http://waesjg.azurewebsites.net/swagger/ui/index


THEEND
