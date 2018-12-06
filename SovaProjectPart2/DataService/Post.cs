using System;

namespace DomainModel
{
	public abstract class Post
	{
		public int Id { get; set; }
		public int Posttype { get; set; }
		public string Body { get; set; }
		public int Score { get; set; }
		public int QA_UserId { get; set; }
		public DateTime CreationDate { get; set; }
    }
}
