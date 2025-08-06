using LibraNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraNet.Data.Map;

public class BookMap : BaseMap<Book>
{
    public BookMap() : base("tb_books")
    {
    }

    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title).HasColumnName("title").IsRequired();
        builder.Property(x => x.Subject).HasColumnName("subject").IsRequired();

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
    }
}
