using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using MoneyFlow.Models;
using MoneyFlow.Data;
using MoneyFlow.Services;
using iv = MoneyFlow.Utils.Validator.InputValidator;
using de = MoneyFlow.Utils.DataExtractor;
using System.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MoneyFlow.Controllers
{
    [Authorize]
    [Route(UriPath.INCOMES)]
    public class IncomeController : Controller
    {
        private readonly IncomeService _incomeService;

        private readonly ILogger<IncomeController> _logger;

        public IncomeController(IncomeService incomeService, ILogger<IncomeController> logger)
        {
            _logger = logger;
            _incomeService = incomeService;
        }

        [HttpGet()]
        public async Task<IActionResult> Incomes(string page, string limit, string keyword, string order, string filters)
        {
            try
            {
                ViewData["Title"] = "Pendapatan";

                filters = filters == null ?
                    "" :
                    HttpUtility.UrlDecode(filters);
                de.TryDeserializeList(filters, out List<int> filterList);
                TableView<Income> userIncomes = await _incomeService.GetIncomes(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    iv.GetValidIntegerFromString(page, 1),
                    iv.GetValidIntegerFromString(limit, 10),
                    keyword ?? "",
                    order ?? "",
                    filterList
                );

                return View(userIncomes);
            } catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpGet(UriPath.INCOMES_CREATE)]
        public IActionResult CreateIncomes()
        {
            return View();
        }

        [HttpPost(UriPath.INCOMES_CREATE)]
        public async Task<IActionResult> CreateIncomes(Income income)
        {
            try
            {
                ModelState.Remove("UserId");
                if (ModelState.IsValid)
                {
                    await _incomeService.CreateIncomes(
                        User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                        income
                    );
                    return Redirect(UriPath.INCOMES);
                }
                return View(income);
            } catch (Exception)
            {
                return View(income);
            }
        }

        [HttpGet(UriPath.INCOMES_UPDATE)]
        public async Task<IActionResult> UpdateIncomes(string incomeId)
        {
            try
            {
                ViewData["Title"] = "Ubah Pendapatan";
                return View(await _incomeService.GetIncome(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    incomeId
                ));
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.INCOMES);
                }
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpPost(UriPath.INCOMES_UPDATE)]
        public async Task<IActionResult> UpdateIncomes(string incomeId, Income income)
        {
            try
            {
                ModelState.Remove("UserId");
                if (ModelState.IsValid)
                {
                    await _incomeService.UpdateIncomes(
                        User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                        incomeId,
                        income
                    );
                    return Redirect(UriPath.INCOMES);
                }
                return View(income);
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.INCOMES);
                }
                return Redirect(UriPath.ERROR);
            }
        }
    
        [HttpGet(UriPath.INCOMES_DELETE)]
        public async Task<IActionResult> DeleteIncomes(string incomeId)
        {
            try
            {
                await _incomeService.DeleteIncomes(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    incomeId
                );
                return Redirect(UriPath.INCOMES);
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.INCOMES);
                }
                _logger.LogError("DeleteIncomes", e);
                return Redirect(UriPath.ERROR);
            }
        }
    }
}