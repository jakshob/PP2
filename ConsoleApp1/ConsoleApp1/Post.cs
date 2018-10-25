using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    class Post
    {
        public int Id { get; set; }
        public int QA_UserId { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public int PostTypeId { get; set; }
        public int ParrentId { get; set; }
        public DateTime CloseDate { get; set; }
        public string Title { get; set; }
        public int LinkPostId { get; set; }
    }
}
