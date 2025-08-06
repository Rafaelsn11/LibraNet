namespace LibraNet.Models.Entities;

public class Role : EntityBase
{
    public Guid RoleIdentifier { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public List<UserRole> UserRoles { get; set; } = [];
}
