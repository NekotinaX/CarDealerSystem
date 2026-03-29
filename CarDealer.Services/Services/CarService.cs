using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data.Data;
using CarDealer.Data.Models;
using CarDealer.Services.Interfaces;

namespace CarDealer.Services.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext context;

        public CarService(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Car> GetAll()
        {
            return context.Cars.ToList();
        }

        public Car? GetById(int id)
        {
            return context.Cars.Find(id);
        }

        public void Add(Car car)
        {
            if (string.IsNullOrWhiteSpace(car.Brand))
            {
                throw new ArgumentException("Brand is required.");
            }

            if (string.IsNullOrWhiteSpace(car.Model))
            {
                throw new ArgumentException("Model is required.");
            }

            context.Cars.Add(car);
            context.SaveChanges();
        }

        public void Update(Car car)
        {
            var existingCar = context.Cars.Find(car.Id);

            if (existingCar == null)
            {
                throw new ArgumentException("Car not found.");
            }

            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
            existingCar.Mileage = car.Mileage;
            existingCar.FuelType = car.FuelType;
            existingCar.IsSold = car.IsSold;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = context.Cars.Find(id);

            if (car == null)
            {
                throw new ArgumentException("Car not found.");
            }

            context.Cars.Remove(car);
            context.SaveChanges();
        }
    }
}