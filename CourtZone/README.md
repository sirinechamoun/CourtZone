# CourtZone - ASP.NET Core MVC + SQLite

CourtZone is a simple football and basketball court reservation website for a university ASP.NET project.

## Features

- View football and basketball courts
- Add real stadium names and image URLs later
- Reserve courts by date and time
- Prevent double booking
- Admin dashboard
- Add/edit courts
- Happy hour discounts for dead hours
- Reservation approval/rejection
- SQLite database generated automatically

## Requirements

- .NET 8 SDK
- VS Code
- C# extension or C# Dev Kit extension

No Oracle or SQL Server installation is required.

## How to open in VS Code

1. Extract the ZIP file.
2. Open VS Code.
3. Click File > Open Folder.
4. Select the `CourtZone` folder.
5. Open Terminal > New Terminal.

## How to run

```bash
dotnet restore
dotnet run
```

Then open the localhost link shown in the terminal.

## Database

The app uses SQLite. The database file is created automatically as:

```text
courtzone.db
```

You do not need to manually create tables. The project uses `EnsureCreated()` on first run and seeds sample courts and discounts.

## Where to add real stadium pictures

Go to:

```text
/Admin/Courts
```

Then edit or add a court and put either:

- an online image URL, or
- a local image path like `/images/my-stadium.jpg`

If using a local image, place it inside:

```text
wwwroot/images
```

## Suggested project title

CourtZone: Football & Basketball Court Reservation System with Happy Hour Discounts
