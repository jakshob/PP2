using System;
using Microsoft.AspNetCore.Mvc;


namespace WebService.Controllers
{
    public static class HelperController 
    {
        public static int ComputeNumberOfPages(int pageSize, int numberOfItems)
        {
            return (int)Math.Ceiling((double)numberOfItems / pageSize);
        }

        public static string CreateLink(int page, int pageSize, string nameof, IUrlHelper url)
        {
            var result = url.Link(nameof, new { page, pageSize });

            return result;
        }

        public static string CreateLinkToNextPage(int page, int pageSize, int numberOfPages, string nameof, IUrlHelper url)
        {
            return page >= numberOfPages - 1
                ? null
                : CreateLink(page = page + 1, pageSize, nameof, url);
        }

        public static string CreateLinkToPrevPage(int page, int pageSize, string nameof, IUrlHelper url)
        {
            return page == 0
                ? null
                : CreateLink(page - 1, pageSize, nameof, url);
        }

    }
}
