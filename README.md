# Veat - User Account Management (Veat UAM)

Application lourde de gestion de comptes utilisateurs codées en C# (WPF).

L'application doit contenir les fonctionnalités suivantes : 

- Gestion de base de données relationnelles : WPF C# <---> Microsoft SQL Server
- Authentification SQL Server en mode mixte 
- Une connexion pour chaque utilisateur 
- Gestion des privilèges
- Journaux des transactions

## Fonctionnalités actuelles

### Fonctionnalités principales

- Authentification BDD MSSQL en mode mixte.
- Page de login au démarrage.
- Connexion unique de l'utilisateur à la base de données (Unique ouverture de connexion à la BDD suite à l'authentification).
- Gestion des privilèges (User, Admin et Super Admin).
- Gestion des requêtes utilisateur via logs.

### Fonctionnalités autres

- Système de gestion des mots de passe avec hachage.
- Gestion de l'Authentification utilisateur via login.
- Créer, Modifier, Supprimer un "Utilisateur final".
- Créer, Modifier, Supprimer un "Livreur".
- Créer, Modifier, Supprimer un "Développeur".
- Créer, Modifier, Supprimer un "Restaurant".
- Créer, Modifier, Supprimer un "Utilisateur du service commercial".
- Créer, Modifier, Supprimer un "Utilisateur du service technique".
