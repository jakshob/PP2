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
    [Route("api/history")]
    [ApiController]
    public class HistoryController : Controller
    {
        DataService _dataService;

        public HistoryController(DataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet("{username}")]
        public IActionResult GetHistory(string username)
        {
            var historyByUsername = _dataService.GetHistory(username,0,0);
            return Ok(historyByUsername);
        }


    }

}
