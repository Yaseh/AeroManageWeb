# AeroManage — Gestion Aéroportuaire

Application de gestion aéroportuaire développée en **ASP.NET Core 10** (backend) et **Angular 21** (frontend), suivant les principes de la **Clean Architecture**.

---

## Fonctionnalités

Le projet propose **6 fonctionnalités CRUD complètes** (création, consultation, modification, suppression) :

1. **Avions** — gestion de la flotte (modèle, capacité, statut)
2. **Aéroports** — gestion des aéroports (code IATA, nom, ville, pays)
3. **Personnel** — gestion du personnel navigant (commandants, copilotes, etc.)
4. **Passagers** — gestion des passagers (nom, prénom, nationalité)
5. **Vols** — gestion des vols (numéro, dates, statuts, aéroports, avion, commandant)
6. **Réservations** — gestion des réservations (numéro de siège, vol, passager)

---

## Prérequis

| Outil | Version minimale | Vérification |
|---|---|---|
| .NET SDK | **10.0** | `dotnet --version` |
| Node.js | **20 LTS ou supérieur** | `node --version` |
| npm | **11.x** | `npm --version` |
| Angular CLI | **21.x** | `ng version` |

> **Base de données** : SQLite — aucune installation requise, le fichier est créé automatiquement.

---

## Installation

### 1. Cloner le dépôt

```bash
git clone <url-du-depot>
cd "Session learning aerport c"
```

### 2. Installer les dépendances Angular

```bash
cd AeroManageWeb/AeroManage.Web
npm install
```

### 3. Restaurer les packages .NET

```bash
cd ../AeroManage.Api
dotnet restore
```

---

## Base de données

La base de données **SQLite** est créée **automatiquement** au premier démarrage de l'API.  
Le script `Database/schema.sql` est exécuté par `Program.cs` à chaque lancement — les tables sont créées avec `CREATE TABLE IF NOT EXISTS`, donc les données existantes sont préservées.

> **Note importante** : Si vous avez une base `aeromanag.db` issue d'une version antérieure du projet (colonne `Numerosiege` en `INTEGER`), supprimez le fichier `AeroManageWeb/Database/aeromanag.db` avant de lancer l'API. La base sera recréée automatiquement avec le bon schéma (`Numerosiege TEXT`).

Aucune configuration de chaîne de connexion n'est nécessaire : le chemin relatif est déjà configuré dans `AeroManage.Api/appsettings.json` :

```json
"ConnectionStrings": {
  "Default": "Data Source=../Database/aeromanag.db"
}
```

---

## Lancement du backend

> **Attention** : la commande `dotnet run` doit être exécutée **depuis le dossier `AeroManage.Api`**, pas depuis la racine de la solution.

```bash
cd AeroManageWeb/AeroManage.Api
dotnet run
```

L'API démarre sur `http://localhost:5287`.  
La documentation interactive (Scalar) est accessible à l'adresse :

```
http://localhost:5287/scalar/v1
```

---

## Lancement du frontend

> **Attention** : la commande `ng serve` doit être exécutée **depuis le dossier `AeroManage.Web`**, pas depuis la racine de la solution.

```bash
cd AeroManageWeb/AeroManage.Web
ng serve
```

L'application Angular est accessible à l'adresse :

```
http://localhost:4200
```

> Le backend doit être démarré **avant** le frontend pour que les appels API fonctionnent.

---

## Lancement complet (résumé)

Ouvrez **deux terminaux** :

**Terminal 1 — Backend :**
```bash
cd "chemin/vers/Session learning aerport c/AeroManageWeb/AeroManage.Api"
dotnet run
```

**Terminal 2 — Frontend :**
```bash
cd "chemin/vers/Session learning aerport c/AeroManageWeb/AeroManage.Web"
ng serve
```

Puis ouvrir `http://localhost:4200` dans un navigateur.

---

## Comptes de test

L'application ne nécessite **pas d'authentification**. Toutes les fonctionnalités sont accessibles directement.

---

## Architecture du projet

```
AeroManageWeb/
│
├── AeroManage.Core/               ← Couche métier (aucune dépendance externe)
│   ├── Entities/                  ← Entités du domaine
│   ├── DTOs/                      ← Objets de transfert de données
│   ├── Interfaces/                ← Contrats (IXRepository, IXService)
│   └── Services/                  ← Logique métier, mapping Entité ↔ DTO
│
├── AeroManage.Infrastructure/     ← Couche d'accès aux données
│   ├── Data/                      ← Factory de connexion SQLite
│   └── Repositories/              ← Implémentations Dapper (SQL paramétré)
│
├── AeroManage.Api/                ← Couche présentation (HTTP)
│   ├── Controllers/               ← Points d'entrée REST, aucune logique métier
│   ├── Program.cs                 ← DI, CORS, création automatique de la BD
│   └── appsettings.json           ← Configuration
│
├── AeroManage.Web/                ← Frontend Angular 21
│   └── src/app/
│       ├── components/            ← Un composant par entité (signals, @if/@for)
│       ├── Services/              ← Services HTTP (HttpClient + Observables)
│       ├── models/                ← Interfaces TypeScript (camelCase)
│       ├── app.routes.ts          ← Routage Angular
│       └── app.html               ← Layout principal (sidebar + router-outlet)
│
└── Database/
    └── schema.sql                 ← Schéma SQLite (exécuté automatiquement)
```

---

## Stack technique

| Couche | Technologie |
|---|---|
| Backend | ASP.NET Core 10 |
| ORM | Dapper 2.x |
| Base de données | SQLite (Microsoft.Data.Sqlite) |
| Documentation API | Scalar (`/scalar/v1`) |
| Frontend | Angular 21.2 |
| Langage frontend | TypeScript 5.9 |
| Formulaires | ReactiveFormsModule |
| HTTP | HttpClient (Observables RxJS) |
| État des composants | Angular Signals (natif, pas de bibliothèque externe) |

---

## Principes respectés

- **Clean Architecture** : dépendances orientées vers le centre (Core ← Infrastructure, Core ← API)
- **Repository Pattern** : les repositories manipulent uniquement des Entités ; les Services mappent vers des DTOs
- **Injection de dépendances** : toutes les classes reçoivent leurs dépendances via le constructeur, enregistrées dans `Program.cs`
- **Séparation des responsabilités** : les contrôleurs ne contiennent aucune logique métier ni SQL
- **Nouvelle syntaxe Angular** : `@if`, `@for`, `@empty` utilisés dans tous les templates
- **Aucune bibliothèque d'état externe** : état géré uniquement par des Signals Angular natifs et des Services
