using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();

            builder.Property(i => i.UserName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Password).HasMaxLength(50).IsRequired();
            builder.Property(i => i.Email).HasMaxLength(100).IsRequired();
            builder.Property(i => i.Name).HasMaxLength(50).IsRequired();
            builder.Property(i => i.SurName).HasMaxLength(50);

            builder.HasMany(i => i.Blogs).WithOne(i => i.AppUser).HasForeignKey(i => i.AppUserId);
        }
    }
}
