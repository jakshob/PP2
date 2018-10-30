using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Webservice.Controllers
{
    public class ControllerHelper : Controller
    {
        public int ComputeNumberOfPages(int pageSize, int numberOfItems)
        {
            return (int)Math.Ceiling((double)numberOfItems / pageSize);
        }

        public string CreateLink(string listMethod, int page, int pageSize)
        {
            return Url.Link(nameof(listMethod), new { page, pageSize });
        }
    }
}