FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["packages/Common.EventBus.RabbitMQ.1.0.1.nupkg", "src/Services/packages/"]
COPY ["Services/Task.Service/Task.Service.API/nuget.config", "src/Services/Task.Service/Task.Service.API/"]
COPY ["Services/Task.Service/Task.Service.API/Task.Service.API.csproj", "src/Services/Task.Service/Task.Service.API/"]
RUN dotnet restore "src/Services/Task.Service/Task.Service.API/Task.Service.API.csproj"
COPY . .
WORKDIR "/src/Services/Task.Service/Task.Service.API"
RUN dotnet build "Task.Service.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task.Service.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task.Service.API.dll"]
