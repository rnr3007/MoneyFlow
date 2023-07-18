using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using MoneyFlow.Models;
using MoneyFlow.Services;
using MoneyFlow.Utils;

namespace MoneyFlow.Controllers
{
    [Route("/api/v1/user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("/user/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                string jwtToken =_userService.Register(user);
                return new OkObjectResult(new ResponseGenerator(
                    200,
                    SuccessMessage.REGISTER_SUCCESS,
                    new {token = jwtToken}
                ));
            } catch (FormatException e)
            {
                return new BadRequestObjectResult(new ResponseGenerator(400, e.Message));
            } catch (SqlException e)
            {
                if (e.Number == 2705)
                {
                    return new BadRequestObjectResult(new ResponseGenerator(400, e.Message));
                } else 
                {
                    return new StatusCodeResult(500);    
                }
            } catch (Exception e)
            {

                System.Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                string jwtToken = _userService.Login(user);
                return new OkObjectResult(new ResponseGenerator(
                    200,
                    SuccessMessage.REGISTER_SUCCESS,
                    new {token = jwtToken}
                ));
            } catch (DataException e){
                return new UnauthorizedObjectResult(new ResponseGenerator(
                    401,
                    e.Message
                ));
            } catch (Exception e)
            {
                System.Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                string jwtToken = _userService.Edit(user);
                return new OkObjectResult(new ResponseGenerator(
                    200,
                    SuccessMessage.UPDATE_PROFILE_SUCCESS,
                    new {token = jwtToken}
                ));
            } catch (DataException e){
                return new UnauthorizedObjectResult(new ResponseGenerator(
                    401,
                    e.Message
                ));
            } catch (Exception e)
            {
                System.Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("deactivate")]
        public async Task<IActionResult> Deactivate(User user)
        {
            try
            {
                _userService.Delete(user);
                return new OkObjectResult(new ResponseGenerator(
                    200,
                    SuccessMessage.DEACTIVATE_ACCOUNT_SUCCESS
                ));
            } catch (Exception e)
            {
                System.Console.WriteLine(e);
                return new StatusCodeResult(500);
            }
        }
    }
}