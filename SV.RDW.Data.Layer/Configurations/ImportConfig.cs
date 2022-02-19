namespace SV.RDW.Data.Layer.Configurations;

internal class MerkConfig : IEntityTypeConfiguration<Merk>
{
	public void Configure(EntityTypeBuilder<Merk> builder)
	{
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
	}
}
