using System.Data.Entity;
using System.Linq;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;
using Product = DAL.ManagerSalesModel.Product;
using Sale = DAL.ManagerSalesModel.Sale;

namespace DAL.ReposytoryModel
{
    public class SaleRepository : GenericDataRepitory<ManagerSalesModel.Sale, EntytiModel.Sale>
    {
        protected override EntytiModel.Sale ObjectToEntity(ManagerSalesModel.Sale item)
        {
            return new EntytiModel.Sale
            {
                Summ = item.Summ,
                Date = item.Date,
                CustomerId = item.CustomerId,
                ManagerId = item.ManagerId,
                ProductId = item.ProductId,
            };
        }

        protected override ManagerSalesModel.Sale EntityToObject(EntytiModel.Sale item)
        {
            return new ManagerSalesModel.Sale
            {
                Summ = item.Summ,
                Date = item.Date,
                CustomerId = item.CustomerId,
                ManagerId = item.ManagerId,
                ProductId = item.ProductId,
                Customer = new ManagerSalesModel.Customer(item.Customer.Name),
                Manager = new ManagerSalesModel.Manager(item.Manager.LastName),
                Product = new ManagerSalesModel.Product(item.Product.Name)
            };
        }

        public override Sale GetSingle(Sale item)
        {
            Sale resItem;
            using (var context = new ManagerSaleDBEntities())
            {
                resItem = context
                    .Set<EntytiModel.Sale>()
                    .Select(EntityToObject)
                    .FirstOrDefault(x => x.Manager == item.Manager);
            }
            return resItem;
        }
    }
}