using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Data.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public decimal Price { get; set; }

        public int Mileage { get; set; }

        public string FuelType { get; set; } = null!;

        public bool IsSold { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
