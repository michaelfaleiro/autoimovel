FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/AutoImovel.Shared/AutoImovel.Shared.csproj src/AutoImovel.Shared/
COPY src/AutoImovel.UI.Shared/AutoImovel.UI.Shared.csproj src/AutoImovel.UI.Shared/
COPY src/AutoImovel.Admin/AutoImovel.Admin.csproj src/AutoImovel.Admin/
RUN dotnet restore src/AutoImovel.Admin/AutoImovel.Admin.csproj

COPY src/AutoImovel.Shared/ src/AutoImovel.Shared/
COPY src/AutoImovel.UI.Shared/ src/AutoImovel.UI.Shared/
COPY src/AutoImovel.Admin/ src/AutoImovel.Admin/
RUN dotnet publish src/AutoImovel.Admin/AutoImovel.Admin.csproj -c Release -o /app/publish

FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY src/nginx/admin.nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
