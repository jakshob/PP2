﻿using System;
namespace Webservice.Models
{
    public class AnswerListModel
    {
        //public string Url { get; set; }
        public int QA_UserId { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }        
    }
}
