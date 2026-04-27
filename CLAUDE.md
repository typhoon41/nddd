# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What This Is

`GMForce.NDDD` is a zero-dependency NuGet library providing foundational DDD (Non-Dogmatic Domain-Driven Design) building blocks for .NET 10 applications. It ships pure abstractions and base classes — no framework assumptions, no enforced patterns.

NuGet: `https://www.nuget.org/packages/GMForce.NDDD`  
Repo: `https://github.com/typhoon41/nddd`

## Common Commands

```bash
# Restore (required before build — lock file is enforced)
dotnet restore -r win-x64

# Build (warnings are treated as errors)
dotnet build --no-incremental --configuration Release --no-restore

# Pack NuGet package
dotnet pack
```

There are no test projects — the library is validated through the Azure Pipelines CI.

## CI/CD

Defined in `Deployment/azure-pipelines.yml` + `Deployment/Templates/build.yaml`.

- **Trigger:** pushes to `main`
- **Stage 1 — Build:** restore → build → pack → publish artifact (`NDDD-{BuildNumber}.nupkg`)
- **Stage 2 — NugetPublish:** manual approval gate → push to nuget.org

## Architecture

```
GMForce.NDDD/
├── Concepts/       Base classes: Entity<T>, ValueObject, Enumeration<T>
├── Contracts/      Interfaces: IDomainEvent, IDispatchEvents, IHandleDomainEvents,
│                               IStoreEvents, IUnitOfWork, IPaginateRequest, IAuditUser
├── Abstractions/   Period (sealed record — time range with Contains/InFuture/InPast)
└── Persistance/    EntityDto (ORM base with domain event collection)
```

### Core building blocks

| Type | Purpose |
|------|---------|
| `Entity<T>` | Base entity; equality by ID + runtime type; `T` must be a struct |
| `ValueObject` | Structural equality via `GetAtomicValues()` |
| `Enumeration<T>` | Type-safe enum with display name and `IComparable` |
| `Period` | Immutable time range; uses `TimeProvider` for testability |
| `EntityDto` | ORM mapping base; holds `IList<IDomainEvent>` (not persisted) |

### Contracts (what consumers implement)

- **`IDomainEvent`** — marker with `Name` (string) and `Data` (dynamic)
- **`IDispatchEvents`** — `Task Dispatch(IDomainEvent)`
- **`IHandleDomainEvents`** — `Task Handle(IDomainEvent, CancellationToken)`
- **`IStoreEvents`** — `void Add(IDomainEvent)` + `Task Publish()`
- **`IUnitOfWork`** — `Task<int> SaveChangesAsync(CancellationToken)` + `void CancelSaving()`
- **`IPaginateRequest`** — `PageNumber`, `PageSize`, `SortBy`, `DescendingSort`
- **`IAuditUser`** — `string Report()` + `string[] Details()`

## Code Style (enforced by `.editorconfig` + analyzers)

- Accessibility modifiers always required (error)
- `var` preferred; expression-bodied members encouraged
- Null-coalescing (`??`) and null-propagation (`?.`) mandatory where applicable
- File-scoped namespaces
- Private fields: `_camelCase`; interfaces: `IPrefix`
- Max line length: 160 characters
- CRLF line endings, UTF-8 with BOM
- `TreatWarningsAsErrors: true` — a warning breaks the build

## Project-wide Settings (`Directory.Build.props`)

- `Nullable: enable`
- `ImplicitUsings: enable`
- `LangVersion: latest`
- `RestorePackagesWithLockFile: true` (lock file must be committed on dependency changes)
- Central package management via `Directory.Packages.props` (currently empty — zero deps)
