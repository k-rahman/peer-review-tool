FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["packages/Common.EventBus.RabbitMQ.1.0.2.nupkg", "packages/"]
COPY ["Services/Workshop.Service/Workshop.Service.API/nuget.config", "Services/Workshop.Service/Workshop.Service.API/"]
COPY ["Services/Workshop.Service/Workshop.Service.Contracts/Workshop.Service.Contracts.csproj", "Services/Workshop.Service/Workshop.Service.Contracts/"]
COPY ["Services/Workshop.Service/Workshop.Service.API/Workshop.Service.API.csproj", "Services/Workshop.Service/Workshop.Service.API/"]
RUN dotnet restore "Services/Workshop.Service/Workshop.Service.API/Workshop.Service.API.csproj"
COPY . .
WORKDIR "/src/Services/Workshop.Service/Workshop.Service.API"
RUN dotnet build "Workshop.Service.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workshop.Service.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshop.Service.API.dll"]