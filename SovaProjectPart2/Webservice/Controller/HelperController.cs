using System;
using Microsoft.AspNetCore.Mvc;


namespace Webservice.Controller
{
    public static class HelperController 
    {
        public static int ComputeNumberOfPages(int pageSize, int numberOfItems)
        {
            return (int)Math.Ceiling((double)numberOfItems / pageSize);
        }

        public static string CreateLink(int page, int pageSize, string action, IUrlHelper url)
        {
            var result = url.Link(action, new { page, pageSize });

            return result;
        }

        public static string CreateLinkToNextPage(int page, int pageSize, int numberOfPages, string action, IUrlHelper url)
        {
            return page >= numberOfPages - 1
                ? null
                : CreateLink(page = page + 1, pageSize, action, url);
        }

        public static string CreateLinkToPrevPage(int page, int pageSize, string action, IUrlHelper url)
        {
            return page == 0
                ? null
                : CreateLink(page - 1, pageSize, action, url);
        }

    }
}
