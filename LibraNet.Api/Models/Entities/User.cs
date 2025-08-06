namespace LibraNet.Models.Entities;

public class User : EntityBase
{
    public Guid UserIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public bool IsActive { get; set; }
    public List<Edition> Loans { get; set; } = [];
    public List<UserRole> UserRoles { get; set; } = [];
}
