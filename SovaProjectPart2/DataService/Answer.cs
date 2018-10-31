using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Answer : Post
    {
        public int ParentId { get; set; }

		//public Question Question { get; set; }
    }
}
