using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();

            builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
            builder.HasMany(i => i.CategoryBlogs).WithOne(i => i.Category).HasForeignKey(i => i.CategoryId);
        }
    }
}
