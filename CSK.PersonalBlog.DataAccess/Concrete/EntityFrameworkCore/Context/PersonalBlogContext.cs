using CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Mappings;
using CSK.PersonalBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CSK.PersonalBlog.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class PersonalBlogContext : DbContext
    {
        public PersonalBlogContext(DbContextOptions<PersonalBlogContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new BlogMap());
            modelBuilder.ApplyConfiguration(new CategoryBlogMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new LogMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryBlog> CategoryBlogs { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
