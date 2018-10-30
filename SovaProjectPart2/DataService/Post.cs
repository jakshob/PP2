using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public abstract class Post
    {
        public int Id { get; set; }
        public int Posttype { get; set; }
        public string Body { get; set; }
        public int Score { get; set; }

	}
}
