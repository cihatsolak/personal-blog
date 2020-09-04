using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();

            builder.Property(i => i.AuthorName).HasMaxLength(50).IsRequired();
            builder.Property(i => i.AuthorEmail).HasMaxLength(100).IsRequired();
            builder.Property(i => i.Description).HasColumnType("nvarchar").HasMaxLength(350).IsRequired();
            builder.Property(i => i.PostedTime).IsRequired();

            builder.HasOne(i => i.ParentComment).WithMany(i => i.SubComments).HasForeignKey(i => i.ParentCommentId);
        }
    }
}
