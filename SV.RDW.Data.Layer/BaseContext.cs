using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using SV.RDW.Data.Entities;
using System.Diagnostics;

namespace SV.RDW.Data.Layer;

public partial class BaseContext : DbContext
{
	public BaseContext()
	{
	}

	public BaseContext(DbContextOptions<BaseContext> options)
		: base(options)
	{
	}

	protected BaseContext(DbContextOptions options)
	   : base(options)
	{
	}

	public virtual DbSet<Voertuig> Voertuigen { get; set; } = null!;

	public virtual DbSet<VoertuigSoort> VoertuigSoorten { get; set; } = null!;

	public virtual DbSet<Merk> Merken { get; set; } = null!;

	public virtual DbSet<Handelsbenaming> Handelsbenamingen { get; set; } = null!;

	public virtual DbSet<Import> Imports { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.EnableSensitiveDataLogging()
			.EnableDetailedErrors()
			.LogTo(msg => Debug.WriteLine(msg), LogLevel.Information);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// default settings
		SetDefaults(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(assembly: typeof(BaseContext).Assembly);
	}

	protected void SetDefaults(ModelBuilder modelBuilder)
	{
		foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
		{
			string? tableName = entityType.GetTableName();
			if (tableName != null)
			{
				tableName = tableName[..1].ToLower() + tableName[1..];
				entityType.SetTableName(tableName);

				if (tableName.StartsWith("asp_net_"))
				{
					entityType.SetSchema("identity");
				}
			}

			foreach (IMutableProperty property in entityType.GetProperties())
			{
				var propertyName = property.Name;
				propertyName = propertyName.Substring(0, 1).ToLower() + propertyName[1..];
				property.SetColumnName(propertyName);
			}
		}

	}

}
