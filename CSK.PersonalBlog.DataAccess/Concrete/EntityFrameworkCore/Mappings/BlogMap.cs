using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class BlogMap : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();

            builder.Property(i => i.Title).HasMaxLength(50).IsRequired();
            builder.Property(i => i.ShortDescription).HasMaxLength(250).IsRequired();
            builder.Property(i => i.Description).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(i => i.ImagePath).HasMaxLength(300).IsRequired();
            builder.Property(i => i.PostedTime).IsRequired();

            builder.HasMany(i => i.CategoryBlogs).WithOne(i => i.Blog).HasForeignKey(i => i.BlogId);
            builder.HasMany(i => i.Comments).WithOne(i => i.Blog).HasForeignKey(i => i.BlogId);
        }
    }
}
