FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["packages/Common.EventBus.RabbitMQ.1.0.2.nupkg", "packages/"]
COPY ["packages/Workshop.Service.Contracts.2.2.4.nupkg", "packages/"]
COPY ["Services/Emailing.Service/Emailing.Service.API/nuget.config", "Services/Emailing.Service/Emailing.Service.API/"]
COPY ["Services/Emailing.Service/Emailing.Service.API/Emailing.Service.API.csproj", "Services/Emailing.Service/Emailing.Service.API/"]
RUN dotnet restore "Services/Emailing.Service/Emailing.Service.API/Emailing.Service.API.csproj"
COPY . .
WORKDIR "/src/Services/Emailing.Service/Emailing.Service.API"
RUN dotnet build "Emailing.Service.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Emailing.Service.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Emailing.Service.API.dll"]