using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public class QA_User
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
    }
}