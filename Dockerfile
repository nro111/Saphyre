# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy solution and all source
COPY Saphyre.sln ./
COPY src/ ./src/

# Restore all projects
RUN dotnet restore

# Build everything to validate
RUN dotnet build -c Release --no-restore

###############
# Publish UI
###############
FROM build AS publish-ui
WORKDIR /src/src/Web/UI
RUN dotnet publish -c Release -o /app/ui --no-restore

###############
# Publish API
###############
FROM build AS publish-api
WORKDIR /src/src/Web/API
RUN dotnet publish -c Release -o /app/api --no-restore

###############
# Final UI image
###############
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS ui-runtime
WORKDIR /app
COPY --from=publish-ui /app/ui .
EXPOSE 8080
ENTRYPOINT ["dotnet", "UI.dll"]

###############
# Final API image
###############
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS api-runtime
WORKDIR /app
COPY --from=publish-api /app/api .
EXPOSE 5000
ENTRYPOINT ["dotnet", "API.dll"]
