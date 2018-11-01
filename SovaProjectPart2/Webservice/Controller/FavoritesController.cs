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
        IDataService _dataService;

        public FavoritesController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet("{username}")]
        public IActionResult GetFavorites(string username)
        {
            var favoritesByUsername = _dataService.GetFavorites(username,0,0);
            return Ok(favoritesByUsername);
        }


    }

}
