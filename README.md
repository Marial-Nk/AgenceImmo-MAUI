# AgenceImmo - Application MAUI

Application de gestion d'une agence immobilière développée avec .NET MAUI dans le cadre du cours de Programmation et Gestion de Bases de Données (ESA, 2025-2026).

## Structure du projet

```
AgenceImmo/
├── BaseMauiApp/              # Application MAUI (interface mobile/desktop)
├── AgenceImmo.DataAccess/    # Couche d'accès aux données (Entity Framework Core)
│   ├── Entities/             # Modèles de données (Bien, Personne, Adresse, ...)
│   ├── Enums/                # Énumérations (TypeBien, StatutBien, TypeContrat, ...)
│   └── Migrations/           # Migrations EF Core
├── AgenceImmo.Business/      # Couche métier (services et interfaces)
│   ├── Interfaces/           # IBienService, IPersonneService, IUtilisateurService
│   └── Services/             # Implémentations des services
└── AgenceImmo.TestConsole/   # Projet de test console
```

## Technologies

- **.NET 9** / **C#**
- **.NET MAUI** — interface multiplateforme
- **Entity Framework Core 9** — ORM
- **SQL Server** — base de données
- **Microsoft.Extensions.Configuration** — gestion de la configuration

## Prérequis

- Visual Studio 2022 (avec workload .NET MAUI)
- SQL Server (local ou distant)
- .NET 9 SDK

## Configuration

Créez un fichier `appsettings.json` dans `AgenceImmo.TestConsole/` avec la chaîne de connexion :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=AgenceImmo;Trusted_Connection=True;"
  }
}
```

## Lancer les migrations

```bash
dotnet ef database update --project AgenceImmo.DataAccess --startup-project AgenceImmo.TestConsole
```

## Auteur

Henriette Nkondi — ESA 2025-2026
