using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Comment
    {
        public int Id { get; set; }
        public int QA_UserId { get; set; }
        public int PostId { get; set; }
		//public Answer Answer { get; set; }
        //public Question Question { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
		/*
		 Forklaring: Answer tager sin Id fra tabellen Post i databasen, men man kan ikke bruge modelBuilder til
		 at koble flere klasser til samme tabel. Ihvertfald ikke uden at få en fejl om at deres Id attribut også 
		 skal kobles sammen. Så cowboy-koden med at Answer og Question eksisterer adskilt fra Post klassen skal
		 redigeres.
		 */
	}
}
