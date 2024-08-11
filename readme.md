



# Описание проектов

## Core

### PsyAssistPlatform.AuthService.Application

### PsyAssistPlatform.AuthService.Domain

Содержит доменные модели

## DatabaseProxyService

WebApi проект с контроллером, для работы с базой данных на одном физической машине. 

## IdentityService

WebApi проект с реализацией IdentityServer4.
Не содержит базу данных пользователей и ролей. Обращается к ней через API DatabaseProxyService.
Создан для запуска в контейнере

## Infrastructure

Проект для работы с базой данный клиентов и ролей с использованием Entity Framework Core.

### Применение миграций
```
dotnet ef migrations add InitialCreate -s ../PsyAssistPlatform.AuthService.WebApi/PsyAssistPlatform.AuthService.WebApi.csproj
dotnet ef database update -s ../PsyAssistPlatform.AuthService.WebApi/PsyAssistPlatform.AuthService.WebApi.csproj
```

## Presentation

### PsyAssistPlatform.AuthService.WebApi

Классическая реализация IdentityServer4 с базой данный Пользователей и Ролей, с использование ASP.Net Core Identity