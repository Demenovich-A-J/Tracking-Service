using DAL.ManagerSalesModel;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;

namespace DAL.ReposytoryModel
{
    public class SaleRepository : GenericDataRepitory<Sale,SaleSet>
    {
        protected override SaleSet ObjectToEntity(Sale item)
        {
            return new SaleSet
            {
                Summ = item.Summ,
                Date = item.Date,
                CustomerSet = new CustomerSet { Name = item.Customer.Name },
                ManagerSet = new ManagerSet { Name = item.Customer.Name },
                ProductSet = new ProductSet { Name = item.Product.Name }
            };
        }

        protected override Sale EntityToObject(SaleSet item)
        {
            return new Sale
            {
                Summ = item.Summ,
                Date = item.Date,
                Customer = new Customer(item.CustomerSet.Name),
                Manager = new Manager(item.ManagerSet.Name),
                Product = new Product(item.ProductSet.Name)
            };
        }
    }
}