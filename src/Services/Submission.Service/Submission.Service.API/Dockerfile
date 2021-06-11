FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["packages/Common.EventBus.RabbitMQ.1.0.1.nupkg", "src/Services/packages/"]
COPY ["Services/Submission.Service/Submission.Service.API/nuget.config", "src/Services/Submission.Service/Submission.Service.API/"]
COPY ["Services/Submission.Service/Submission.Service.API/Submission.Service.API.csproj", "src/Services/Submission.Service/Submission.Service.API/"]
RUN dotnet restore "src/Services/Submission.Service/Submission.Service.API/Submission.Service.API.csproj"
COPY . .
WORKDIR "/src/Services/Submission.Service/Submission.Service.API"
RUN dotnet build "Submission.Service.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Submission.Service.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Submission.Service.API.dll"]
