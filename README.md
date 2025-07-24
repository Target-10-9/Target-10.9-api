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
  ssh -i "TargetKeyGen1.pem" ec2-user@ec2-35-180-55-188.eu-west-3.compute.amazonaws.com
```

se connecter a l'API héberger sur EC2 : http://35.180.55.188:5000/swagger

Lien du site  : http://localhost/login <br>
Lien du swagger : http://localhost:5000/swagger/index.html

