using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();
            builder.Property(i => i.Path).HasMaxLength(400);

            builder.HasOne(i => i.AppUser).WithMany(i => i.Logs).HasForeignKey(i => i.AppUserId);
        }
    }
}
