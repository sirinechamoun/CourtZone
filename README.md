CourtZone - ASP.NET Core MVC + SQLite
CourtZone is a simple football and basketball court reservation website for a university ASP.NET project. Development branch used for UI improvements and project documentation...

Features
View football and basketball courts
Add real stadium names and image URLs later
Reserve courts by date and time
Prevent double bookingg
Admin dashboard
Add/edit courts
Happy hour discounts for dead hours
Reservation approval or rejection
SQLite database generated automatically
Requirements
.NET 8 SDK
VS Code
C# extension or C# Dev Kit extension
No Oracle or SQL Server installation is required.

How to open in VS Code
Extract the ZIP file.
Open VS Code.
Click File > Open Folder.
Select the CourtZone folder.
Open Terminal > New Terminal.
How to run
dotnet restore
dotnet run
Then open the localhost link shown in the terminal.

Database
The app uses SQLite. The database file is created automatically as:

courtzone.db
You do not need to manually create tables. The project uses EnsureCreated() on first run and seeds sample courts and discounts.

Where to add real stadium pictures
Go to:

/Admin/Courts
Then edit or add a court and put either:

an online image URL, or
a local image path like /images/my-stadium.jpg
If using a local image, place it pls inside:

wwwroot/images
Suggested project title
CourtZone: Football & Basketball Court Reservation System with Happy Hour Discounts
