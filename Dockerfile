# Imagen base de compilación (.NET 6 SDK)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar el csproj y restaurar dependencias
COPY ApiNovaAnalyzer/ApiNovaAnalyzer.csproj ApiNovaAnalyzer/
RUN dotnet restore ApiNovaAnalyzer/ApiNovaAnalyzer.csproj

# Copiar todo y compilar
COPY . .
WORKDIR /src/ApiNovaAnalyzer
RUN dotnet publish -c Release -o /app

# Imagen final de runtime (.NET 6 Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ApiNovaAnalyzer.dll"]

