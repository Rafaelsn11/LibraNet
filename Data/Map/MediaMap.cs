using LibraNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraNet.Data.Map;

public class MediaMap : BaseMap<Media>
{
    public MediaMap() : base("tb_media_formats")
    {
    }

    public override void Configure(EntityTypeBuilder<Media> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(x => x.Description).HasColumnName("description").IsRequired();
    }
}
