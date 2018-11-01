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
    [Route("api/favorites")]
    [ApiController]
    public class FavoritesController : Controller
    {
        DataService _dataService;

        public FavoritesController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{username}")]
        public IActionResult GetFavorites(string username)
        {
            var favoritesByUsername = _dataService.GetFavorites(username,0,0);
            return Ok(favoritesByUsername);
        }

        [HttpPut("makeFavorite/{username}/{id}")]
        public IActionResult CreateFavoriteQuestion(int id, string username)
        {
            bool usernameExist = _dataService.CheckIfUsernameExist(username);
            if (usernameExist)
            {
                var newFavorite = _dataService.CreateFavoriteQuestion(id, username);
                return Ok(newFavorite);
            }
            else
            {
                return NotFound("Sorry, Username does not exist");
            }
        }
        [HttpPut("makeFavorite/{username}/{id}/{note}")]
        public IActionResult CreateFavoriteQuestion(int id, string username, string note)
        {
            bool usernameExist = _dataService.CheckIfUsernameExist(username);
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
    }

}
