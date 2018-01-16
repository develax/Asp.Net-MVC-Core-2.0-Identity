-= Sample project using Identity in ASP.NET Core 2.0 =-

* Seeding admin user at the first run.
* Creating and editing users and roles.
* Using "Authorize" attributes with controllers & actions.

1. Create a database and change "ConnectionString" parameter in configuration "appsettings.json" file  accordingly.
2. In Visual Studio go to "Tools" -> "NuGet Package Manager" -> "Package Manager Console" and execute "Update-Database" command in order to create tables for Identity classes (if you create a new project execute "Add-Migration Initial" command before "Update-Database"). 
3. Compile and run the project.
