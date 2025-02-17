namespace LibraNet.Models.Entities;

public class Book : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public List<Edition> Editions { get; set; } = [];
}
