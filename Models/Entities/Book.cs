namespace LibraNet.Models.Entities;

public class Book : EntityBase
{
    public string Title { get; set; }
    public string Subject { get; set; }
    public List<Edition> Editions { get; set; }
}
