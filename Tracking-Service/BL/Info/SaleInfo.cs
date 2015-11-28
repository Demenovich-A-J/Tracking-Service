using System;

namespace BL.Info
{
    public class SaleInfo
    {
        private readonly string _product;
        private readonly string _customer;
        private readonly DateTime _date;
        private readonly double _summ;

        public SaleInfo(string product, string customer, DateTime date, double summ)
        {
            _product = product;
            _customer = customer;
            _date = date;
            _summ = summ;
        }

        public string Product => _product;
        public string Customer => _customer;
        public DateTime Date => _date;
        public double Summ => _summ;
    }
}