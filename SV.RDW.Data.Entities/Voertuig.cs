namespace SV.RDW.Entities;

[Table("Voertuigen")]
public class Voertuig
{

    public int Id { get; set; }

    [MaxLength(10)]
    public string Kenteken { get; set; } = null!;

    public int VoertuigSoortId { get; set; }
    public VoertuigSoort VoertuigSoort { get; set; } = null!;

    public int MerkId { get; set; }
    public Merk Merk { get; set; } = null!;

    public int HandelsbenamingId { get; set; }
    public Handelsbenaming Handelsbenaming { get; set; } = null!;

    public DateTime? VervalDatumAPK { get; set; }

    public DateTime Tenaamstelling { get; set; }

    public DateTime EersteToelating { get; set; }

    [MaxLength(50)]
    public string? Inrichting { get; set; }

    [MaxLength(20)]
    public string? Kleur { get; set; }

    public decimal? MassaLedig { get; set; }

    public int ImportId { get; set; }
    public Import Import { get; set; } = null!;

}
