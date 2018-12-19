using System.Linq;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Webservice.Models;

namespace WebService.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoritesController : Controller
    {
        private readonly IDataService _dataService;

        public FavoritesController(IDataService dataService)
        {
            _dataService = dataService;
        }

// Virker ikke ordentligt pt.
        [HttpGet("{username}", Name = nameof(GetFavorites))]
        public IActionResult GetFavorites(string username, int page = 0, int pageSize = 5)
        {
            var favoritesByUsername = _dataService.GetFavorites(username,page,pageSize).Select(CreateFavoriteListModel);
            var numberOfPages = HelperController.ComputeNumberOfPages(page, pageSize);

            var result = new
            {
                Page = page,
                First = HelperController.CreateLink(0, pageSize, nameof(GetFavorites), Url),
                Next = HelperController.CreateLinkToNextPage(page, pageSize, numberOfPages, nameof(GetFavorites), Url),
                Prev = HelperController.CreateLinkToPrevPage(page, pageSize, nameof(GetFavorites), Url),
                Items = favoritesByUsername
            };
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateFavoriteQuestion(int id, string username, string note)
        {
            var usernameExist = _dataService.CheckIfUsernameExist(username);
            if (usernameExist)
            {
                var newFavorite = _dataService.CreateFavoriteQuestion(id, username, note);
                return Ok(newFavorite);
            }
            else
            {
                return NotFound("Sorry, Username does not exist");
            }
        }

        //Helper
        private QuestionListModel CreateFavoriteListModel(Question favorite)
        {
            var model = Mapper.Map<QuestionListModel>(favorite);
            model.Url = Url.Link(nameof(GetFavorites), new { id = favorite.Id });
            return model;
        }

        private FavoriteModel CreateFavoriteModel(Question questions)
        {
            var model = Mapper.Map<FavoriteModel>(questions);
            model.Url = Url.Link(nameof(GetFavorites), new { id = questions.Id });
            return model;
        }
    }
}
