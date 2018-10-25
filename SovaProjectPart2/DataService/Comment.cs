using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer
{
    class Comment
    {
        public int Id { get; set; }
        public int QA_UserId { get; set; }
        public int PostId { get; set; }
        public Answer Answer { get; set; }
        public Question Question { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
    }
}
