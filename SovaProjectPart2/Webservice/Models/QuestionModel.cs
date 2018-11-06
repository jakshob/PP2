using System;
using System.Collections.Generic;

namespace Webservice.Models
{
    public class QuestionModel
    {

        public string Url { get; set; }
        public string Qa_UserId { get; set; }
        public string Body { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public int PostTypeId { get; set; }
        public int LinkPostId { get; set; }
        public List<string> Tags { get; set; }

    }
}
