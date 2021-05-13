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
COPY ["Services/Work.Service/Work.Service.API/Work.Service.API.csproj", "src/Services/Work.Service/Work.Service.API/"]
RUN dotnet restore "src/Services/Work.Service/Work.Service.API/Work.Service.API.csproj"
COPY . .
WORKDIR "/src/Services/Work.Service/Work.Service.API"
RUN dotnet build "Work.Service.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Work.Service.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Work.Service.API.dll"]
