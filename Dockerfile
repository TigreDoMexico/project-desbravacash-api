FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY src/*.csproj ./src/
RUN dotnet restore src/TigreDoMexico.DesbravaCash.Api.csproj

COPY src/ ./src/
RUN dotnet publish src/TigreDoMexico.DesbravaCash.Api.csproj -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "TigreDoMexico.DesbravaCash.Api.dll"]
