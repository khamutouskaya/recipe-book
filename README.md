# Książka Kucharska — Recipe Book (client–server)

🇬🇧 English | [🇵🇱 Polski](README.pl.md)

A desktop recipe book built in C# as a client–server application.
The WPF client talks to a TCP server over a custom protocol; the server stores data in MySQL.

## Features
- Browse recipes by category (soups, main dishes, desserts, snacks)
- View a recipe with its ingredients
- Add recipes, products and ingredients
- Products with nutrition data (calories, protein, fat, carbs)

## Architecture
```
ClienttApp (WPF)  ──TCP, custom protocol──▶  ServerApp  ──▶  MySQL
        └──────────── CommonEntities (shared models & commands) ────────────┘
```
- **ClienttApp** — WPF client: views, components, TCP client protocol
- **ServerApp** — TCP server, request handling, repositories (Repository pattern) over MySQL
- **CommonEntities** — models (`Recipe`, `Product`, `Category`, …) and the `Command` enum shared by client and server

## Tech stack
C#, .NET, WPF, TCP sockets, MySQL (MySql.Data)

## Getting started
1. Install MySQL and run `GenerationScript.sql` to create the `recipe_database` schema.
2. Set your MySQL connection string in `ServerApp/DB/DBManager.cs`
   (default: `server=127.0.0.1;uid=root;pwd=root;database=recipe_database;`).
3. Open `ServerApp/KsiazkaKucharskaSolution.sln` in Visual Studio.
4. Start **ServerApp** first, then **ClienttApp** (or set multiple startup projects).

## Screenshots
_Coming soon._
