Steam Game Recommendation API
A C#/.NET backend API that integrates with the Steam Web API, syncs a user's 'owned' game library into SQL Server, allows personal game ratings, and returns ranked game recommendations using a weighted scoring model.

Work in progress

Overview
The Steam Recommendation project ...

Tech Stack
C# / .NET
ASP.NET Core Web API
Entity Framework Core
SQL Server Express
Steam Web API
Repository Pattern
Features
Fetch owned Steam games from the live Steam API
Sync owned games into SQL Server
Retrieve saved games from SQL Server
Submit personal game ratings
Generate ranked game recommendations
API Endpoints
Method - Endpoint - Description

GET /api/SteamGames/owned/{steamUserId} Fetches live owned games from Steam POST /api/SteamGames/sync/{steamUserId} Syncs Steam games into SQL Server GET /api/SteamGames/saved/{steamUserId} Gets saved games from SQL Server POST /api/SteamGames/ratings | Saves or updates a personal game rating GET /api/SteamGames/recommendations/{steamUserId} Returns scored game recommendations

Recommendation Scoring
more to come ...

Configuration
more to come ...

CI/CD
Add rating validation
Verify user owns a game before rating it
Add playtime signal to recommendation scoring
Add genre/tag-based recommendations later