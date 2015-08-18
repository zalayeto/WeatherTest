# Weather Test

## Goal

Develop MVC 5 application that will show aggregated weather data to the user.

## Requirements

From here: https://github.com/mattridgway/WeatherTest

## Design

MVC 5 + WebApi 
IoC : Unity

### Key decisions

- Create custom 'weatherApi' section that holds a list of 'api' in web.config in order to allow adding new apis easily.
- 

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



