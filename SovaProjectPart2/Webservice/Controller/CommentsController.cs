using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Webservice.Models;

namespace WebService.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : Controller
    {
        DataService _dataService;

        public CommentsController(DataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var commentById = _dataService.GetComment(id);
            return Ok(commentById);
        }

        [HttpGet("fromPost/{id}", Name = nameof(GetCommentsByPostId))]
        public IActionResult GetCommentsByPostId(int id, int page=0, int pageSize = 5 )
        {
            var comments = _dataService.GetCommentsByPostId(id, page, pageSize)
                .Select(CreateCommentListModel);

            var numberOfItems = _dataService.GetNumberOfComments();
            var numberOfPages = ComputeNumberOfPages(pageSize, numberOfItems);

            var result = new
            {
                Page = page,
                First = CreateLink(0, pageSize),
                Next = CreateLinkToNextPage(page, pageSize, numberOfPages),
                Prev = CreateLinkToPrevPage(page, pageSize),
                Items = comments
            };

            return Ok(result);
        }
        //Helpers

        private CommentListModel CreateCommentListModel(Comment comment)
        {
            var model = Mapper.Map<CommentListModel>(comment);
            return model;
        }

        private static int ComputeNumberOfPages(int pageSize, int numberOfItems)
        {
            return (int)Math.Ceiling((double)numberOfItems / pageSize);
        }

        private string CreateLink(int page, int pageSize)
        {
            return Url.Link(nameof(GetCommentsByPostId), new { page, pageSize });
        }

        private string CreateLinkToNextPage(int page, int pageSize, int numberOfPages)
        {
            return page >= numberOfPages - 1
                ? null
                : CreateLink(page = page + 1, pageSize);
        }

        private string CreateLinkToPrevPage(int page, int pageSize)
        {
            return page == 0
                ? null
                : CreateLink(page - 1, pageSize);
        }
    }

}
