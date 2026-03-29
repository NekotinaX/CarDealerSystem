using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Data.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; } = null!;

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public DateTime SaleDate { get; set; }

        public decimal FinalPrice { get; set; }
    }
}