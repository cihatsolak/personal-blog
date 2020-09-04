using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class CategoryBlogMap : IEntityTypeConfiguration<CategoryBlog>
    {
        public void Configure(EntityTypeBuilder<CategoryBlog> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();

            /*
             * Id'si 1 olan Blog ile id'si 1 olan Category eşleştirildi ise,
             * Tekrar bu işlemin gerçekleşmemesi için (yani idsi 1 olan blog ile idsi 1 olan category'nin eşleşmemesi)
             * her iki tarafıda uniqe olarak belirtmeliyiz. Böylece mükerrer kayıt olmayacak.
             */
            builder.HasIndex(i => new { i.BlogId, i.CategoryId }).IsUnique();
        }
    }
}
