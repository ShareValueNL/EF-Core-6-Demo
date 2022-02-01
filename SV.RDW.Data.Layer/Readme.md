# EF Core Migration

Migration naar twee databases: MySQL en PostgreSQL

Commando voor maken van een migratie:

Voor MySQL:
```
add-migration [migratienaam] -a SV.RDW.Data.Layer -s SV.RDW.DbUp.MySQL -p SV.RDW.Migrations.MySQL
dotnet ef migrations add [migratienaam] --startup-project SV.RDW.DbUp.MySQL --project SV.RDW.Migrations.MySQL
```

Voor PostgreSQL:
```
add-migration [migratienaam] -startup-project SV.RDW.DbUp.PostgreSQL -project SV.RDW.Migrations.PostgreSQL
dotnet ef migrations add [migratienaam] --startup-project SV.RDW.DbUp.PostgreSQL --project SV.RDW.Migrations.PostgreSQL
```

# DbUp

DbUp maakt sql bestand

Voor MySQL:
```
script-migration [vanaf] [totenmet] -s SV.RDW.DbUp.MySQL -p SV.RDW.Migrations.MySQL
dotnet ef migrations script [vanaf] [totenmet] -s SV.RDW.DbUp.MySQL -p SV.RDW.Migrations.MySQL
```

Voor PostgreSQL:
```
script-migration [vanaf] [totenmet] -s SV.RDW.DbUp.PostgreSQL -p SV.RDW.Migrations.PostgreSQL
dotnet ef migrations script [vanaf] [totenmet] -s SV.RDW.DbUp.PostgreSQL -p SV.RDW.Migrations.PostgreSQL
```