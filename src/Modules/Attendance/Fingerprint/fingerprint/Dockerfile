﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FuckWeb/FuckWeb.csproj", "FuckWeb/"]
RUN dotnet restore "FuckWeb/FuckWeb.csproj"
COPY . .
WORKDIR "/src/FuckWeb"
RUN dotnet build "FuckWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FuckWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FuckWeb.dll"]
