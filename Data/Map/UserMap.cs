using LibraNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraNet.Data.Map;

public class UserMap : BaseMap<User>
{
    public UserMap() : base("tb_users")
    {
    }

    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").IsRequired();
        builder.Property(x => x.Password).HasColumnName("password").IsRequired();
        builder.Property(x => x.Salt).HasColumnName("salt").IsRequired();
        builder.Property(x => x.BirthDate).HasColumnName("birth_date").IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("is_active");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()");
    }
}
