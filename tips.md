For someone not familiar the v8, here is some notes and tips when I coded it
https://learn.microsoft.com/zh-cn/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code

dotnet run --launch-profile https

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet tool uninstall -g dotnet-aspnet-codegenerator
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool update -g dotnet-aspnet-codegenerator

dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers

dotnet aspnet-codegenerator controller -name UserController -async -api -m User -dc UserContext -outDir Controllers

dotnet aspnet-codegenerator controller -name EventController -async -api -m Event -dc EventContext -outDir Controllers

dotnet aspnet-codegenerator controller -name ClientController -async -api -m Client -dc ClientContext -outDir Controllers

dotnet aspnet-codegenerator controller -name OrgansationController -async -api -m Organsation -dc OrgansationContext -outDir Controllers

dotnet aspnet-codegenerator controller -name MedicalRecordController -async -api -m MedicalRecord -dc MedicalRecordContext -outDir Controllers


https://www.c-sharpcorner.com/article/asp-net-core-web-api-for-crud-operations-with-mysql/
https://stackoverflow.com/questions/36187540/asp-net-web-api-with-mysql-database
***https://stackoverflow.com/questions/40059929/cannot-find-the-usemysql-method-on-dbcontextoptions
https://mysqlconnector.net/tutorials/connect-to-mysql/
https://stackoverflow.com/questions/12545565/find-a-record-in-dbset-using-find-without-a-primary-key
**https://learn.microsoft.com/en-us/ef/ef6/querying/
https://stackoverflow.com/questions/70273434/unable-to-resolve-service-for-type-%C2%A8microsoft-entityframeworkcore-dbcontextopti
https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many


//dotnet different route for method
**https://stackoverflow.com/questions/34709085/creating-a-different-route-to-a-specific-action



dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package MySqlConnector.DependencyInjection

https://stackoverflow.com/questions/18215376/have-model-contain-field-without-adding-it-to-the-database