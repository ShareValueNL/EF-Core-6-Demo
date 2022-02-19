namespace SV.RDW.Data.Layer.Configurations;

internal class ImportConfig : IEntityTypeConfiguration<Import>
{
	public void Configure(EntityTypeBuilder<Import> builder)
	{
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
	}
}
