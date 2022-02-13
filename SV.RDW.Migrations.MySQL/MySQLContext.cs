using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SV.RDW.Data.Layer;

namespace SV.RDW.Migrations.MySQL;

public class MySQLContext : BaseContext
{
	public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
	{
	}


}
// add-migration Initieel -a SV.RDW.Data.Layer -s SV.RDW.Apps.MigrationHandler -p SV.RDW.Migrations.MySQL -c SV.RDW.Migrations.MySQL.MySQLContext