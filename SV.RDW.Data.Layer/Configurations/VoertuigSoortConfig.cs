namespace SV.RDW.Data.Layer.Configurations;

internal class VoertuigSoortConfig : IEntityTypeConfiguration<VoertuigSoort>
{
	public void Configure(EntityTypeBuilder<VoertuigSoort> builder)
	{
		builder.Property(p => p.Id).ValueGeneratedOnAdd();
	}
}
