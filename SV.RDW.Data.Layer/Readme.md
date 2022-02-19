# EF Core Migration

Migration naar twee databases: MySQL en PostgreSQL

Commando voor maken van een migratie:

Voor MySQL:
```
add-migration [migratienaam] -a SV.RDW.Data.Layer -s SV.RDW.Apps.MigrationHandler -p SV.RDW.Migrations.MySQL -Context MySQLContext
// dotnet ef migrations add [migratienaam] --startup-project SV.RDW.DbUp.MySQL --project SV.RDW.Migrations.MySQL
```

Voor PostgreSQL:
```
add-migration [migratienaam] -a SV.RDW.Data.Layer -s SV.RDW.Apps.MigrationHandler -p SV.RDW.Migrations.PostgreSQL -Context PostgreSQLContext
// dotnet ef migrations add ....
```

# DbUp

DbUp maakt sql bestand

De ```[vanaf]``` en ```[totenmet]``` zijn de hele Migration naam, inclusief datum en tijd (zie Migrations map).

Voor MySQL:
```
script-migration [vanaf] [totenmet] -a SV.RDW.Data.Layer -s SV.RDW.Apps.MigrationHandler -p SV.RDW.Migrations.MySQL -Context MySQLContext -o SV.RDW.DbUp.MySQL\Scripts\[totenmet].sql
// dotnet ef migrations script ...
```

Voor PostgreSQL:
```
script-migration [vanaf] [totenmet] -a SV.RDW.Data.Layer -s SV.RDW.Apps.MigrationHandler -p SV.RDW.Migrations.PostgreSQL -Context PostgreSQLContext -o SV.RDW.DbUp.PostgreSQL\Scripts\[tptenmet].sql
//dotnet ef migrations script ...
```

Voorbeelden
```
script-migration 20220219112632_ImportVoertuigenFK 20220219125331_MeerNullFKs -a SV.RDW.Data.Layer -s SV.RDW.Apps.MigrationHandler -p SV.RDW.Migrations.MySQL -Context MySQLContext -o SV.RDW.DbUp.MySQL\Scripts\20220219125331_MeerNullFKs.sql
```

```
script-migration 20220219120337_ImportVoertuigenFK 20220219125452_MeerNullFKs -a SV.RDW.Data.Layer -s SV.RDW.Apps.MigrationHandler -p SV.RDW.Migrations.PostgreSQL -Context PostgreSQLContext -o SV.RDW.DbUp.PostgreSQL\Scripts\20220219125452_MeerNullFKs.sql
```