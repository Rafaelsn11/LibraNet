using LibraNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraNet.Data.Map;

public class EditionMap : BaseMap<Edition>
{
    public EditionMap() : base("tb_editions")
    {
    }

    public override void Configure(EntityTypeBuilder<Edition> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(x => x.Year).HasColumnName("year").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();
        builder.Property(x => x.LastLoanDate).HasColumnName("last_loan_date");

        builder.HasOne(x => x.Book)
            .WithMany(b => b.Editions)
            .HasForeignKey(x => x.BookId);
        builder.Property(x => x.BookId).HasColumnName("book_id");

        builder.HasOne(x => x.Media)
            .WithMany(m => m.Editions)
            .HasForeignKey(x => x.MediaId);
        builder.Property(x => x.MediaId).HasColumnName("media_id");

        builder.HasOne(x => x.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired(false);

    }
}
