using CarDealer.Data.Data;
using CarDealer.Data.Models;
using CarDealer.Services.Interfaces;
using CarDealer.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

var connectionString = configuration.GetConnectionString("DefaultConnection");

services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

services.AddScoped<ICarService, CarService>();
services.AddScoped<ICustomerService, CustomerService>();
services.AddScoped<ISaleService, SaleService>();

var serviceProvider = services.BuildServiceProvider();

var carService = serviceProvider.GetRequiredService<ICarService>();
var customerService = serviceProvider.GetRequiredService<ICustomerService>();
var saleService = serviceProvider.GetRequiredService<ISaleService>();

bool exit = false;

while (!exit)
{
    Console.WriteLine("=== CAR DEALER SYSTEM ===");
    Console.WriteLine("1. Manage Cars");
    Console.WriteLine("2. Manage Customers");
    Console.WriteLine("3. Manage Sales");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    try
    {
        switch (choice)
        {
            case "1":
                ManageCars(carService);
                break;
            case "2":
                ManageCustomers(customerService);
                break;
            case "3":
                ManageSales(saleService);
                break;
            case "0":
                exit = true;
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine();
}

static void ManageCars(ICarService carService)
{
    Console.WriteLine("=== CARS ===");
    Console.WriteLine("1. Add Car");
    Console.WriteLine("2. List Cars");
    Console.WriteLine("3. Update Car");
    Console.WriteLine("4. Delete Car");
    Console.Write("Choose: ");
    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            var car = new Car();

            Console.Write("Brand: ");
            car.Brand = Console.ReadLine() ?? "";

            Console.Write("Model: ");
            car.Model = Console.ReadLine() ?? "";

            Console.Write("Year: ");
            car.Year = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Price: ");
            car.Price = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Mileage: ");
            car.Mileage = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Fuel Type: ");
            car.FuelType = Console.ReadLine() ?? "";

            car.IsSold = false;

            carService.Add(car);
            Console.WriteLine("Car added successfully.");
            break;

        case "2":
            var cars = carService.GetAll();

            foreach (var c in cars)
            {
                Console.WriteLine($"{c.Id}: {c.Brand} {c.Model} | Year: {c.Year} | Price: {c.Price:F2} | Mileage: {c.Mileage} | Fuel: {c.FuelType} | Sold: {c.IsSold}");
            }
            break;

        case "3":
            Console.Write("Car ID: ");
            int updateId = int.Parse(Console.ReadLine() ?? "0");

            var existingCar = carService.GetById(updateId);

            if (existingCar == null)
            {
                Console.WriteLine("Car not found.");
                return;
            }

            Console.Write("New Brand: ");
            existingCar.Brand = Console.ReadLine() ?? "";

            Console.Write("New Model: ");
            existingCar.Model = Console.ReadLine() ?? "";

            Console.Write("New Year: ");
            existingCar.Year = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("New Price: ");
            existingCar.Price = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("New Mileage: ");
            existingCar.Mileage = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("New Fuel Type: ");
            existingCar.FuelType = Console.ReadLine() ?? "";

            carService.Update(existingCar);
            Console.WriteLine("Car updated successfully.");
            break;

        case "4":
            Console.Write("Car ID: ");
            int deleteId = int.Parse(Console.ReadLine() ?? "0");

            carService.Delete(deleteId);
            Console.WriteLine("Car deleted successfully.");
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

static void ManageCustomers(ICustomerService customerService)
{
    Console.WriteLine("=== CUSTOMERS ===");
    Console.WriteLine("1. Add Customer");
    Console.WriteLine("2. List Customers");
    Console.WriteLine("3. Update Customer");
    Console.WriteLine("4. Delete Customer");
    Console.Write("Choose: ");
    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            var customer = new Customer();

            Console.Write("First Name: ");
            customer.FirstName = Console.ReadLine() ?? "";

            Console.Write("Last Name: ");
            customer.LastName = Console.ReadLine() ?? "";

            Console.Write("Phone: ");
            customer.Phone = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            customer.Email = Console.ReadLine() ?? "";

            customerService.Add(customer);
            Console.WriteLine("Customer added successfully.");
            break;

        case "2":
            var customers = customerService.GetAll();

            foreach (var c in customers)
            {
                Console.WriteLine($"{c.Id}: {c.FirstName} {c.LastName} | Phone: {c.Phone} | Email: {c.Email}");
            }
            break;

        case "3":
            Console.Write("Customer ID: ");
            int updateId = int.Parse(Console.ReadLine() ?? "0");

            var existingCustomer = customerService.GetById(updateId);

            if (existingCustomer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.Write("New First Name: ");
            existingCustomer.FirstName = Console.ReadLine() ?? "";

            Console.Write("New Last Name: ");
            existingCustomer.LastName = Console.ReadLine() ?? "";

            Console.Write("New Phone: ");
            existingCustomer.Phone = Console.ReadLine() ?? "";

            Console.Write("New Email: ");
            existingCustomer.Email = Console.ReadLine() ?? "";

            customerService.Update(existingCustomer);
            Console.WriteLine("Customer updated successfully.");
            break;

        case "4":
            Console.Write("Customer ID: ");
            int deleteId = int.Parse(Console.ReadLine() ?? "0");

            customerService.Delete(deleteId);
            Console.WriteLine("Customer deleted successfully.");
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

static void ManageSales(ISaleService saleService)
{
    Console.WriteLine("=== SALES ===");
    Console.WriteLine("1. Add Sale");
    Console.WriteLine("2. List Sales");
    Console.WriteLine("3. Delete Sale");
    Console.Write("Choose: ");
    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            var sale = new Sale();

            Console.Write("Car ID: ");
            sale.CarId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Customer ID: ");
            sale.CustomerId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Final Price: ");
            sale.FinalPrice = decimal.Parse(Console.ReadLine() ?? "0");

            saleService.Add(sale);
            Console.WriteLine("Sale added successfully.");
            break;

        case "2":
            var sales = saleService.GetAll();

            foreach (var s in sales)
            {
                Console.WriteLine($"{s.Id}: {s.Car.Brand} {s.Car.Model} sold to {s.Customer.FirstName} {s.Customer.LastName} for {s.FinalPrice:F2} on {s.SaleDate}");
            }
            break;

        case "3":
            Console.Write("Sale ID: ");
            int deleteId = int.Parse(Console.ReadLine() ?? "0");

            saleService.Delete(deleteId);
            Console.WriteLine("Sale deleted successfully.");
            break;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}