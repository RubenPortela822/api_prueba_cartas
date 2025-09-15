# Imagen base de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y csproj
COPY ApiNovaAnalyzer/ApiNovaAnalyzer.csproj ApiNovaAnalyzer/
COPY ApiNovaAnalyzer.sln ./

# Restaurar dependencias
RUN dotnet restore ApiNovaAnalyzer.sln

# Copiar todo el código y compilar
COPY . .
RUN dotnet publish ApiNovaAnalyzer/ApiNovaAnalyzer.csproj -c Release -o /app

# Imagen final de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ApiNovaAnalyzer.dll"]
