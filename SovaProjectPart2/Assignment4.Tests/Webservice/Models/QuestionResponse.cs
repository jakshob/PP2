using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webservice.Models {
	public class QuestionResponse {
		int Page;
        string First;
        string Next;
        string Prev;
        public List<QuestionListModel> Items;
	}
}
