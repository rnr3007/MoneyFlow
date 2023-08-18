using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using MoneyFlow.Models;
using MoneyFlow.Services;

namespace MoneyFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpenseService _expenseService;

        private readonly MotivationService _motivationService;

        private readonly SavingsService _savingsService;

        public HomeController(ILogger<HomeController> logger, ExpenseService expenseService, MotivationService motivationService, SavingsService savingsService)
        {
            _logger = logger;
            _expenseService = expenseService;
            _motivationService = motivationService;
            _savingsService = savingsService;
        }

        [Authorize()]
        [HttpGet("/")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(UriPath.DASHBOARD);
            }
            return View();
        }

        [Authorize()]
        [HttpGet(UriPath.DASHBOARD)]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                ViewData["Title"] = "Dashboard";
                SummaryView summaryViewModel = new SummaryView();
                string userId = User.FindFirst(MiscConstants.USER_ID_CLAIM).Value;
                summaryViewModel.TotalCostByDate = await _expenseService.GetCostByDate(userId);
                summaryViewModel.MotivationList = (await _motivationService.GetMotivations(
                    userId,
                    1,
                    10,
                    ""
                )).Data;
                summaryViewModel.Savings = await _savingsService.GetSaving(
                    userId
                );
                return View(summaryViewModel);
            } catch (Exception e)
            {
                _logger.LogError(e.Message);
                Console.WriteLine(e);
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpGet(UriPath.ERROR)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet(UriPath.NOT_FOUND)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult PathNotFound()
        {
            return View();
        }
    }
}
