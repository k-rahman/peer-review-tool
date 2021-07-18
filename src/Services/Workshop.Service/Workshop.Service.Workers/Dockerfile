FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app


# ENV ASPNETCORE_ENVIRONMENT=Production

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["packages/Common.EventBus.RabbitMQ.1.0.2.nupkg", "packages/"]
COPY ["Services/Workshop.Service/Workshop.Service.Workers/nuget.config", "Services/Workshop.Service/Workshop.Service.Workers/"]
COPY ["Services/Workshop.Service/Workshop.Service.Contracts/Workshop.Service.Contracts.csproj", "Services/Workshop.Service/Workshop.Service.Contracts/"]
COPY ["Services/Workshop.Service/Workshop.Service.Workers/Workshop.Service.Workers.csproj", "Services/Workshop.Service/Workshop.Service.Workers/"]
RUN dotnet restore "Services/Workshop.Service/Workshop.Service.Workers/Workshop.Service.Workers.csproj"
COPY . .
WORKDIR "/src/Services/Workshop.Service/Workshop.Service.Workers"
RUN dotnet build "Workshop.Service.Workers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workshop.Service.Workers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshop.Service.Workers.dll"]



