# Cinema Manager

A cross-platform .NET MAUI application for managing cinema halls and movie sessions. 

## Project Structure

- **CinemaManager** - The main .NET MAUI application (UI, Pages, Navigation).
- **CinemaManager.Composition** - Dependency Injection (IoC) configuration (`CompositionRoot`).
- **CinemaManager.Services** - Business logic and data mapping services (`HallStorageService`, `SessionStorageService`).
- **CinemaManager.Storage** - In-memory data storage context and data initialization.
- **CinemaManager.DBModels** - Data storage models (raw data only, no computed fields or complex object references).
- **CinemaManager.UIModels** - UI/Display models (includes computed fields like `TotalDurationOfSessions`, `EndTime`, and related session collections).
- **CinemaManager.DTOs** - Data Transfer Objects used for lightweight data passing (e.g., list items).
- **CinemaManager.Common** - Shared enumerations (`CinemaHallType`, `FilmGenre`).
- **CinemaManagerConsole** - Legacy console application for basic testing.


## How to Run

1. Open the solution in **Visual Studio 2022** (ensure the .NET MAUI workload is installed).
2. Set **`CinemaManager`** as the startup project.
3. Select your target platform from the debug dropdown (e.g., **Windows Machine** or **Android Emulator**).
4. Run the application (F5).

## Initial Data

The storage is pre-populated with realistic initial data:
- **3 cinema halls** (IMAX, VIP, Standard2D)
- **12 movie sessions** with varying genres, dates, and durations.
