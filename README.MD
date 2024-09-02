# SignalRChat - ASP.NET Core Blazor (Server) App
## Requirements: .NET 8 SDK
### A real-time chat with sentiment analysis. Technologies used:
- Azure SignalR Service - for real-time communication between users.
- Azure Cognitive Services Text Analytics API - to analyse message sentiment.
- Azure SQL Database - to store messages.
- EF Core - for db setup and data retrieval
---
### In order to run this web application you need to create .env file in the root folder of the project and provide the next values (use .env.sample as an example):
- Azure SignalR Service - connection string.
> If you plan to use SignalR locally, simply replace 'builder.Services.AddSignalR().AddAzureSignalR(...)' with 'builder.Services.AddSignalR()' in Program.cs file. In such case connection string is not needed.

- Azure Cognitive Services Text Analytics API - key and endpoint.
- Azure SQL Database - connection string.
> If you plan to use database locally, provide your SQL Server connection string instead.

With .env file set up, run the next commands: 
```
> dotnet ef database update
> dotnet run
```