namespace LibraNet.Models.Entities;

public class Media : EntityBase
{
    public string Description { get; set; } = string.Empty;
    public List<Edition> Editions { get; set; } = [];
}
