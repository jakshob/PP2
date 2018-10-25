using Microsoft.EntityFrameworkCore;


namespace DomainModel
{
    public class SovaContext : DbContext
    {
       
        public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }


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

			//Comment
			modelBuilder.Entity<Comment>().ToTable("comment");
			modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("id");
			modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("postid");
			modelBuilder.Entity<Comment>().Property(x => x.Score).HasColumnName("score");

		}
    }
}
