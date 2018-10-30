using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class Question
    {
        public int Id { get; set; }
		public string Name { get; set; }
		public int QA_UserId { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        //public Nullable<DateTime> CloseDate { get; set; }
        //public List<int> LinkPostId { get; set; }
        //public List<string> Tags { get; set; }

		//public Answer Answer { get; set; }
	}
}
