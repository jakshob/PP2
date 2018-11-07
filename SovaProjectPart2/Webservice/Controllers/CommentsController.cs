using System.Linq;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Mvc;

using Webservice.Models;

namespace WebService.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly IDataService _dataService;

        public CommentsController(IDataService dataService)
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
        public IActionResult GetCommentsByPostId(int id, int page=0, int pageSize=5 )
        {
            var comments = _dataService.GetCommentsByPostId(id, page, pageSize)
                .Select(CreateCommentListModel);

            var numberOfItems = _dataService.GetNumberOfComments(id);
            var numberOfPages = HelperController.ComputeNumberOfPages(pageSize, numberOfItems);

            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(GetCommentsByPostId), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(GetCommentsByPostId), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(GetCommentsByPostId), Url),
                NumberOfComments = numberOfItems,
                Items = comments
            };

            return Ok(result);
        }
        //Helpers

        private static CommentListModel CreateCommentListModel(Comment comment)
        {
            var model = Mapper.Map<CommentListModel>(comment);
            return model;
        }       
    }

}
