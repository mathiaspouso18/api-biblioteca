# Fase 1: Compilación
# Usamos la imagen del SDK de .NET 8 para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos los archivos .csproj y restauramos las dependencias primero
# Esto aprovecha el cache de Docker y acelera futuras compilaciones
COPY ["MiBibliotecaApi/MiBibliotecaApi.csproj", "MiBibliotecaApi/"]
RUN dotnet restore "MiBibliotecaApi/MiBibliotecaApi.csproj"

# Copiamos el resto del código fuente
COPY . .
WORKDIR "/src/MiBibliotecaApi"
# Compilamos y publicamos la aplicación en modo Release
RUN dotnet publish "MiBibliotecaApi.csproj" -c Release -o /app/publish

# Fase 2: Ejecución
# Usamos la imagen de ASP.NET, que es más ligera porque no tiene el SDK
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
# El ENTRYPOINT es el comando que se ejecutará cuando el contenedor inicie
ENTRYPOINT ["dotnet", "MiBibliotecaApi.dll"]