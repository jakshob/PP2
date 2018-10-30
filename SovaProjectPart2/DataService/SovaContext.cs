using Microsoft.EntityFrameworkCore;


namespace DomainModel
{
    public class SovaContext : DbContext
    {
       
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("host=localhost;db=stackoverflow;uid=postgres;pwd=Pluto25");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			//Comment
			modelBuilder.Entity<Comment>().ToTable("comment");
			modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("id");
			modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("postid");
			modelBuilder.Entity<Comment>().Property(x => x.Score).HasColumnName("score");
			modelBuilder.Entity<Comment>().Property(x => x.QA_UserId).HasColumnName("authorid");
			modelBuilder.Entity<Comment>().Property(x => x.CreationDate).HasColumnName("creationdate");
			modelBuilder.Entity<Comment>().Property(x => x.Text).HasColumnName("body");

			//Answer
			modelBuilder.Entity<Answer>().ToTable("answers");
			modelBuilder.Entity<Answer>().Property(x => x.Id).HasColumnName("id");
			//modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("body");
			modelBuilder.Entity<Answer>().Property(x => x.Score).HasColumnName("score");
			modelBuilder.Entity<Answer>().Property(x => x.ParentId).HasColumnName("parentid");
			modelBuilder.Entity<Answer>().Property(x => x.QA_UserId).HasColumnName("ownerid");
			modelBuilder.Entity<Answer>().Property(x => x.CreationDate).HasColumnName("creationdate");

			//Question
			modelBuilder.Entity<Question>().ToTable("questions");
			modelBuilder.Entity<Question>().Property(x => x.Id).HasColumnName("id");
			modelBuilder.Entity<Question>().Property(x => x.Name).HasColumnName("title");
			//modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("body");
			modelBuilder.Entity<Question>().Property(x => x.Score).HasColumnName("score");
			modelBuilder.Entity<Question>().Property(x => x.QA_UserId).HasColumnName("ownerid");
			modelBuilder.Entity<Question>().Property(x => x.CreationDate).HasColumnName("creationdate");
			modelBuilder.Entity<Question>().Property(x => x.CloseDate).HasColumnName("closeddate");

			modelBuilder.Entity<Question>().HasOne(p => p.Answer)
				.WithOne(p => p.Question).HasForeignKey<Answer>(p => p.Id);
			modelBuilder.Entity<Answer>().HasOne(p => p.Question)
				.WithOne(p => p.Answer).HasForeignKey<Answer>(p => p.Id);
		}
    }
}
