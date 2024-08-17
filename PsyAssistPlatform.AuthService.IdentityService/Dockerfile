#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["psy-assist-platform-identity/PsyAssistPlatform.AuthService.IdentityService/PsyAssistPlatform.AuthService.IdentityService.csproj", "psy-assist-platform-identity/PsyAssistPlatform.AuthService.IdentityService/"]
COPY ["psy-assist-platform-identity/PsyAssistPlatform.AuthService.Application/PsyAssistPlatform.AuthService.Application.csproj", "psy-assist-platform-identity/PsyAssistPlatform.AuthService.Application/"]
COPY ["psy-assist-platform-identity/PsyAssistPlatform.AuthService.Domain/PsyAssistPlatform.AuthService.Domain.csproj", "psy-assist-platform-identity/PsyAssistPlatform.AuthService.Domain/"]
COPY ["psy-assist-platform-identity/PsyAssistPlatform.AuthService.Persistence/PsyAssistPlatform.AuthService.Persistence.csproj", "psy-assist-platform-identity/PsyAssistPlatform.AuthService.Persistence/"]
RUN dotnet restore "./psy-assist-platform-identity/PsyAssistPlatform.AuthService.IdentityService/PsyAssistPlatform.AuthService.IdentityService.csproj"
COPY . .
WORKDIR "/src/psy-assist-platform-identity/PsyAssistPlatform.AuthService.IdentityService"
RUN dotnet build "./PsyAssistPlatform.AuthService.IdentityService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PsyAssistPlatform.AuthService.IdentityService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PsyAssistPlatform.AuthService.IdentityService.dll"]
COPY ["/psy-assist-platform-identity/PsyAssistPlatform.AuthService.IdentityService/keys/certificate.pfx", "/app/certificate.pfx"]
