namespace LibraNet.Models.Entities;

public class Edition : EntityBase
{
    public int Year { get; set; }
    public char Status { get; set; }
    public DateTime? LastLoanDate { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int MediaId { get; set; }
    public Media Media { get; set; }
    public int? UserId { get; set; }
    public User User { get; set; }
}
