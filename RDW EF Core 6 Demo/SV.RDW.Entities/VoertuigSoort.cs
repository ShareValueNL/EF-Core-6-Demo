namespace SV.RDW.Entities;

[Table("VoertuigSoorten")]
public class VoertuigSoort
{

    public int Id { get; set; }

    public string Naam { get; set; } = null!;

    public virtual HashSet<Voertuig> Voertuigen { get; set; } = new HashSet<Voertuig>();

}
