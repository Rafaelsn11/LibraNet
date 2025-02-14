namespace LibraNet.Models.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; }
    public List<Edition> Loans { get; set; }
}
