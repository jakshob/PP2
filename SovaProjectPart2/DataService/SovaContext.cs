using Microsoft.EntityFrameworkCore;


namespace WebServer
{
    public class SovaContext : DbContext
    {
       
        public DbSet<Post> Posts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("host=localhost;db=stackoverflow;uid=postgres;pwd=olivia");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Post
            modelBuilder.Entity<Post>().ToTable("post");
            modelBuilder.Entity<Post>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Post>().Property(x => x.Name).HasColumnName("title");
            modelBuilder.Entity<Post>().Property(x => x.Posttype).HasColumnName("posttype");
            //modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Post>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Post>().Property(x => x.ParentId).HasColumnName("parentid");

        }
    }
}
