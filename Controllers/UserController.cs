using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using MoneyFlow.Models;
using MoneyFlow.Services;
using MoneyFlow.Utils;
using iv = MoneyFlow.Utils.InputValidator;

namespace MoneyFlow.Controllers
{
    [Route("/user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(UserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    await _userService.Register(user);    
                }
                return View(user);
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return View(user);
            }
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                if (iv.IsNotEmpty(user.Password) && iv.IsNotEmpty(user.Username)) 
                {
                    await _userService.Login(user);
                }
                return View(user);
            } catch(DataException e) 
            {
                if (e.Message == ErrorMessage.WRONG_PASSWORD) 
                { 
                    ModelState.AddModelError("Password", e.Message); 
                } else if (e.Message == ErrorMessage.USER_NOT_FOUND) 
                {
                    ModelState.AddModelError("Username", e.Message); 
                }
                return View(user);
            } catch (Exception e)
            {   
                Console.WriteLine(e);
                return View(user);
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                _userService.Edit(user);
                return new OkObjectResult(new ResponseGenerator(
                    200,
                    SuccessMessage.UPDATE_PROFILE_SUCCESS
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