﻿using System;
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
    [Route("api/posts")]
    [ApiController]
    public class PostController : Controller
    {
        DataService _dataService;

        public PostController(DataService dataService)
        {
            _dataService = dataService;
        }
        
        [HttpGet]
        public IActionResult GetQuestions()
        {
            var data = _dataService.GetQuestions(0,0);

            return Ok(data);
        }
        

        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
        {
            var p = _dataService.GetQuestion(id);
            if (p == null) return NotFound();
            return Ok(p); 
        }

        [HttpGet("answersToQuestion/{id}")]
        public IActionResult GetAnswersToQuestion(int id)
        {
            var answerPosts = _dataService.GetAnswersToQuestion(id,0,0);
            return Ok(answerPosts);
        }

        [HttpGet("searchQuestionsSortByScore/{searchInput}")]
        public IActionResult GetAnswersToQuestion(string searchInput)
        {
            var answerPosts = _dataService.GetSearchQuestionsSortedByScore(searchInput,0,0);
            return Ok(answerPosts);
        }

    }
}
