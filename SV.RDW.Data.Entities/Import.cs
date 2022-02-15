namespace SV.RDW.Entities;

[Table("Imports")]
public class Import
{

    public int Id { get; set; }

    public DateTime EersteToelatingDatum { get; set; }

    public int TotaalImport { get; set; }

    public decimal ImportSeconden { get; set; }

    public virtual HashSet<Voertuig> Voertuigen { get; set; } = new HashSet<Voertuig>();

}
