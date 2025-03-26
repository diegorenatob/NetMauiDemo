# MyBlog - DDD & Local SQLite Example in .NET MAUI (.NET 8)

**Author**: Diego Belapatiño

This repository demonstrates a multi-layer, Domain-Driven Design (DDD) approach in a .NET MAUI application (targeting **.NET 8**), using a local SQLite database for offline storage. It includes a simple example of fetching and storing blog posts, applying Entity Framework Core migrations, and running unit tests with NUnit.

---

## Overview

- **.NET MAUI** (Presentation Layer)  
- **Domain Layer** containing core Entities (e.g., `Post`) and Domain logic  
- **Application Layer** (Services, DTOs, Interfaces) that orchestrates use cases and bridges Domain & Infrastructure  
- **Infrastructure Layer** for data persistence (SQLite via EF Core) and external service calls  
- **Tests** (NUnit + Moq) verifying Domain Entities and Application Services logic  

### Why DDD?

1. **Separation of Concerns**: Domain logic stays within Entities/Domain Services, isolated from UI or persistence details.  
2. **Resilience**: Changes in UI or Infrastructure (e.g., switching from SQLite to another store) do not break core business rules.  
3. **Clean Architecture**: Each layer has a single responsibility, making the system more maintainable.

---

## Layers Explanation

1. **Domain Layer**  
   - **Entities**: For example, `Post` with properties (`Id`, `Title`, `Body`) and domain methods (`UpdateTitle`, etc.).  
   - **Interfaces** (e.g., `IPostRepository`): Abstract definitions that describe how data is accessed, without any implementation details.  
   - **Pure business logic** (no knowledge of databases or UI frameworks).

2. **Application Layer**  
   - **Services** (e.g., `PostService`): Coordinates use cases, mapping between Domain and other layers.  
   - **DTOs** (e.g., `PostDto`): Used for data transfer to/from UI or external services.  
   - Depends only on **Domain** (through interfaces).

3. **Infrastructure Layer**  
   - **Data** (e.g., `MyBlogDbContext` for EF Core), **Repositories** (implementing `IPostRepository`), **Migrations** for SQLite.  
   - **External Services** (e.g., an `IExternalPostService` implementation that calls JSONPlaceholder).  
   - Orchestrates all external concerns: databases, file systems, or external APIs.

4. **Presentation Layer** (`.NET MAUI`)  
   - **Views, ViewModels**: The front-end of the application, using MVVM patterns.  
   - **MauiProgram.cs** for app configuration and DI setup.  
   - Calls Application Services to fetch or persist data, injects them into ViewModels.

---

## Getting Started

### Prerequisites
1. **.NET 8 SDK**  
2. One of the following IDEs:
   - **Visual Studio 2022** (with .NET MAUI workload installed)  
   - **JetBrains Rider** (supports .NET MAUI in recent versions)  
   - **VS Code** (with the .NET MAUI extension/tooling)  
3. (Optional) **dotnet-ef** CLI tool for generating new EF migrations:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

### Steps to Run

1. **Clone the repository**:
   ```bash
   git clone https://github.com/<your-repo>/NetMauiDemo.git
   cd NetMauiDemo
   ```

2. **Generate or update migrations** *(only if needed)*:
   ```bash
   dotnet ef migrations add InitialCreate \
       --project MyBlog.Infrastructure \
       --startup-project MyBlog.Infrastructure
   ```
   This creates (or updates) the `Migrations` folder in **MyBlog.Infrastructure** with an `InitialCreate.cs`.

3. **Open the solution** in [Visual Studio 2022](https://visualstudio.microsoft.com/), [JetBrains Rider](https://www.jetbrains.com/rider/), or [VS Code](https://code.visualstudio.com/).

4. **Run the application** (`MyBlog.Maui`) on a simulator/device (Android/iOS/Windows):
   - EF Core automatically applies any pending migrations (creating `myblog.db3` locally).
   - The app may fetch posts from a remote source and store them in SQLite for offline usage.

---

## Running the Tests

The **MyBlog.Tests** project (NUnit-based) includes unit tests for:
- **Domain** (`PostTests`) – verifying entity logic.  
- **Application** (`PostServiceTests`) – using Moq to mock the repository/external services.

To run tests from the command line:

```bash
dotnet test
```

Alternatively, you can run them directly inside your IDE (Visual Studio’s Test Explorer, Rider’s Unit Tests panel, etc.).

---

## Project Architecture

```
NetMauiDemo
├─ MyBlog.Domain
│   ├─ Entities
│   │   └─ Post.cs
│   └─ Interfaces
│       └─ IPostRepository.cs
├─ MyBlog.Application
│   ├─ DTOs
│   ├─ Interfaces
│   └─ Services
│       └─ PostService.cs
├─ MyBlog.Infrastructure
│   ├─ Data
│   │   └─ MyBlogDbContext.cs
│   ├─ Repositories
│   │   └─ PostRepository.cs
│   └─ Migrations
├─ MyBlog.Maui
│   ├─ Views
│   ├─ ViewModels
│   └─ MauiProgram.cs
└─ MyBlog.Tests
    ├─ Domain
    │   └─ PostTests.cs
    └─ Application
        └─ PostServiceTests.cs
```

- **MyBlog.Maui**: UI, main DI config (`MauiProgram.cs`), and MVVM.  
- **MyBlog.Infrastructure**: EF Core DbContext, Repositories, Migrations.  
- **MyBlog.Domain**: Entities (`Post`), domain logic, domain interfaces (`IPostRepository`).  
- **MyBlog.Application**: Services (`PostService`), DTOs, or other higher-level logic.  
- **MyBlog.Tests**: NUnit tests covering Domain and Application layers.

---

## Contact

If you have any questions or would like to discuss job opportunities, feel free to email:

**Diego Belapatiño**  
**[diegorenatobf@gmail.com](mailto:diegorenatobf@gmail.com)**  

Enjoy building robust, offline-capable .NET MAUI apps with a clean DDD architecture!
