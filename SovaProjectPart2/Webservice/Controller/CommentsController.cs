using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [HttpGet("fromPost/{id}")]
        public IActionResult GetComments(int id)
        {
            var commentById = _dataService.GetCommentsByPostId(id, 0, 0);
            return Ok(commentById);
        }

    }

}
