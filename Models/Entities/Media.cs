namespace LibraNet.Models.Entities;

public class Media
{
    public int Id { get; set; }
    public string Description { get; set; }
    public List<Edition> Editions { get; set; }
}
