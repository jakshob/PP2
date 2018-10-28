using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DomainModel
{
    class SOVAContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<QA_User> Qa_Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SOVA_User> SOVA_Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("host=localhost;db=stackoverflow;uid=postgres;pwd=olivia");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Answer>().ToTable("answers");
            modelBuilder.Entity<Answer>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Answer>().Property(x => x.QA_UserId).HasColumnName("qa_userid");
            modelBuilder.Entity<Answer>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Answer>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<Answer>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Answer>().Property(x => x.ParentId).HasColumnName("parrentid");

            modelBuilder.Entity<Comment>().ToTable("comment");
            modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("postid");
            modelBuilder.Entity<Comment>().Property(x => x.Text).HasColumnName("body");
            modelBuilder.Entity<Comment>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<Comment>().Property(x => x.CreationDate).HasColumnName("creationdate");            
            modelBuilder.Entity<Comment>().Property(x => x.QA_UserId).HasColumnName("authorid");

            modelBuilder.Entity<Comment>().Property(x => x.Answer).HasColumnName("answer");
            modelBuilder.Entity<Comment>().Property(x => x.Question).HasColumnName("question");

            modelBuilder.Entity<Favorite>().ToTable("favorites");
            modelBuilder.Entity<Favorite>().Property(x => x.PostId).HasColumnName("postid");
            modelBuilder.Entity<Favorite>().Property(x => x.SOVA_UserUsername).HasColumnName("username");
            modelBuilder.Entity<Favorite>().Property(x => x.Note).HasColumnName("note");

            modelBuilder.Entity<History>().ToTable("history");
            modelBuilder.Entity<History>().Property(x => x.SOVA_UserUsername).HasColumnName("username");
            modelBuilder.Entity<History>().Property(x => x.CreationDate).HasColumnName("creation_date");
            modelBuilder.Entity<History>().Property(x => x.SearchText).HasColumnName("search_text");

            modelBuilder.Entity<QA_User>().ToTable("qauser");
            modelBuilder.Entity<QA_User>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<QA_User>().Property(x => x.DisplayName).HasColumnName("displayname");
            modelBuilder.Entity<QA_User>().Property(x => x.CreationDate).HasColumnName("creationdate");
            modelBuilder.Entity<QA_User>().Property(x => x.Location).HasColumnName("location");
            modelBuilder.Entity<QA_User>().Property(x => x.Age).HasColumnName("age");
        }
    }
}
