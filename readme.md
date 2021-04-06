## Components

Solution consists of following 2 components:

 - ASP.NET Core Web API
 - Web UI with React

## Api

#### Endpoints

The API exposes following  endpoints:

| Endpoint						 | Description |
| ------------------------------ | ----------- |
| `/api/agents/top/by-property-count`      | Returns top 10 agents with most properties for sales     |
| `/api/agents/top/by-property-keyword`    | Returns top 10 agents selling properties containing certain keyword i.e. tuin        |

#### Frameworks & Libraries used

1. .NET 5
2.  MediatR for CQRS
3.  xUnit for unit testing
4.  WireMock.NET for mocking API

## Web UI

Web UI is a simple javascript SPA built with React.

#### How to run



1. `cd` into `./Funda.Web/app`
2. run `npm install`
3. run `npm start`
4. run `Funda.Api` from visual studio


**Note** : change `MaxRequestsPerMinute` in `appsettings.json` to 5 and re-run the api and UI project,
click `Load Listings` button on UI more than 5 times to see the API error.