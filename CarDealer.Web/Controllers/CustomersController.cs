using CarDealer.Data.Models;
using CarDealer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult Index()
        {
            var customers = customerService.GetAll();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            customerService.Add(customer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var customer = customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            customerService.Update(customer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var customer = customerService.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            customerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}