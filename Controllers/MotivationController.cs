using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using MoneyFlow.Data;
using MoneyFlow.Models;
using MoneyFlow.Services;
using iv = MoneyFlow.Utils.Validator.InputValidator;

namespace MoneyFlow.Controllers
{
    [Authorize]
    [Route(UriPath.MOTIVATIONS)]
    public class MotivationController : Controller
    {
        private readonly MotivationService _motivationService;

        private readonly ILogger<MotivationController> _logger;

        public MotivationController(MotivationService motivationService, ILogger<MotivationController> logger)
        {
            _logger = logger;
            _motivationService = motivationService;
        }

        [HttpGet()]
        public async Task<IActionResult> Motivations(string page, string limit, string keyword)
        {
            try
            {
                ViewData["Title"] = "Target Barang";
                TableView<Motivation> tableView = await _motivationService.GetMotivations(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    iv.GetValidIntegerFromString(page, 1),
                    iv.GetValidIntegerFromString(limit, 10),
                    keyword ?? ""
                );

                return View(tableView);
            } catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpGet(UriPath.MOTIVATIONS_CREATE)]
        public IActionResult CreateMotivations()
        {
            ViewData["Title"] = "Buat barang impian";
            return View();
        }

        [HttpPost(UriPath.MOTIVATIONS_CREATE)]
        public async Task<IActionResult> CreateMotivations(Motivation motivation, IFormFile formFile)
        {
            try
            {
                if (formFile == null || !iv.IsFileValid(formFile, new string[]{"image"}))
                {
                    throw new InvalidDataException(ErrorMessage.TARGET_IMAGE_EMPTY);
                } 


                ModelState.Remove("UserId");
                if (ModelState.IsValid)
                {
                    await _motivationService.CreateMotivations(
                        User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                        motivation,
                        formFile
                    );
                    return Redirect(UriPath.MOTIVATIONS);
                }
                return View(motivation);
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(InvalidDataException))
                {
                    ModelState.AddModelError("FormFile", e.Message);
                    return View(motivation);
                }
                _logger.LogError(e.Message);
                return Redirect(UriPath.ERROR);
            }
        }
    
    
        [HttpGet(UriPath.MOTIVATIONS_UPDATE)]
        public async Task<IActionResult> UpdateMotivations(string motivationId)
        {
            try
            {
                ViewData["Title"] = "Ubah barang impian";
                Motivation motivation = await _motivationService.GetMotivation(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    motivationId
                );
                return View(motivation);
            } catch (Exception e)
            {
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.MOTIVATIONS);
                }
                _logger.LogError(e.Message);
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpPost(UriPath.MOTIVATIONS_UPDATE)]
        public async Task<IActionResult> UpdateMotivations(string motivationId, Motivation motivation, IFormFile formFile)
        {
            try
            {
                if (formFile == null || !iv.IsFileValid(formFile, new string[]{"image"}))
                {
                    throw new InvalidDataException(ErrorMessage.TARGET_IMAGE_EMPTY);
                } 

                ModelState.Remove("UserId");
                if (ModelState.IsValid)
                {
                    await _motivationService.UpdateMotivation(
                        User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                        motivationId,
                        motivation,
                        formFile
                    );
                    return Redirect(UriPath.MOTIVATIONS);
                }
                return View(motivation);
                
            } catch (Exception e)
            {
                Console.WriteLine(e);
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.MOTIVATIONS);
                } else if (eType == typeof(InvalidDataException))
                {
                    ModelState.AddModelError("FormFile", e.Message);
                    return View(motivation);
                }
                _logger.LogError(e.Message);
                return Redirect(UriPath.ERROR);
            }
        }
    
        [HttpGet(UriPath.MOTIVATIONS_DELETE)]
        public async Task<IActionResult> DeleteMotivations(string motivationId)
        {
            try
            {
                await _motivationService.DeleteMotivation(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    motivationId
                );
                return Redirect(UriPath.MOTIVATIONS);
            } catch (Exception e)   
            {
                Type eType = e.GetType();
                if (eType == typeof(DataException))
                {
                    return Redirect(UriPath.MOTIVATIONS);
                }
                _logger.LogError(e.Message);
                return Redirect(UriPath.ERROR);
            }
        }

        [HttpPost(UriPath.MOTIVATIONS_BUY)]
        public async Task<IActionResult> BuyMotivations(string motivationId)
        {
            try
            {
                await _motivationService.BuyMotivation(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    motivationId
                );
                return Redirect(UriPath.DASHBOARD);
            } catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return Redirect(UriPath.ERROR);
            }
        }
    }
}