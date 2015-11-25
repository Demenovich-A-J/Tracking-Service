using System;

namespace DAL.ManagerSalesModel
{
    public class Sale
    {
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public int Summ { get; set; }
        public Manager Manager { get; set; }
    }
}