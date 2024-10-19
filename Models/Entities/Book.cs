namespace LibraNet.Models.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subject { get; set; }
    public List<Edition> Editions { get; set; }
}
