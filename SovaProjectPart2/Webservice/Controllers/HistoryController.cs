using System.Linq;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Webservice;
using Webservice.Models;

namespace WebService.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : Controller
    {
        private readonly IDataService _dataService;

        public HistoryController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        public IActionResult GetHistory()
        {
            var historyByUsername = _dataService.GetHistory(Program.CurrentUsername,0,5).Select(CreateHistoryListModel);
            return Ok(historyByUsername);
        }

        private static HistoryListModel CreateHistoryListModel(History history)
        {
            var model = Mapper.Map<HistoryListModel>(history);
            model.Url = "http://localhost:5001/api/posts/TraverseSearchResults/" + model.SearchText;
            return model;
        }
    }
}
