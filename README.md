# InventoryControlSystem_MVC

A modern **ASP.NET Core MVC (.NET 8)** inventory management system built with **Entity Framework Core**, clean architecture principles, and full CRUD operations for products, categories, and stock tracking. Designed as a professional portfolio project demonstrating real-world MVC patterns, dependency injection, layered architecture, and database-first/EF Core workflows.

---

## 🚀 Features
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core with SQL Server
- Clean, layered architecture (Controllers → Services → Data)
- Full CRUD for inventory entities
- Dependency Injection throughout the application
- Strong separation of concerns
- Responsive Razor Views
- Local SQL Server database integration

---

## 🛠️ Tech Stack
**Backend**
- ASP.NET Core MVC (.NET 8)
- C#
- Entity Framework Core
- SQL Server 25

**Frontend**
- Razor Views
- Bootstrap 5

**Tools**
- Visual Studio / VS Code
- SSMS
- Git & GitHub

```mermaid
sequenceDiagram
    participant Client
    participant Controller as CategoryController
    participant UnitOfWork
    participant Repository as CategoryRepository
    participant DbContext
    participant Database

    Client->>Controller: GET /admin/category
    activate Controller
    Controller->>UnitOfWork: Category.GetAll()
    activate UnitOfWork
    UnitOfWork->>Repository: GetAll()
    activate Repository
    Repository->>DbContext: DbSet<Category>.ToList()
    DbContext->>Database: SELECT * FROM Categories
    Database-->>DbContext: categories
    DbContext-->>Repository: categories
    Repository-->>UnitOfWork: IEnumerable<Category>
    deactivate Repository
    UnitOfWork-->>Controller: categories
    deactivate UnitOfWork
    Controller-->>Client: Category Index View
    deactivate Controller

    Client->>Controller: POST /admin/category/create
    activate Controller
    Controller->>UnitOfWork: Category.Add(entity)
    activate UnitOfWork
    UnitOfWork->>Repository: Add(entity)
    activate Repository
    Repository->>DbContext: DbSet<Category>.Add(entity)
    Repository-->>UnitOfWork: -
    deactivate Repository
    UnitOfWork->>UnitOfWork: Save()
    UnitOfWork->>DbContext: SaveChanges()
    DbContext->>Database: INSERT INTO Categories
    Database-->>DbContext: success
    DbContext-->>UnitOfWork: -
    UnitOfWork-->>Controller: -
    deactivate UnitOfWork
    Controller-->>Client: Redirect to Index
    deactivate Controller

```
