# Etapa 1: Compilación de la aplicación con .NET 10 SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ["PaginaPruebaUCSD_V2.csproj", "./"]
RUN dotnet restore "PaginaPruebaUCSD_V2.csproj"

# Copiar todo el código fuente y publicar la aplicación en modo Release
COPY . .
RUN dotnet publish "PaginaPruebaUCSD_V2.csproj" -c Release -o /app/publish

# Etapa 2: Servidor Nginx ligero para servir los archivos estáticos de Blazor
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copiar los archivos compilados desde la etapa anterior
COPY --from=build /app/publish/wwwroot .

# Copiar la configuración personalizada de Nginx para manejar las rutas SPA de Blazor
COPY nginx.conf /etc/nginx/conf.d/default.conf