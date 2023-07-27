using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using MoneyFlow.Models;
using MoneyFlow.Services;
using MoneyFlow.Utils;
using iv = MoneyFlow.Utils.Validator.InputValidator;

namespace MoneyFlow.Controllers
{
    [Route("/user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        private readonly ILogger<UserController> _logger;

        private readonly static string baseUrl = Startup.StaticConfiguration.GetSection("BASE_URL").Value;

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
                    return Redirect($"{baseUrl}/user/login");
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
                if (!string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Username)) 
                {
                    await _userService.Login(user);
                    return Redirect($"{baseUrl}/expenses");
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

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("TokenBearer");
                return Redirect($"{baseUrl}/user/login");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect($"{baseUrl}/");
            }
        }
    }
}