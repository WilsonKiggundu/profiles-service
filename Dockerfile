FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ProfileService/*.csproj ./ProfileService
RUN dotnet restore

# Copy everything else and build
COPY ProfileService/. ./ProfileService/
WORKDIR /app/ProfileService
RUN dotnet publish -c release -o /app --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app ./
ENTRYPOINT ["dotnet", "ProfileService.dll"]
