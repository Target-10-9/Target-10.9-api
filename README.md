# Target-10.9-api
🎯 Target-10.9 est un système de cible connectée pour le tir sportif. Le projet combine des capteurs physiques, une API, et des interfaces web/mobile pour offrir une expérience interactive en temps réel. Chaque tir est analysé grâce aux vibrations sur la cible, permettant d’afficher le score et la position exacte de l’impact en direct.

## 🔧Composants du projet
Target-10.9-api
API principale en charge de recevoir et traiter les données des capteurs, gérer les scores, les sessions, et exposer des endpoints pour les interfaces front-end.

## 📌 Objectifs du projet (MVP)
- ✅ Détecter les impacts sur la cible via capteurs de vibrations.
- ✅ Calculer et afficher le score en temps réel.
- ✅ Envoyer les données à l’API.
- ✅ Afficher la cible en 3D avec les impacts visibles sur l’interface web/mobile.
- ✅ Sauvegarder les sessions et afficher des statistiques.

## 🚀 Stack technique



## Tuto de Camille 

Pour mettre à jour l'API
```bash
  docker-compose up -d --build api
```

### POUR FABIEN !!!
```bash
  ssh -i "TargetKeyGen1.pem" ec2-user@ec2-13-38-34-156.eu-west-3.compute.amazonaws.com
```

### dans le Power Shell de EC2 

pour se connecter a la DB 
```bash
    docker exec -it target10_postgres psql -U target_user -d target_db
```

pour litser les tables 
```bash
    \dt
```

se connecter a l'API héberger sur EC2 : http://13.38.34.156:5000/swagger => a chaque terraform apply l'ip change 

Lien du site  : http://localhost/login <br>
Lien du swagger : http://localhost:5000/swagger/index.html



## Pour mettre en place les migrations :
il faut tellecharger le SDK de dotnet et run manuellement la migration

### Sur EC2 après etre connecter : 

Se situer dans le dosser app : 
```bash
     cd /home/ec2-user/app
```

checker la version de dotnet
```bash
     dotnet --version
```
Au cas où désinstaller 
```bash
     => chercher chat
```
Installer dotnet 8
```bash
     curl -sSL https://dot.net/v1/dotnet-install.sh | sudo bash /dev/stdin --channel 8.0 --install-dir /usr/share/dotnet
```
controler les runtimes :
```bash
     dotnet --list-runtimes
```
résultat :

```bash
     Microsoft.AspNetCore.App 8.0.18 [/home/ec2-user/.dotnet/shared/Microsoft.AspNetCore.App]
     Microsoft.NETCore.App 8.0.18 [/home/ec2-user/.dotnet/shared/Microsoft.NETCore.App]
```

Télécharger dotnet tool : 
```bash
     dotnet tool install --global dotnet-ef --version 8.0.0
     
```

Metre le tout en variable d'environment :
```bash
     export PATH="$PATH:/home/ec2-user/.dotnet:/home/ec2-user/.dotnet/tools"
     echo $PATH
```

Résultat → position de l'élément dans l'environment 
```bash
     dotnet --list-runtimes
```

Voir si le .csproj de persistance existe : 
```bash
     find /home/ec2-user/app -name "*.csproj"
```

Lancer la migration :

```bash
     dotnet ef database update \
  --project Target10.9.Persistence/Target10.9.Persistence.csproj \
  --startup-project Target10.9.Api/Target10.9.Api.csproj
```

Avec ces étapes aller dans le docker et regarder existences des tables 



