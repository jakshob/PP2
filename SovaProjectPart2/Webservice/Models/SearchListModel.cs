﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Webservice.Models
{
    public class SearchListModel
    {
        public string title { get; set; }
        public int postId { get; set; }
        public int rank { get; set; }
        public string body { get; set; }
        public int posttype { get; set; }
    }
}
