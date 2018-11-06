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
            
            return Ok(question); 
        }

        [HttpGet("answersToQuestion/{id}", Name = nameof(GetAnswersToQuestion))]
        public IActionResult GetAnswersToQuestion(int id, int page = 0, int pageSize = 5)
        {
            
            var answerPosts = _dataService.GetAnswersToQuestion(id,page,pageSize)
                .Select(CreateAnswerListModel);

            var numberOfItems = _dataService.GetNumberOfAnswers(id);
            var numberOfPages = HelperController.ComputeNumberOfPages(pageSize, numberOfItems);
            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(GetAnswersToQuestion), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(GetAnswersToQuestion), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(GetAnswersToQuestion), Url),
                NumberOfAnswers = numberOfItems,
                Items = answerPosts
            };
            return Ok(result);
        }

        [HttpGet("searchQuestionsSortByScore/{searchInput}", Name = nameof(GetSearchQuestionsSortByScore))]
        public IActionResult GetSearchQuestionsSortByScore(string searchInput, int page = 0, int pageSize = 5)
        {
            var answerPosts = _dataService.GetSearchQuestionsSortedByScore(searchInput, page, pageSize)
                .Select(CreateQuestionListModel);
            var numberOfPages = HelperController.ComputeNumberOfPages(page, pageSize);

            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(GetSearchQuestionsSortByScore), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(GetSearchQuestionsSortByScore), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(GetSearchQuestionsSortByScore), Url),
                Items = answerPosts
            };
            return Ok(result);
        }

        //Helpers

        private QuestionListModel CreateQuestionListModel(Question question)
        {
            var model = Mapper.Map<QuestionListModel>(question);
            model.Url = Url.Link(nameof(GetQuestionById), new { id = question.Id });
            return model;
        }
        private AnswerListModel CreateAnswerListModel (Answer answer)
        {
            var model = Mapper.Map<AnswerListModel>(answer);
            model.Url = Url.Link(nameof(GetAnswersToQuestion), new { id = answer.Id });
            return model;
        }
    }
}
