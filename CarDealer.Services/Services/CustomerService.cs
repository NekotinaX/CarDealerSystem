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
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext context;

        public CustomerService(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public Customer? GetById(int id)
        {
            return context.Customers.Find(id);
        }

        public void Add(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                throw new ArgumentException("First name is required.");
            }

            if (string.IsNullOrWhiteSpace(customer.LastName))
            {
                throw new ArgumentException("Last name is required.");
            }

            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var existingCustomer = context.Customers.Find(customer.Id);

            if (existingCustomer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = context.Customers.Find(id);

            if (customer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            context.Customers.Remove(customer);
            context.SaveChanges();
        }
    }
}