# Cinema Manager

Console application for managing cinema halls and movie sessions.

## Project Structure

- **CinemaManager_Common** - Enumerations (CinemaHallType, FilmGenre)
- **CinemaManager_DBModels** - Data storage models (without computed fields or collections)
- **CinemaManager_UIModels** - UI/Display models (with computed fields and session collections)
- **CinemaManager_Storage** - Storage services and in-memory data storage
- **CinemaManagerConsole** - Console application

## Architecture Notes

### Separation of Concerns
- **DBModels**: Store raw data only, no computed fields, no cross-references between entities
- **UIModels**: Include computed fields (e.g., `TotalDurationOfSessions`, `EndTime`) and entity relationships
- **Storage**: Internal static class with public fields, accessible only through service classes

### Storage Services
- `HallStorageService`: Manages cinema hall data retrieval and conversion from DB to UI models
- `SessionStorageService`: Manages movie session data retrieval

## How to Run

1. Open the solution in Visual Studio
2. Set `CinemaManagerConsole` as the startup project
3. Run the application (F5)

## Initial Data

The storage is pre-populated with:
- 3 cinema halls (IMAX, VIP, Standard2D)
- 12 movie sessions with realistic data