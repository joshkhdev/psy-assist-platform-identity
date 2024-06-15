# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET SDK as a build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthService.csproj", "."]
RUN dotnet restore "./AuthService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./AuthService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthService.csproj" -c $BUILD_CONFIGURATION -o /app/publish 
# /p:UseAppHost=false

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Copy the certificate into the container
COPY ["certificate.pfx", "/app/certificate.pfx"]
# Set the entry point for the container
ENTRYPOINT ["dotnet", "AuthService.dll"]
