# === Étape 1 : build de la solution ===
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1) Copier la solution et les csproj pour un restore intelligent
COPY Target10.9-api.sln                          ./
COPY Target10.9.Business/Target10.9.Business.csproj    Target10.9.Business/
COPY Target10.9.Persistence/Target10.9.Persistence.csproj Target10.9.Persistence/
COPY Target10.9.Api/Target10.9.Api.csproj          Target10.9.Api/
RUN dotnet restore "Target10.9-api.sln"

# 2) Copier tout le code et publier l’API (inclut Persistence & Business)
COPY . .
WORKDIR "/src/Target10.9.Api"
RUN dotnet publish -c Release -o /app/publish

# === Étape 2 : image runtime allégée ===
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# 1) Copier l’artefact publié
COPY --from=build /app/publish .

# Installer postgresql-client pour pg_isready
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# 2) Créer un user non-root (optionnel mais recommandé)
RUN addgroup --system appgroup \
 && adduser  --system --ingroup appgroup appuser
USER appuser

# 3) Entrypoint : attendre la BDD puis démarrer l’API
#    On utilise 'postgres' comme hostname tel que défini dans docker-compose.yml
ENTRYPOINT ["sh", "-c", "\
  echo '⏳ Waiting for PostgreSQL at postgres:5432…' && \
  until pg_isready -h postgres -U \"$${POSTGRES_USER}\"; do \
    sleep 2; \
  done && \
  echo '✅ PostgreSQL is up — launching API' && \
  exec dotnet Target10.9.Api.dll \
"]
