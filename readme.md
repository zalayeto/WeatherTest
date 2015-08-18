# Weather Test

## Goal

Develop MVC 5 application that will show aggregated weather data to the user from a location.

## Requirements

From here: https://github.com/mattridgway/WeatherTest

## Design

### Architecture

MVC 5 + WebApi with Unity and MVVM AngularJS in the client.

### Key decisions

  It should be taken into account that the decisions have been made keeping always in mind SOLID principles in order to design a solution that potentially could grow much further that the current requirements.

- Create custom section 'weatherApi' in web.config that holds a list of 'api' in order to allow adding new apis easy as required.
  
- The method in the ApiController is executed Asynchronously. Although in this case we won't face problems like high concurrency, it is always a good practice, taking into account that we're doing use of resources that can be potentially slow and are out of our control. 
  
- An instance of ApiWeatherManager will be in charge of dealing with the call int the ApiController. This class contains a list of IWeatherApi which interface in turn offers a method GetWeather that returns a WeatherResult object. The method will await until it has all the results from the calls to the different apis to calculate the average value with the unit required in every case. If some call has failed, the result is ignored.
  
- IWeatherApi is implemented by the class ApiWeatherHttpClient. This way, potentially, we could include new types of client easily like FtpClient, TCPClient, etc. to consume different types of apis, not only http.
  
- The ApiWeatherManager creates each IWeatherApi with a builder class instance (IApiWeatherHttpClientBuilder), that receives the api configuration for the api, and will return a IWeatherApi instance. In this case, the builder will return ApiWeatherHttpClient, with the implementation of IHttpClient passed as a generic in the class definition. In turn, the IHttpClient is created through a factory leaving the design opened to include new kinds of HttpClient and making easy to inject a IHttpClient implementation for testing purposes.
   
- The IHttpClient only includes a property for the TimeOut and the method Get from an uri, returning an HttpClientResponse. Our implementation of IHttpClient is wrapper around System.Net.HttpClient.
   
### Class diagram

![Alt text](/doc/class_diagram_weather_test.png "Class diagram")

## Implementation details

### Project structure

##### Project WeatherTest.Web

The HttpApplication that holds both the MVC and Web Api implementations.

##### Project WeatherTest.MicroFramework

Interfaces and classes required to implement the solution.

Two namespaces:

- WeatherTest.MicroFramework.Api: all classes and interfaces specifically related to the API.
- WeatherTest.MicroFrawework.Http: infraestructure for IHttpClient component.

### Client

Small angularJS application with one controller and one service to pull the data from the server. The angular project structure code doesn't follow best practices ( folders for controllers, services, views, etc) for simplicity.

The controller holds models for location, temperature unit and wind unit, and the location field is required.

## Testing

Two projects for testing, one for the web and another for the MicroFramework.

In the MicroFrawework the focus for testing has been in two classes:
  
  ApiWeatherHttpClient: to ensure that if a valid response is received this is returned successfully in a WeatherResult.
  ApiWeatherManager: to test that the result returned by the manager are coherent with the different scenarios on calling the api (all returning correct responses, one of the failing, all failing).

## Dependencies

For version details, please, see file package.config:

- Microsoft Unity, Unity Mvc, Unity WebAPI for IoC.
- Newtonsoft.Json for parsing responses from the different apis.
- log4net for logging errors and warnings.
- AngularJs for client side application.
- NUnit, Moq and Moq.Secuences, Fluent Assertions for testing.

## Further work

  The ApiManager could handle some cache in order to save calls to the apis and improve performance. In the same way could keep track of the availability to the different apis (200 response) and notify with a higher level than warning when an api is down x times in a row.

  Use experience improvement.
  
  Error handling in ApiWeatherClient is only log to a file. 

  Thorough testing for every of the component on the server side.

  Client side unit testing with Jasmine.



