﻿using System;
using System.Collections.Generic;

namespace Webservice.Models
{
    public class QuestionModel
    {

        public string Url { get; set; }
        public string Qa_UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
    
    }
}
