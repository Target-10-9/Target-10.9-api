# --- Étape 1 : build de la solution entière ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie de la solution et de tous les projets
COPY ./Target10.9-api.sln ./
COPY ./Target10.9.Business/Target10.9.Business.csproj ./Target10.9.Business/
COPY ./Target10.9.Persistence/Target10.9.Persistence.csproj ./Target10.9.Persistence/
COPY ./Target10.9.Api/Target10.9.Api.csproj    ./Target10.9.Api/

# Restore sur la solution
RUN dotnet restore "./Target10.9-api.sln"

# Copie du reste du code
COPY . .

# Publication uniquement du projet API
WORKDIR /src/Target10.9.Api
RUN dotnet publish -c Release -o /app/publish

# --- Étape 2 : runtime allégé ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Target10.9.Api.dll"]
