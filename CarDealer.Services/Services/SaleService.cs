using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data.Data;
using CarDealer.Data.Models;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Services.Services
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext context;

        public SaleService(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Sale> GetAll()
        {
            return context.Sales
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .ToList();
        }

        public Sale? GetById(int id)
        {
            return context.Sales
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .FirstOrDefault(s => s.Id == id);
        }

        public void Add(Sale sale)
        {
            var car = context.Cars.Find(sale.CarId);
            var customer = context.Customers.Find(sale.CustomerId);

            if (car == null)
            {
                throw new ArgumentException("Car not found.");
            }

            if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            if (car.IsSold)
            {
                throw new ArgumentException("This car is already sold.");
            }

            sale.SaleDate = DateTime.Now;
            car.IsSold = true;

            context.Sales.Add(sale);
            context.SaveChanges();
        }

        public void Update(Sale sale)
        {
            var existingSale = context.Sales.Find(sale.Id);

            if (existingSale == null)
            {
                throw new ArgumentException("Sale not found.");
            }

            existingSale.CarId = sale.CarId;
            existingSale.CustomerId = sale.CustomerId;
            existingSale.FinalPrice = sale.FinalPrice;
            existingSale.SaleDate = sale.SaleDate;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var sale = context.Sales.Find(id);

            if (sale == null)
            {
                throw new ArgumentException("Sale not found.");
            }

            context.Sales.Remove(sale);
            context.SaveChanges();
        }
    }
}