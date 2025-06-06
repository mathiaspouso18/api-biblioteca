# Fase 1: Compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# --- CAMBIO 1 ---
# Copiamos solo el archivo .csproj a la raíz de /src.
# La ruta de origen ahora es "MiBibliotecaApi.csproj" (sin la carpeta).
# El destino es "./", que significa el directorio de trabajo actual (/src).
COPY ["MiBibliotecaApi.csproj", "./"]
# Restauramos las dependencias usando la ruta corregida.
RUN dotnet restore "MiBibliotecaApi.csproj"

# Copiamos todo lo demás.
COPY . .
# Ya estamos en el directorio correcto (/src), así que no necesitamos cambiarlo.

# --- CAMBIO 2 ---
# El comando de publicación ahora usa la ruta correcta al archivo .csproj.
RUN dotnet publish "MiBibliotecaApi.csproj" -c Release -o /app/publish

# Fase 2: Ejecución (esta parte no cambia)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MiBibliotecaApi.dll"]