using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Webserver.Models;

namespace WebService.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : Controller
    {
        DataService _dataService;

        public PostController(DataService dataService)
        {
            _dataService = dataService;
        }
        
        [HttpGet]
        public IActionResult GetPosts()
        {
            var data = _dataService.GetPosts();

            return Ok(data);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var p = _dataService.GetPost(id);
            if (p == null) return NotFound();
            return Ok(p);
        }
        
        [HttpGet ("questions")]
        public IActionResult GetQuestions()
        {
            var data = _dataService.GetQuestions();

            return Ok(data);
        }
        [HttpGet("answers2question/{id}")]
        public IActionResult GetAnswersToQuestion(int id)
        {
            var answerPosts = _dataService.GetAnswersToQuestions(id);
            return Ok(answerPosts);
        }

        [HttpGet("searchSortByScore/{searchInput}")]
        public IActionResult GetAnswersToQuestion(string searchInput)
        {
            var answerPosts = _dataService.GetSearchQuestionsSortedByScore(searchInput);
            return Ok(answerPosts);
        }

        [HttpGet("commentById/{id}")]
        public IActionResult GetComment(int id)
        {
            var commentById = _dataService.GetComment(id);
            return Ok(commentById);
        }

        [HttpGet("commentsOfOnePost/{id}")]
        public IActionResult GetComments(int id)
        {
            var commentById = _dataService.GetComments(id,0,0);
            return Ok(commentById);
        }

        /*
                [HttpPost]
                public IActionResult AddCategory(CategoryPostAndPutModel category)
                {
                    _dataService.CreateCategory(category.Name);
                    return Ok();
                }

                [HttpPut("{id}")]
                public IActionResult UpdateCategory(int id, CategoryPostAndPutModel category)
                {
                    var cat = _dataService.UpdateCategory(id, category.Name);
                    if (cat == null) return NotFound();
                    return Ok(cat);
                }
                */
    }
}
