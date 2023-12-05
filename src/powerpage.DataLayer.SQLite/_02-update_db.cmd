dotnet tool update --global dotnet-ef
dotnet tool restore
dotnet build
dotnet ef --startup-project ../powerpage/ database update --context SQLiteDbContext
pause