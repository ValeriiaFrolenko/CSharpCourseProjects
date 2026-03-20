# Cinema Manager

.NET MAUI app for managing cinema halls and movie sessions with MVVM architecture and IoC.

## Features

- View list of cinema halls
- View hall details (type, capacity) with sessions schedule
- View session details (movie, genre, start/end time, duration)

## Architecture

**3-tier architecture with clear separation of concerns:**

### Layer 1: Repositories
- **CinemaManager.Storage** - In-memory data storage
- **CinemaManager.Repositories** - Data access through interfaces
- **CinemaManager.DBModels** - Database models (HallDBModel, SessionDBModel)

Returns DB Models.

### Layer 2: Services
- **CinemaManager.Services** - Business logic with interfaces
- **CinemaManager.DTOs** - Data Transfer Objects:
  - `HallListDTO` - for list view (Id, Name, NumberOfSessions)
  - `HallDetailsDTO` - for details (Id, Name, NumberOfSeats, CinemaHallType)
  - `SessionListDTO` - for session list (Id, MovieName, StartTime)
  - `SessionDetailsDTO` - full details (all fields + computed EndTime)

Converts DB Models → DTO Models.

### Layer 3: UI
- **CinemaManager** - MAUI project with Pages, ViewModels, Navigation
- **CinemaManager.Common** - Shared enums (CinemaHallType, FilmGenre)

Works only with Services and DTOs. No direct access to Repositories or DB Models.

## Patterns & Principles

- **MVVM** - Views (.xaml), ViewModels (logic), Code-behind (InitializeComponent + BindingContext only)
- **Dependency Injection** - All dependencies via constructor, registered in CompositionRoot
- **SOLID** - Dependency Inversion (interfaces), Single Responsibility, Separation of Concerns

## Project Structure

```
CinemaManager/
├── CinemaManager              # Main MAUI app (Pages, ViewModels, Navigation)
├── CinemaManager.DBModels     # DB models
├── CinemaManager.DTOs         # Data Transfer Objects
├── CinemaManager.Repositories # Data access interfaces + implementations
├── CinemaManager.Services     # Business logic interfaces + implementations
├── CinemaManager.Storage      # In-memory storage
└── CinemaManager.Common       # Shared enums
```

## Initial Data

- **3 halls:** IMAX (250 seats), VIP (50 seats), Standard 2D (150 seats)
- **12 sessions:** Avatar, Dune, Oppenheimer, Interstellar, Inception, The Dark Knight, etc.

## Navigation

1. **HallsPage** - list of all halls
2. Tap hall → **HallDetailsPage** - hall details + sessions
3. Tap session → **SessionDetailsPage** - full session info

Routes: `HallsPage` → `HallsPage/HallDetailsPage` → `HallsPage/HallDetailsPage/SessionDetailsPage`

## How to Run

1. Open `CinemaManager.sln` in **Visual Studio 2022**
2. Ensure .NET MAUI workload is installed
3. Set **CinemaManager** as startup project
4. Select platform (Windows Machine or Android Emulator)
5. Press F5

## Tech Stack

- .NET MAUI
- C# 12
- CommunityToolkit.Mvvm
- Microsoft.Extensions.DependencyInjection
