using Bussiness.Abstracts;
using Bussiness.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.Admin.Controllers
{
    [Area("Admin")]
 
    [Authorize(Roles = "Admin")]
    public class PortfolioController : Controller
    {
        IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public IActionResult Index()
        {
            var portfolios= _portfolioService.GetAllPortfolios();
            return View(portfolios);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _portfolioService.Create(portfolio);
            }
            catch (NotFoundException)
            {

                return NotFound();
            }
            catch (NotNullException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            try
            {
                _portfolioService.Delete(id);
            }
            catch (NotFoundException)
            {

                return NotFound();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var updatePortfolio=_portfolioService.GetPortfolio(x=>x.Id == id);
            if(updatePortfolio == null) {
                return NotFound();
            }
            return View(updatePortfolio);
        }
        [HttpPost]
        public IActionResult Update(Portfolio portfolio)
        {
            try
            {
                _portfolioService.Update(portfolio.Id, portfolio);
            }
            catch (NotFoundException)
            {

                return NotFound();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return View();
            }
            return RedirectToAction("Index");

        }
    }
}
