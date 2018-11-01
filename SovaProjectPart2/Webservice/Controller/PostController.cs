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
    [Route("api/posts")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IDataService _dataService;

        public PostController(IDataService dataService)
        {
            _dataService = dataService;
            
        }
        
        [HttpGet(Name = nameof(GetQuestions))]
        public IActionResult GetQuestions(int page = 0, int pageSize = 5)
        {
            var questions = _dataService.GetQuestions(page, pageSize)
                .Select(CreateQuestionListModel);

            var numberOfItems = _dataService.GetNumberOfQuestions();
            var numberOfPages = ComputeNumberOfPages(pageSize, numberOfItems);
            
            var result = new
            {
                Page = page,
                First = CreateLink(0, pageSize),
                Next = CreateLinkToNextPage(page, pageSize, numberOfPages),
                Prev = CreateLinkToPrevPage(page, pageSize),
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

        private static int ComputeNumberOfPages(int pageSize, int numberOfItems)
        {
            return (int)Math.Ceiling((double)numberOfItems / pageSize);
        }

        private string CreateLink(int page, int pageSize)
        {
            return Url.Link(nameof(GetQuestions), new { page, pageSize });
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
