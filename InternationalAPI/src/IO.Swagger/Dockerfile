FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/IO.Swagger/IO.Swagger.csproj", "src/IO.Swagger/"]
RUN dotnet restore "src/IO.Swagger/IO.Swagger.csproj"
COPY . .
WORKDIR "/src/src/IO.Swagger"
RUN dotnet build "IO.Swagger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IO.Swagger.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#ENV ASPNETCORE_URLS http://*:8080
ENTRYPOINT ["dotnet", "IO.Swagger.dll"]
