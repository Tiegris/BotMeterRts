FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Source/BotMeter.Web/BotMeter.Web.csproj", "BotMeter.Web/"]
RUN dotnet restore "Source/BotMeter.Web/BotMeter.Web.csproj"
COPY . .
WORKDIR "/src/BotMeter.Web"
RUN dotnet build "BotMeter.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BotMeter.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BotMeter.Web.dll"]
