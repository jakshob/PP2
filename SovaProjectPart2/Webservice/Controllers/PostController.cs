using System.Linq;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
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
                NumberOfItems = numberOfItems,
                Items = questions
            };
            return Ok(result);
        }


        [HttpGet("{id}", Name = nameof(GetQuestionById))]
        public IActionResult GetQuestionById(int id)
        {
            var question = _dataService.GetQuestion(id);
            if (question == null) return NotFound();
            var model = CreateQuestionModel(question);
            model.Url = Url.Link(nameof(GetAnswersToQuestion), new { id });
            model.PostId = id;
            return Ok(model);
        }

        [HttpGet("answersToQuestion/{id}", Name = nameof(GetAnswersToQuestion))]
        public IActionResult GetAnswersToQuestion(int id, int page = 0, int pageSize = 5)
        {

            var answerPosts = _dataService.GetAnswersToQuestion(id, page, pageSize)
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

        [HttpGet("relevantWords/{wordInput}", Name = nameof(GetRelevantWords))]
        public IActionResult GetRelevantWords(string wordInput, int page = 0, int pageSize = 20)
        {
            var answerPosts = _dataService.GetRelevantWords(wordInput, page, pageSize)
                .Select(CreateWordListModel);
            var numberOfPages = HelperController.ComputeNumberOfPages(page, pageSize);

            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(GetRelevantWords), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(GetRelevantWords), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(GetRelevantWords), Url),
                Items = answerPosts
            };
            return Ok(answerPosts);
        }

		[HttpGet("termNetwork/{wordInput}", Name = nameof(GetForceGraph))]
		public IActionResult GetForceGraph(string word) {
			var result = _dataService.GetForceGraph(word);
			return Ok(result);
		}

		[HttpGet("searchQuestionsSortByScore/{searchInput}", Name = nameof(GetSearchQuestionsSortByScore))]
        public IActionResult GetSearchQuestionsSortByScore(string searchInput, int page = 0, int pageSize = 5)
        {
            var questionPosts = _dataService.GetSearchQuestionsSortedByScore(searchInput, page, pageSize)
                .Select(CreateSearchListModel);
            var numberOfPages = HelperController.ComputeNumberOfPages(page, pageSize);

            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(GetSearchQuestionsSortByScore), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(GetSearchQuestionsSortByScore), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(GetSearchQuestionsSortByScore), Url),
                Items = questionPosts
            };
            return Ok(result);
        }
        

        [HttpGet("TraverseSearchResults/{searchInput}", Name =nameof(TraverseSearchResults))]
        public IActionResult TraverseSearchResults(string searchInput, int page = 0, int pageSize = 5)
        {
            var answerPosts = _dataService.TraverseSearchResults(searchInput, "Mogens", page, pageSize)
                .Select(CreateQuestionListModel);
            var numberOfPages = HelperController.ComputeNumberOfPages(page, pageSize);

            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(TraverseSearchResults), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(TraverseSearchResults), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(TraverseSearchResults), Url),
                Items = answerPosts
            };

            return Ok(result);
        }
        /*
        [HttpGet("searchQuestionsSortByScore/{searchInput}", Name = nameof(GetSearchQuestionsSortByScore))]
        public IActionResult GetSearchQuestionsSortByScore(string searchInput, int page = 0, int pageSize = 5)
        {
            var answerPosts = _dataService.SearchSova(searchInput, "Mogens", page, pageSize)
                .Select(CreateQuestionListModel);
            var numberOfPages = HelperController.ComputeNumberOfPages(page, pageSize);

            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(TraverseSearchResults), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(TraverseSearchResults), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(TraverseSearchResults), Url),
                Items = answerPosts
            };
            return Ok(result);
        }
        */

        //Helpers

        private QuestionListModel CreateQuestionListModel(Question question)
        {
            var model = Mapper.Map<QuestionListModel>(question);
            model.Url = Url.Link(nameof(GetQuestionById), new { id = question.Id });
            model.PostId = question.Id.ToString();
            return model;
        }

        private static AnswerListModel CreateAnswerListModel (Answer answer)
        {
            var model = Mapper.Map<AnswerListModel>(answer);
            model.PostId = answer.Id;
            return model;
        }

        private QuestionModel CreateQuestionModel(Question question)
        {
            var model = Mapper.Map<QuestionModel>(question);
            model.Url = Url.Link(nameof(GetQuestionById), new { id = question.Id });
            return model;
        }

        private static WordListModel CreateWordListModel(RelevantWord word)
        {
            var model = Mapper.Map<WordListModel>(word);
            return model;
        }

        private SearchListModel CreateSearchListModel(SearchResult searchResult)
        {
            var model = Mapper.Map<SearchListModel>(searchResult);

            return model;
        }
    }
}
