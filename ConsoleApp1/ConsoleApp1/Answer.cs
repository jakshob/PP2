using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    class Answer
    {
        public int Id { get; set; }
        public int QA_UserId { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public int ParrentId { get; set; }
    }
}
