# ASP.NET Core Scheduler for CRUD Actions with Editor Template

## Overview

This sample demonstrates how to perform CRUD actions in the Syncfusion [ASP.NET Core Scheduler](https://www.syncfusion.com/aspnet-core-ui-controls/scheduler) using a custom editor template. The application binds the Scheduler to a SQL Server LocalDB database through Entity Framework Core, and grouped resources (`Projects` and `Categories`) drive the timeline and agenda views. Appointments are loaded and persisted through Syncfusion `DataManager` URL adaptor endpoints, so create, edit, and delete operations round-trip from the custom editor window back to the database without a full page reload.

## Key Features

- Uses the Syncfusion EJ2 ASP.NET Core `Scheduler` to render appointments with timeline and agenda views.
- Replaces the default editor window with a custom editor template that exposes Summary, Status, Category, and From/To inputs.
- Uses Syncfusion `DropDownList`, `MultiSelect`, and `DateTimePicker` widgets inside the editor for picking project, category, and time fields.
- Groups appointments across two resource columns, `Projects` and `Categories`, with `Categories` filtering against the selected `ProjectId` using an `ej.data.Query` predicate on the `groupId` field.
- Performs CRUD through `DataManager` URL adaptor endpoints that load and update appointments in the database.
- Persists appointments to SQL Server LocalDB through Entity Framework Core.
- Seeds the database with two sample appointments (including a recurring one defined by the recurrence rule `FREQ=DAILY;INTERVAL=1;COUNT=5`) at startup so the Scheduler always has data on first run.

## Prerequisites

- Visual Studio 2022 or Visual Studio Code
- .NET SDK compatible with the project's target framework
- SQL Server Express LocalDB instance

## How to Run the Project

### Visual Studio 2022

1. Clone or download this repository.
2. Open the project file:

   `EJ2ScheduleSample.csproj`

3. Ensure that the SQL Server Express LocalDB instance is available so that the database file at `Data/ScheduleEventData.mdf` can be attached through the connection string built in `Startup.cs`.
4. Restore the NuGet packages by rebuilding the solution.
5. Set the startup project to:

   `EJ2ScheduleSample`

6. Build the solution.
7. Run the application using `Ctrl+F5`.
8. Open the application URL displayed by Visual Studio after launch.
9. Verify that the Scheduler renders with the TimelineMonth view and the grouped `Projects` and `Categories` resources, and that double-clicking a cell or appointment opens the custom editor window.

### Visual Studio Code

1. Clone or download this repository.
2. Open the repository folder in Visual Studio Code.
3. Open the integrated terminal.
4. Navigate to the project directory:

```bash
cd aspnetcore-scheduler-crud-actions-with-editor-template
```

5. Ensure that the SQL Server Express LocalDB instance is available so that the database file at `Data/ScheduleEventData.mdf` can be attached through the connection string built in `Startup.cs`.
6. Restore the NuGet packages:

```bash
dotnet restore
```

7. Build the project:

```bash
dotnet build
```

8. Run the project:

```bash
dotnet run
```

9. Open the local URL displayed in the terminal after the application starts.
10. Verify that the Scheduler renders with the TimelineMonth view and the grouped `Projects` and `Categories` resources, and that double-clicking a cell or appointment opens the custom editor window.

## Project Structure

- `Controllers/HomeController.cs` — MVC controller that returns the Home view and exposes the load and update endpoints that drive the Scheduler's CRUD operations.
- `Models/ScheduleDataContext.cs` — Entity Framework Core `DbContext` that exposes the appointments set mapped to the database table.
- `Data/DbInitializer.cs` — Seeds the appointments table with sample appointments when the database is empty.
- `Views/Home/Index.cshtml` — Razor view that renders the Scheduler, declares the custom editor template script block, and defines the client event handler that creates the editor widgets.

## Support and Feedback

- For general product questions, visit the [Syncfusion Community Forum](https://www.syncfusion.com/forums) or [Syncfusion Support](https://www.syncfusion.com/support).
- To report an issue specific to this sample, open a GitHub issue in this repository.
- Official documentation: [Getting Started with ASP.NET Core Scheduler](https://help.syncfusion.com/scheduler-sdk/asp-net-core/schedule/getting-started)

## License

This is a Syncfusion sample project provided to demonstrate product usage. Review the [Syncfusion license terms](https://www.syncfusion.com/sales/pricing?category=ui-components) before using Syncfusion components in your own applications.
