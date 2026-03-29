using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data.Models;

namespace CarDealer.Services.Interfaces
{
    public interface ISaleService
    {
        IEnumerable<Sale> GetAll();
        Sale? GetById(int id);
        void Add(Sale sale);
        void Update(Sale sale);
        void Delete(int id);
    }
}