using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SV.RDW.Data.Layer;

namespace SV.RDW.Migrations.PostgreSQL;
public class PostgreSQLContext : BaseContext
{
	public PostgreSQLContext(DbContextOptions<BaseContext> options) : base(options)
	{
	}
}
