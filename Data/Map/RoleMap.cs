using LibraNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraNet.Data.Map;

public class RoleMap : BaseMap<Role>
{
    public RoleMap() : base("tb_roles")
    {
    }

    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(r => r.RoleName).HasColumnName("role_name").IsRequired().HasMaxLength(50);

        builder.Property(x => x.RoleIdentifier)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("role_identifier").IsRequired();
    }
}
