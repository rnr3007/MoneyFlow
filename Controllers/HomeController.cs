using System;
using System.Diagnostics;
using System.Threading.Tasks;
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

        [HttpGet("/")]
        public IActionResult Index()
        {
            if (!string.IsNullOrWhiteSpace(Request.Cookies["TokenBearer"]))
            {
                return Redirect(UriPath.DASHBOARD);
            }
            return View();
        }

        [HttpGet(UriPath.DASHBOARD)]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                string baseUrl = $"{Request.Scheme}://{Request.Host}";
                ViewData["Title"] = "Dashboard";
                SummaryViewModel summaryViewModel = new SummaryViewModel();
                string userId = Request.Headers["userId"];
                summaryViewModel.TotalCostByDate = await _expenseService.GetCostByDate(userId);
                summaryViewModel.MotivationList = (await _motivationService.GetMotivations(
                    Request.Headers["userId"],
                    1,
                    10,
                    "",
                    baseUrl
                )).Data;
                summaryViewModel.Savings = await _savingsService.GetSaving(
                    Request.Headers["userId"]
                );
                return View(summaryViewModel);
            } catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpGet(UriPath.ERROR)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet(UriPath.NOT_FOUND)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult PathNotFound()
        {
            return View();
        }
    }
}
