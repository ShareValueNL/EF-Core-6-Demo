namespace SV.RDW.Data.Layer.Configurations;

internal class VoertuigConfig : IEntityTypeConfiguration<Voertuig>
{
	public void Configure(EntityTypeBuilder<Voertuig> builder)
	{
		builder.Property(p => p.Id).ValueGeneratedOnAdd();

		builder.Property(e => e.Kleur)
			.HasMaxLength(50)
			.IsUnicode(false)
			.HasComment("Hier staat de kleur.");

		// builder.Property(e => e.EersteToelating).HasColumnType("datetime");

		//builder.HasOne(d => d.ModelPage)
		//	.WithMany(p => p.Comments)
		//	.HasPrincipalKey(p => new { p.ModelId, p.PageId })
		//	.HasForeignKey(d => new { d.ModelId, d.PageId })
		//	.OnDelete(DeleteBehavior.Restrict);

	}
}
