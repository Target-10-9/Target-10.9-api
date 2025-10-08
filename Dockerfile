# ===== Étape Build =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier la solution + csproj (les 4 projets)
COPY Target10.9-api.sln                                              ./
COPY Target10.9.Api/Target10.9.Api.csproj                             Target10.9.Api/
COPY Target10.9.Business/Target10.9.Business.csproj                   Target10.9.Business/
COPY Target10.9.Persistence/Target10.9.Persistence.csproj             Target10.9.Persistence/
COPY Target10.9.Migrator/Target10.9.Migrator.csproj                   Target10.9.Migrator/

RUN dotnet restore "Target10.9-api.sln"

# Copier tout le code
COPY . .

# Publier l'API et le Migrator (Release)
RUN dotnet publish Target10.9.Api/Target10.9.Api.csproj -c Release -o /out/api
RUN dotnet publish Target10.9.Migrator/Target10.9.Migrator.csproj -c Release -o /out/migrator

# ===== Image Runtime Lambda .NET 8 =====
FROM public.ecr.aws/lambda/dotnet:8

# Déposer les artefacts au LAMBDA_TASK_ROOT
COPY --from=build /out/api/      ${LAMBDA_TASK_ROOT}/
COPY --from=build /out/migrator/ ${LAMBDA_TASK_ROOT}/

# Par défaut, l’image lance l'API ASP.NET Core dans Lambda
# (grâce à Amazon.Lambda.AspNetCoreServer.Hosting dans Program.cs)
# Pour .NET 8, le "handler" de type image peut être simplement l'assembly.
CMD ["Target10.9.Api"]
