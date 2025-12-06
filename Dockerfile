# ========================================
# Stage 1 : Build (SDK pour compiler l'application)
# ========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier le fichier de projet et restaurer les dépendances (optimisation du cache Docker)
COPY microstore-front/*.csproj ./microstore-front/
RUN dotnet restore "./microstore-front/microstore-front.csproj"

# Copier tout le code source et compiler
COPY microstore-front/. ./microstore-front/
WORKDIR /src/microstore-front
RUN dotnet build "microstore-front.csproj" -c Release -o /app/build

# ========================================
# Stage 2 : Publish (Créer les artefacts de production)
# ========================================
FROM build AS publish
RUN dotnet publish "microstore-front.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ========================================
# Stage 3 : Runtime (Image finale légère)
# ========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Créer un utilisateur non-root pour des raisons de sécurité
RUN useradd -m -u 1000 appuser && chown -R appuser:appuser /app
USER appuser

# Copier les artefacts publiés depuis le stage publish
COPY --from=publish /app/publish .

# Exposer le port par défaut d'ASP.NET Core
EXPOSE 8080

# Variables d'environnement pour configurer ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Point d'entrée : lancer l'application
ENTRYPOINT ["dotnet", "microstore-front.dll"]
