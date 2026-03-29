using CarDealer.Data.Models;
using CarDealer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Index()
        {
            var cars = carService.GetAll();
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car car)
        {
            if (!ModelState.IsValid)
            {
                return View(car);
            }

            car.IsSold = false;
            carService.Add(car);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var car = carService.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (!ModelState.IsValid)
            {
                return View(car);
            }

            carService.Update(car);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var car = carService.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            carService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}