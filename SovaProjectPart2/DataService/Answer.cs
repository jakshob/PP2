using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Answer : Post
    {
        public int QA_UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public int ParentId { get; set; }

		//public Question Question { get; set; }
    }
}
