using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class SearchResult
    {
        public string title { get; set; }
        public int postId { get; set; }
        public int rank { get; set; }
        public string body { get; set; }
        public int posttype { get; set; }
        public int score { get; set; }
    }
}
