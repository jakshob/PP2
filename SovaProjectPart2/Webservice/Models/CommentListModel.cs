using System;
namespace Webservice.Models
{
    public class CommentListModel
    {
        
        public string qa_userid { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
    }
}
