using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SV.RDW.Data.Layer;

namespace SV.RDW.Migrations.PostgreSQL;
public class PostgreSQLContext : BaseContext
{
	public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		//var voertuigBuilder = modelBuilder.Entity<Voertuig>();
		//voertuigBuilder.Property(e => e.Tenaamstelling).HasColumnType("DATE");
		//voertuigBuilder.Property(e => e.VervalDatumAPK).HasColumnType("DATE");
		//voertuigBuilder.Property(e => e.EersteToelating).HasColumnType("DATE");

		//var importBuilder = modelBuilder.Entity<Import>();
		//importBuilder.Property(e => e.EersteToelatingDatum).HasColumnType("DATE");
	}
}
