# Imagen base de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y proyectos
COPY *.sln ./
COPY ApiNovaAnalyzer/ApiNovaAnalyzer.csproj ApiNovaAnalyzer/

# Restaurar usando la solución
RUN dotnet restore

# Copiar todo y compilar
COPY . .
WORKDIR /src/ApiNovaAnalyzer
RUN dotnet publish -c Release -o /app

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ApiNovaAnalyzer.dll"]
