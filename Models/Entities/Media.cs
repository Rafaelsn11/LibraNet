namespace LibraNet.Models.Entities;

public class Media : EntityBase
{
    public string Description { get; set; }
    public List<Edition> Editions { get; set; }
}
