# R.C. Martin - Clean Architecture - Demo
## Anu Kuusmaa and Andres Käver

Project written for demonstrating Clean Architecture principles. Based on ASP.NET Core 2.2.

### Scaffold controllers with views
~~~
dotnet aspnet-codegenerator controller -name PersonsController -actions -m Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m Contact -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m ContactType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~