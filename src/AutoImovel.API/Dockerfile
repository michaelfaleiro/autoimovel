FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/AutoImovel.Shared/AutoImovel.Shared.csproj src/AutoImovel.Shared/
COPY src/AutoImovel.API/AutoImovel.API.csproj src/AutoImovel.API/
RUN dotnet restore src/AutoImovel.API/AutoImovel.API.csproj

COPY src/AutoImovel.Shared/ src/AutoImovel.Shared/
COPY src/AutoImovel.API/ src/AutoImovel.API/
RUN dotnet publish src/AutoImovel.API/AutoImovel.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080
ENTRYPOINT ["dotnet", "AutoImovel.API.dll"]
