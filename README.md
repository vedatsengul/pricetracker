# Price Tracker Project

This project is created to demonstrate the use of React & ASP.NET Core with SignalR.

## Installation

The frontend and backend are included in the same solution. You can simply open the solution in Visual Studio and run the application.

## Project Structure

The application consists of two projects. pricetracker.client contains the frontend part of the solution. PriceTracker.Server contains the backend part of the solution. 

## Implementation Details - Backend 
There are 3 important classes:

### Stock
This is a model class for encapsulating the stock object

### StockHub
This is SignalR hub class.

### StockPriceGenerator
This is a singleton class that holds the stock list and updates the prices randomly (in the range of 101-200) every second.

## Implementation Details - Frontend
There are 3 important files:
### stock-model.ts
Similar to the backend, this is a model class to hold the stock information
### signalr-connection.ts
This is the connector class that handles SignalR connection management. 

- connectionStart method to start the connection
- connectionClose method to close the connection

When there is a message sent from backend, onMessageReceived event is triggered. 

### App.tsx

stocks object is set by the values fired from events. 

The row colour of the stocks managed by the price changes:
- Green if it is increased,
- Red if it is decreased,
- Yellow if it is unchanged.

