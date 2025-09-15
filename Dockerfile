FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ApiNovaAnalyzer/ApiNovaAnalyzer.csproj ApiNovaAnalyzer/
COPY ApiNovaAnalyzer.sln ./
RUN dotnet restore ApiNovaAnalyzer.sln

COPY . .
RUN dotnet publish ApiNovaAnalyzer/ApiNovaAnalyzer.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ApiNovaAnalyzer.dll"]
