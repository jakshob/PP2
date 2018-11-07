using System;
namespace Webservice.Models
{
    public class QuestionListModel
    {
        public string Url { get; set; }
        public string Qa_UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        
    }
}
