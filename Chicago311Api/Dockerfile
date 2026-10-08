FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY Chicago311Api.csproj .
RUN dotnet restore Chicago311Api.csproj

COPY . .

RUN dotnet publish Chicago311Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Chicago311Api.dll"]
