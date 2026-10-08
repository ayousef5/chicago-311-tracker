FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY Chicago311Api/Chicago311Api.csproj Chicago311Api/
RUN dotnet restore Chicago311Api/Chicago311Api.csproj

COPY Chicago311Api/ Chicago311Api/

WORKDIR /src/Chicago311Api
RUN dotnet publish Chicago311Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Chicago311Api.dll"]