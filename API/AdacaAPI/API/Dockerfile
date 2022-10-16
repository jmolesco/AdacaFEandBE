#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoreApi.csproj", "."]

RUN dotnet restore "./CoreApi.csproj"
COPY . .
RUN dotnet build "CoreApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoreApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Adaca.Api.dll"]