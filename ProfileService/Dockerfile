FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
# COPY ProfileService/ProfileService.csproj ./
# RUN dotnet restore ProfileService.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish "ProfileService/ProfileService.csproj" -o /app/ProfileService/out
RUN ls /app/ProfileService/out/
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
RUN ls
COPY --from=build-env /app/ProfileService/out .
ENTRYPOINT ["dotnet", "ProfileService.dll"]