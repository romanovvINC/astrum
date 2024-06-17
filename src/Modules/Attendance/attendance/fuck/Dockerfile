FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["fuck/fuck.csproj", "fuck/"]
RUN dotnet restore "fuck/fuck.csproj"
COPY . .
WORKDIR "/src/fuck"
RUN dotnet build "fuck.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fuck.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fuck.dll"]
