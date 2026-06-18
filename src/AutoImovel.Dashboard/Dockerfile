FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/AutoImovel.Shared/AutoImovel.Shared.csproj src/AutoImovel.Shared/
COPY src/AutoImovel.UI.Shared/AutoImovel.UI.Shared.csproj src/AutoImovel.UI.Shared/
COPY src/AutoImovel.Dashboard/AutoImovel.Dashboard.csproj src/AutoImovel.Dashboard/
RUN dotnet restore src/AutoImovel.Dashboard/AutoImovel.Dashboard.csproj

COPY src/AutoImovel.Shared/ src/AutoImovel.Shared/
COPY src/AutoImovel.UI.Shared/ src/AutoImovel.UI.Shared/
COPY src/AutoImovel.Dashboard/ src/AutoImovel.Dashboard/
RUN dotnet publish src/AutoImovel.Dashboard/AutoImovel.Dashboard.csproj -c Release -o /app/publish

FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY src/nginx/dashboard.nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
