using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebServer;
using Webserver.Models;

namespace WebService.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class CategoriesController : Controller
    {
        DataService _dataService;

        public CategoriesController(DataService dataService)
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
