# Define la imagen base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia el archivo del proyecto y restaura las dependencias
COPY *.csproj .
RUN dotnet restore

# Copia todos los archivos y construye la aplicación
COPY . .
RUN dotnet publish -c Release -o out

# Define la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/bin/Debug/net6.0 .
ENTRYPOINT ["dotnet", "libraryapi.dll"]
