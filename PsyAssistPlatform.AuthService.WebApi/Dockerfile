#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PsyAssistPlatform.AuthService.WebApi/PsyAssistPlatform.AuthService.WebApi.csproj", "PsyAssistPlatform.AuthService.WebApi/"]
COPY ["PsyAssistPlatform.AuthService.Application/PsyAssistPlatform.AuthService.Application.csproj", "PsyAssistPlatform.AuthService.Application/"]
COPY ["PsyAssistPlatform.AuthService.Domain/PsyAssistPlatform.AuthService.Domain.csproj", "PsyAssistPlatform.AuthService.Domain/"]
COPY ["PsyAssistPlatform.AuthService.Persistence/PsyAssistPlatform.AuthService.Persistence.csproj", "PsyAssistPlatform.AuthService.Persistence/"]
RUN dotnet restore "./PsyAssistPlatform.AuthService.WebApi/PsyAssistPlatform.AuthService.WebApi.csproj"
COPY . .
WORKDIR "/src/PsyAssistPlatform.AuthService.WebApi"
RUN dotnet build "./PsyAssistPlatform.AuthService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PsyAssistPlatform.AuthService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PsyAssistPlatform.AuthService.WebApi.dll"]
COPY ["PsyAssistPlatform.AuthService.WebApi/keys/certificate.pfx", "/app/certificate.pfx"]