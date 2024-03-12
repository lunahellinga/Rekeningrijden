FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Router-Api/Router-Api.csproj", "Router-Api/"]
RUN dotnet restore "Router-Api/Router-Api.csproj"
COPY . .
WORKDIR "/src/Router-Api"
RUN dotnet build "Router-Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Router-Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#ENV ASPNETCORE_URLS http://*:8080
ENTRYPOINT ["dotnet", "Router-Api.dll"]
