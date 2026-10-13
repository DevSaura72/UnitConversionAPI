# UnitConversionAPI

An API for unit conversion from various registered units





\# Unit Conversion API



\## Overview

A RESTful API built with \*\*ASP.NET Core 8.0\*\* that converts numerical values between different units of measurement, including length, weight, and temperature.



\## Features

\- Converts units such as meters to feet, kilograms to pounds, and Celsius to Fahrenheit.

\- Stores conversion factors and offsets in `appsettings.json`.

\- Uses a common base unit for each category to simplify conversion logic.

\- Supports decimal values and case-insensitive unit names.

\- Validates unit compatibility and handles invalid requests.

\- Follows separation of concerns using controllers, service interfaces, and dependency injection.

\- Provides Swagger UI for API testing.



\## Technologies

\- ASP.NET Core 8.0 Web API

\- C#

\- JSON configuration

\- Swagger / OpenAPI



\## API Endpoint



\*\*POST\*\* `/api/UnitConversion/convert`



json

{

&#x20; "value": 10,

&#x20; "fromUnit": "meter",

&#x20; "toUnit": "feet"

}





json

{

&#x20; "originalValue": 10,

&#x20; "fromUnit": "meter",

&#x20; "toUnit": "feet",

&#x20; "convertedValue": 32.80839895

}





\## Configuration

Supported unit categories, conversion factors, and offsets are maintained in `appsettings.json`, allowing new units to be configured without modifying the conversion logic.



\## Getting Started

1\. Open the solution in Visual Studio.

2\. Restore NuGet packages.

3\. Build and run the project.

4\. Open `/swagger` in the development environment to explore and test the API.



\## Error Handling

The API returns appropriate HTTP status codes, including `400 Bad Request` for unsupported units, incompatible unit categories, and invalid conversion requests.



\## Project Structure

\- `Controllers` – Handles HTTP requests and responses.

\- `Models` – Defines request and response models.

\- `Services` – Contains the conversion logic and service interface.

\- `Configuration` – Defines strongly typed configuration models.

\- `appsettings.json` – Stores conversion rules and factors.



