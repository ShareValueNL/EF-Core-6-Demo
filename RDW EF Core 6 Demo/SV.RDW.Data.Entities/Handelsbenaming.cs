namespace SV.RDW.Entities;

[Table("Handelsbenamingen")]
public class Handelsbenaming
{

    public int Id { get; set; }

    [MaxLength(50)]
    public string Naam { get; set; } = null!;

    public int MerkId { get; set; }

    public Merk Merk { get; set; } = null!;

    public virtual HashSet<Voertuig> Voertuigen { get; set; } = new HashSet<Voertuig>();

}
