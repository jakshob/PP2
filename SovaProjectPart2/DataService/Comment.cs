using System;

namespace DomainModel
{
    public class Comment
    {
        public int Id { get; set; }
        public int QA_UserId { get; set; }
        public int PostId { get; set; }
	    public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
	}
}
