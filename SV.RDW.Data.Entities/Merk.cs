namespace SV.RDW.Data.Entities;

[Table("Merken")]
public class Merk
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(50)]
    public string Naam { get; set; } = null!;

    public virtual HashSet<Voertuig> Voertuigen { get; set; } = new HashSet<Voertuig>();
    public virtual HashSet<Handelsbenaming> Handelsbenamingen { get; set; } = new HashSet<Handelsbenaming>();

}
