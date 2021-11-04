#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/FinancialChat.UI/FinancialChat.UI.csproj", "src/FinancialChat.UI/"]
COPY ["src/FinancialChat.Domain/FinancialChat.Domain.csproj", "src/FinancialChat.Domain/"]
COPY ["src/FinancialChat.Infra/FinancialChat.Infra.Data.csproj", "src/FinancialChat.Infra/"]
COPY ["src/FinancialChat.Infra.ExternalServices/FinancialChat.Infra.ExternalServices.csproj", "src/FinancialChat.Infra.ExternalServices/"]
RUN dotnet restore "src/FinancialChat.UI/FinancialChat.UI.csproj"
COPY . .
WORKDIR "/src/src/FinancialChat.UI"
RUN dotnet build "FinancialChat.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialChat.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialChat.UI.dll"]