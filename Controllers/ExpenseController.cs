using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using MoneyFlow.Models;
using MoneyFlow.Data;
using MoneyFlow.Services;
using iv = MoneyFlow.Utils.Validator.InputValidator;
using de = MoneyFlow.Utils.DataExtractor;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace MoneyFlow.Controllers
{
    [Authorize]
    [Route(UriPath.EXPENSE)]
    public class ExpenseController : Controller
    {
        private readonly ILogger<ExpenseController> _logger;

        private readonly ExpenseService _expenseService;

        public ExpenseController(ILogger<ExpenseController> logger, ExpenseService expenseService)
        {
            _logger = logger;
            _expenseService = expenseService;
        }

        [HttpGet(UriPath.EXPENSE_LIST)]
        public async Task<IActionResult> Expenses(string page, string limit, string keyword, string order, string filters)
        {
            try
            {
                ViewData["Title"] = "Pengeluaran";

                filters = filters == null ?
                    "" :
                    HttpUtility.UrlDecode(filters);

                de.TryDeserializeList(filters, out List<int> listObject);
                TableView<Expense> userExpenses = await _expenseService.GetExpenses(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    iv.GetValidIntegerFromString(page, 1),
                    iv.GetValidIntegerFromString(limit, 10),
                    keyword ?? "",
                    order ?? "",
                    listObject
                );

                return View(userExpenses);
            } catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpGet(UriPath.EXPENSE_DELETE)]
        public async Task<IActionResult> DeleteExpense(string expenseId)
        {
            try
            {
                await _expenseService.DeleteExpense(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    expenseId
                );
                return Redirect(UriPath.EXPENSE_LIST);
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.EXPENSE_LIST);
                }
                _logger.LogError("DeleteExpense", e);
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpGet(UriPath.EXPENSE_CREATE)]
        public IActionResult CreateExpense()
        {
            ViewData["Title"] = "Tambah Pengeluaran";

            return View();
        }

        [HttpPost(UriPath.EXPENSE_CREATE)]
        public async Task<IActionResult> CreateExpense(Expense userExpense, IFormFile formFile)
        {
            try
            {
                userExpense.UserId = User.FindFirst(MiscConstants.USER_ID_CLAIM).Value;
                ModelState.Remove("UserId");
                if (formFile != null) {
                    iv.IsFileValid(formFile, new string[]{"image", "pdf"});
                }
                if (ModelState.IsValid)
                {
                    await _expenseService.CreateExpense(
                        User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                        userExpense,
                        formFile
                    );
                    return Redirect(UriPath.EXPENSE_LIST);
                }
                return View(userExpense);
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(InvalidDataException))
                {
                    ModelState.AddModelError("FormFile", e.Message);
                    return View(userExpense);
                }
                return View(UriPath.ERROR);
            }
        }
    
        [HttpGet(UriPath.EXPENSE_UPDATE)]
        public async Task<IActionResult> UpdateExpense(string expenseId)
        {
            try
            {
                ViewData["Title"] = "Ubah Pengeluaran";
                return View(await _expenseService.GetExpense(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    expenseId
                ));
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.EXPENSE_LIST);
                }
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpPost(UriPath.EXPENSE_UPDATE)]
        public async Task<IActionResult> UpdateExpense(string expenseId, Expense expense, IFormFile formFile)
        {
            ViewData["Title"] = "Ubah Pengeluaran";
            try
            {
                ModelState.Remove("UserId");
                if (formFile != null) {
                    iv.IsFileValid(formFile, new string[]{"image", "pdf"});
                }
                if (ModelState.IsValid)
                {
                    await _expenseService.UpdateExpense(
                        User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                        expenseId,
                        expense,
                        formFile
                    );
                    return Redirect(UriPath.EXPENSE_LIST);
                }
                return View(expense);
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.EXPENSE_LIST);
                } else if (eType == typeof(InvalidDataException))
                {
                    ModelState.AddModelError("FormFile", e.Message);
                    return View(expense);
                }
                return Redirect(UriPath.ERROR);
            }
        }
    }
}