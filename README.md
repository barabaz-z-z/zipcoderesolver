Prerequisites:

- .NET Framework 4.7.2 needs being installed. Download it [here](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-developer-pack-offline-installer)
- IIS should be turned on in Windows Features
    - ASP.NET 3.5 and ASP.NET 4.5+ should be turned on too
        **Internet Information Services - World Wide Web Services - Application Development Features**

Steps:
1. Unzip **zipcoderesolver.zip**. It is located in the root folder of project
2. Open extracted folder **zipcoderesolver**
3. Open PowerShell with admin permissions
3. Run `.\deployer.ps1` in PowerShell
    Script creates new website **zipcoderesolver** on **8081** port

Example:

```
GET http://localhost:8081/api/zipcodes/80-392
```

Result json:
```
{
  "city": {
    "id": "ChIJZ2NGD_wH2kIRCC7yKjeIW2c",
    "name": "Prokopyevsk, Russia, 653053",
    "postalCode": "653053",
    "timeZoneName": "Krasnoyarsk Standard Time",
    "elevation": 435.31884765625,
    "temperature": -12.34,
    "location": {
      "latitude": 53.868035199999987,
      "longitude": 86.6179971
    }
  },
  "selectionInfo": {
    "count": 1,
    "message": "Only 1 result was found. You provide precise value of postal code"
  }
}
```


