using DAL.ManagerSalesModel;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;

namespace DAL.ReposytoryModel
{
    public class ProductRepository : GenericDataRepitory<Product, ProductSet>
    {
        protected override ProductSet ObjectToEntity(Product item)
        {
            throw new System.NotImplementedException();
        }

        protected override Product EntityToObject(ProductSet item)
        {
            throw new System.NotImplementedException();
        }
    }
}