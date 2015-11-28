using System.Data.Entity;
using System.Linq;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;

namespace DAL.ReposytoryModel
{
    public class SaleRepository : GenericDataRepitory<ManagerSalesModel.Sale, EntytiModel.Sale>
    {
        public override void Add(ManagerSalesModel.Sale item)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                var manager = context.ManagerSet.FirstOrDefault(x => x.Name == item.Manager.Name);
                var product = context.ProductSet.AsNoTracking().FirstOrDefault(x => x.Name == item.Product.Name);
                var customer = context.CustomerSet.AsNoTracking().FirstOrDefault(x => x.Name == item.Customer.Name);
                var newSale = new EntytiModel.Sale
                {
                    Summ = item.Summ,
                    Date = item.Date,
                };

                if (manager == null)
                {
                    newSale.ManagerSet = new EntytiModel.Manager { Name = item.Manager.Name};
                }
                else
                {
                    newSale.ManagerId = manager.Id;
                }
                if (product == null)
                {
                    newSale.ProductSet = new EntytiModel.Product { Name = item.Product.Name };
                }
                else
                {
                    newSale.ProductId = product.Id;
                }
                if (customer == null)
                {
                    newSale.CustomerSet = new EntytiModel.Customer { Name = item.Customer.Name };
                }
                else
                {
                    newSale.CustomerId = customer.Id;
                }

                context.Entry(newSale).State = EntityState.Added;
                context.SaveChanges();
            }
            base.Add(item);
        }

        protected override EntytiModel.Sale ObjectToEntity(ManagerSalesModel.Sale item)
        {
            return new EntytiModel.Sale
            {
                Summ = item.Summ,
                Date = item.Date,
                CustomerSet = new EntytiModel.Customer { Name = item.Customer.Name },
                ManagerSet = new EntytiModel.Manager{ Name = item.Customer.Name },
                ProductSet = new EntytiModel.Product{ Name = item.Product.Name }
            };
        }

        protected override ManagerSalesModel.Sale EntityToObject(EntytiModel.Sale item)
        {
            return new ManagerSalesModel.Sale
            {
                Summ = item.Summ,
                Date = item.Date,
                Customer = new ManagerSalesModel.Customer(item.CustomerSet.Name),
                Manager = new ManagerSalesModel.Manager(item.ManagerSet.Name),
                Product = new ManagerSalesModel.Product(item.ProductSet.Name)
            };
        }
    }
}