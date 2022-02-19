namespace SV.RDW.Data.Layer.Configurations;

internal class HandelsbenamingConfig : IEntityTypeConfiguration<Handelsbenaming>
{
	public void Configure(EntityTypeBuilder<Handelsbenaming> builder)
	{
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
	}
}
