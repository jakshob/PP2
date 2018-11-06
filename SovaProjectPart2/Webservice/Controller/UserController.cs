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
        //SALT FJERNET MIDLERTIDIGT
        public IActionResult CreateUser(string username, string password, string salt) 
        {
            var newUser = _dataService.CreateUser(username, password);
            return Ok(newUser); 
        }

        [HttpDelete]
        public IActionResult DeleteUser(string username, string password)
        {
            int passwordMatch = _dataService.doesPasswordMatch(username, password); 
            if (passwordMatch == 1)
            {
                _dataService.deleteUser(username, password);
                return Ok("User is deleted.");
            }
            else
            {
                return ErrorMessages(passwordMatch);
            }
        }
        [HttpPut]
        public IActionResult EditUser(string username, string password, string newpassword)
        {
            int passwordMatch = _dataService.doesPasswordMatch(username, password);
            if (passwordMatch == 1)
            {
                _dataService.EditUserPassword(username, password, newpassword);
                return Ok("Your password has been changed");

            }
            else
            {
                return ErrorMessages(passwordMatch);
            }
           
        }

        [HttpGet("myPage/{username}")]
        public IActionResult GetUserPage(string username)
        {
            var data = _dataService.GetUserPage(username);
            return Ok(data);
        } 

        public IActionResult ErrorMessages(int number)
        {
            if (number == 2)
            {
                return NotFound("User does not exist");
            }
            else if (number == 3)
            {
                return NotFound("Password for user is wrong");
            }
            else
            {
                return NotFound("There was a problem? - ErrorMessage is not yet written");
            }
        }


    }
}
