using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Webservice.Controller;
using Webservice.Models;

namespace WebService.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly DataService _dataService;        

        public PostController(DataService dataService)
        {
            _dataService = dataService;           
        }
        
        [HttpGet(Name = nameof(GetQuestions))]
        public IActionResult GetQuestions(int page = 0, int pageSize = 5)
        {
            var questions = _dataService.GetQuestions(page, pageSize)
                .Select(CreateQuestionListModel);

            var numberOfItems = _dataService.GetNumberOfQuestions();
            var numberOfPages = HelperController.ComputeNumberOfPages(pageSize, numberOfItems);
            
            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(GetQuestions), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(GetQuestions), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(GetQuestions), Url),
                Items = questions
            };
            return Ok(result);
        }
        

        [HttpGet("{id}", Name = nameof(GetQuestionById))]
        public IActionResult GetQuestionById(int id)
        {
            
            var question = _dataService.GetQuestion(id);
            if (question == null) return NotFound();
            var model = Mapper.Map<QuestionModel>(question);
            model.Url = Url.Link(nameof(GetQuestionById), new {id = question.Id});
            
            return Ok(question); 
        }

        [HttpGet("answersToQuestion/{id}")]
        public IActionResult GetAnswersToQuestion(int id, int page, int pageSize)
        {
            var answerPosts = _dataService.GetAnswersToQuestion(id,page,pageSize);
            return Ok(answerPosts);
        }

        [HttpGet("searchQuestionsSortByScore/{searchInput}")]
        public IActionResult GetSearchQuestionsSortByScore(string searchInput)
        {
            var answerPosts = _dataService.GetSearchQuestionsSortedByScore(searchInput,0,0);
            return Ok(answerPosts);
        }

        //Helpers

        private QuestionListModel CreateQuestionListModel(Question question)
        {
            var model = Mapper.Map<QuestionListModel>(question);
            model.Url = Url.Link(nameof(GetQuestionById), new { id = question.Id });
            return model;
        }
    }
}
