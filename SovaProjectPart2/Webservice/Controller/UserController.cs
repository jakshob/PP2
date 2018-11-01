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

namespace Webservice.Controllers
{
   
    [Route("/api/user")]
    [ApiController]
    public class UserController : Controller

    {
        IDataService _dataService;

        public UserController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpPost] 
        public IActionResult CreateUser(string username, string password, string salt) 
        {
            var newUser = _dataService.CreateUser(username, password, salt);
            return Ok(newUser); 
        }

        [HttpDelete]
        public IActionResult DeleteUser(string username, string password)
        {
            bool passwordMatch = _dataService.doesPasswordMatch(username, password); 
            if (passwordMatch)
            {
                _dataService.deleteUser(username, password);
                return Ok("User is deleted.");
            }
            else return NotFound("Password is wrong"); 
        }


    }
}
