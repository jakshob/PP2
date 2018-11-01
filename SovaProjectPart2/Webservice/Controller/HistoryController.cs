using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Webservice;
using Webservice.Models;

namespace WebService.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : Controller
    {
        IDataService _dataService;

        public HistoryController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        public IActionResult GetHistory()
        {
            var historyByUsername = _dataService.GetHistory(Program.CurrentUsername,0,5);
            return Ok(historyByUsername);
        }
    }
}
