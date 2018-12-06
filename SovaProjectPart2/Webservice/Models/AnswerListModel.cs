using System;
namespace Webservice.Models
{
    public class AnswerListModel
    {
        public int Qa_UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
    }
}
