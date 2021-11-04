# financial-chat
Financial Chat
This project is created using .NET Core 5.0
- Uses redis to save the messages data 
- Sql server to save the users
- The users management uses microsoft identity
- The messages flow uses SignalR

To Run the project:
- Microsoft SQL Server latest: https://hub.docker.com/_/microsoft-mssql-server
- Redis: https://hub.docker.com/_/redis
- RabbitMQ: https://hub.docker.com/_/rabbitmq
- QuoteAPI: https://github.com/alexspnovaes/quoteAPI

There is a seed data to populate the chatrooms
