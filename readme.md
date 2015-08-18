# Weather Test

## Goal

Develop MVC 5 application that will show aggregated weather data to the user from a location.

## Requirements

From here: https://github.com/mattridgway/WeatherTest

## Design

MVC 5 + WebApi with Unity


### Key decisions

  The decisions have been made keeping always in mind SOLID principles in order to design a solution that pontetially could grow much further that the current requirements.

  Create custom section 'weatherApi' in web.config that holds a list of 'api' in order to allow adding new apis easy as required.
  
  The method in the ApiController is executed Asynchronously. Although in this case we won't face problems like high concurrency, it is always a good practice, taking into account that we're doing use of resources that can be slow 
  
  An instance of ApiWeatherManager will be in charge of receiving the call from the ApiController. This class contains a list of IWeatherApi with a method GetWeather, that returns a WeatherResult. The method will await until it has all the result from the calls to the different apis to calculate the average value with the unit required in every case.
  
   IWeatherApi is implemented by the class ApiWeatherHttpClient. This way, potentially we could include new types of client easily like FtpClient, TCPClient, etc.
  
  The ApiWeatherManager creates the IWeatherApi with a builder class instance(IApiWeatherHttpClientBuilder), that receives the api configuration, and will return a IWeatherApi instance. In this case, the builder will return ApiWeatherHttpClient, with the implementation of IHttpClient passed as a generic in the class definition. In this case, the IHttpClient is created through a factory leaving the design opened to include new kinds of HttpClient.
   
  The IHttpClient only includes a property for the TimeOut and the method Get from an uri, returning an HttpClientResponse. Our implementation of IHttpClient is wrapper around System.Net.HttpClient.
   
### Class diagram

![Alt text](/doc/class_diagram_weather_test.png "Class diagram")

## Implementation details

### Project structure

##### Project WeatherTest.Web

The HttpApplication that holds both the MVC and Web Api implementations.

##### Project WeatherTest.MicroFramework

Interfaces and classes required to implement the solution.

### Client

Small angularJS application with one controller and one service to pull the data from the server. The angular project structure code doesn't follow best practices ( folders for controllers, services, views, etc) for simplicity.

The controller holds models for location, temperature unit and wind unit, and the location field is required.

## Testing



## Dependencies

For version details, please, see file package.config:

- Microsoft Unity, Unity Mvc, Unity WebAPI for IoC.
- Newtonsoft.Json for parsing responses from the different apis.
- log4net for logging errors and warnings.
- AngularJs for client side application.
- NUnit, Moq and Moq.Secuences, Fluent Assertions for testing.



## Further work

Client side unit testing with Jasmine.



