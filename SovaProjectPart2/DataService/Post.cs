using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public int Posttype { get; set; }
        public string Body { get; set; }
        public int Score { get; set; }

		//public Answer Answer { get; set; }
		//public Question Question { get; set; }
	}
}
