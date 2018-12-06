using System;
namespace Webservice.Models
{
    public class CommentListModel
    {
        public string Qa_UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
    }
}
