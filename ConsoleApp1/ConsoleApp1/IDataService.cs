using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    interface IDataService
    {
        List<Comment> GetComments(int page, int pageSize);

    }
}
