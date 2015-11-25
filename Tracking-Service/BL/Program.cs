using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ManagerSalesModel;
using DAL.ReposytoryModel;
using DAL.ReposytoryModel.Interfaces;
using EntytiModel;

namespace BL
{
    class Program
    {
        static void Main(string[] args)
        {
            IGenericDataRepository<Sale> saleDataRepository = new SaleRepository();

            saleDataRepository.Add(new Sale(){Date = DateTime.Now, Product = new Product("Машина"), Customer = new Customer("Инокентий"), Manager = new Manager("Джек"), Summ = 256456548});

            // var d = saleDataRepository.GetAll();
        }
    }
}
