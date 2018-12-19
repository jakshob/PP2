
using Microsoft.EntityFrameworkCore;

namespace DomainModel
{
    public class SovaContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<QA_User> Qa_Users { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SOVA_User> SOVA_Users { get; set; }

        public DbQuery<SearchResult> SearchResults { get; set; }
        public DbQuery<RelevantWord> RelevantWords { get; set; }
        public DbQuery<RelevantWord2> RelevantWords2 { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("host=rawdata.ruc.dk;db=raw1;uid=raw1;pwd=Y.aDOhAK");
            //optionsBuilder.UseNpgsql("host = localhost; db = stackoverflow; uid = postgres; pwd = Sko1bog");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            //Comment
            modelBuilder.Entity<Comment>().ToTable("comment");
            modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("postid");
            modelBuilder.Entity<Comment>().Property(x => x.Text).HasColumnName("body");
            modelBuilder.Entity<Comment>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Comment>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<Comment>().Property(x => x.QA_UserId).HasColumnName("authorid");

            //Favorites
            modelBuilder.Entity<Favorite>().ToTable("favorites");
            modelBuilder.Entity<Favorite>().Property(x => x.PostId).HasColumnName("postid");
            modelBuilder.Entity<Favorite>().Property(x => x.SOVA_UserUsername).HasColumnName("username");
            modelBuilder.Entity<Favorite>().Property(x => x.Note).HasColumnName("note");
            modelBuilder.Entity<Favorite>().HasKey(o => new { o.PostId, o.SOVA_UserUsername });

            //History
            modelBuilder.Entity<History>().ToTable("history");
            modelBuilder.Entity<History>().Property(x => x.SOVA_UserUsername).HasColumnName("username");
            modelBuilder.Entity<History>().Property(x => x.CreationDate).HasColumnName("creation_date");
            modelBuilder.Entity<History>().Property(x => x.SearchText).HasColumnName("search_text");
            modelBuilder.Entity<History>().HasKey(o => new { o.SOVA_UserUsername, o.CreationDate });

            //QA-user
            modelBuilder.Entity<QA_User>().ToTable("qauser");
            modelBuilder.Entity<QA_User>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<QA_User>().Property(x => x.DisplayName).HasColumnName("displayname");
            modelBuilder.Entity<QA_User>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<QA_User>().Property(x => x.Location).HasColumnName("location");
            modelBuilder.Entity<QA_User>().Property(x => x.Age).HasColumnName("age");

            
            //Sova_User
            modelBuilder.Entity<SOVA_User>().ToTable("users");
            modelBuilder.Entity<SOVA_User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<SOVA_User>().Property(x => x.Password).HasColumnName("password");
            //modelBuilder.Entity<SOVA_User>().Property(x => x.Salt).HasColumnName("salt");
            modelBuilder.Entity<SOVA_User>().HasKey(x => x.Username);

            //Posts
            modelBuilder.Entity<Post>().ToTable("post");
            modelBuilder.Entity<Post>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Post>().Property(x => x.Posttype).HasColumnName("posttype");
            modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Post>().Property(x => x.Score).HasColumnName("score");
			modelBuilder.Entity<Post>().Property(x => x.CreationDate).HasColumnName("creationdate");
			modelBuilder.Entity<Post>().Property(x => x.QA_UserId).HasColumnName("ownerid");
			modelBuilder.Entity<Post>().HasDiscriminator(x => x.Posttype)
                .HasValue<Question>(1)
                .HasValue<Answer>(2);

            modelBuilder.Entity<Question>().Property(x => x.Title).HasColumnName("title");
			
            modelBuilder.Entity<Answer>().Property(x => x.ParentId).HasColumnName("parentid");

            
			
        }
	}
}
