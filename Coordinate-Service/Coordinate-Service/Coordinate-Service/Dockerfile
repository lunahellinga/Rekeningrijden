FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Coordinate-Service/Coordinate-Service.csproj", "Coordinate-Service/"]
RUN dotnet restore "Coordinate-Service/Coordinate-Service.csproj"
COPY . .
WORKDIR "/src/Coordinate-Service"
RUN dotnet build "Coordinate-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Coordinate-Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Coordinate-Service.dll"]
