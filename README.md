# Assignment (Required)

Create a .NET Web API that takes ZIP-code as a parameter, then outputs city name, current temperature, time zone, and general elevation at the location with a user-friendly message. For example, *“At the location $CITY_NAME, the temperature is $TEMPERATURE, and the timezone is $TIMEZONE”*. Include documentation with any necessary build instructions and be prepared to discuss your approach.

- Use the [Open WeatherMap current weather API](https://openweathermap.org/api) to retrieve the current temperature and city name. You will be required to sign up for a [free API key](https://openweathermap.org/appid).
- Use the [Google Time Zone API](https://developers.google.com/maps/documentation/timezone/start?hl=en_US) to get the current timezone for a location. You will again need to register a “project” and sign up for a [free API key](https://developers.google.com/maps/documentation/timezone/get-api-key) * with Google.

* Note that a credit card will [soon be required](https://cloud.google.com/maps-platform/user-guide/?_ga=2.15384382.901596364.1527268232-1880365229.1525881538) to register for an API key with Google (though the first 40k API calls are [still free](https://cloud.google.com/maps-platform/pricing/sheet/)).

# Prerequisites:

- .NET Framework 4.7.2 needs being installed. Download it [here](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-developer-pack-offline-installer)
- IIS should be turned on in Windows Features
    - ASP.NET 3.5 and ASP.NET 4.5+ should be turned on too
        **Internet Information Services - World Wide Web Services - Application Development Features**

# Steps:
1. Unzip **zipcoderesolver.zip**. It is located in the root folder of project
2. Open extracted folder **zipcoderesolver**
3. Open PowerShell with admin permissions
3. Run `.\deployer.ps1` in PowerShell
    Script creates new website **zipcoderesolver** on **8081** port

# Example:

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

# Enhancements:
- add unit tests
- add class for API settings
- send requests simultiously (currently requests go one after another sake of simplicity)
- add friendly errors and handling them 