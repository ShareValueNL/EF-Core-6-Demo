namespace SV.RDW.Data.Entities;

[Table("VoertuigSoorten")]
public class VoertuigSoort
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Naam { get; set; } = null!;

    public virtual HashSet<Voertuig> Voertuigen { get; set; } = new HashSet<Voertuig>();

}
