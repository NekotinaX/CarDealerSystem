using CarDealer.Data.Models;
using CarDealer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISaleService saleService;

        public SalesController(ISaleService saleService)
        {
            this.saleService = saleService;
        }

        public IActionResult Index()
        {
            var sales = saleService.GetAll();
            return View(sales);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return View(sale);
            }

            saleService.Add(sale);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var sale = saleService.GetById(id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            saleService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}