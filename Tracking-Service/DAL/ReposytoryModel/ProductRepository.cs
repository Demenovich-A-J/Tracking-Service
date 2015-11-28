using DAL.ReposytoryModel.AbstractClasses;

namespace DAL.ReposytoryModel
{
    public class ProductRepository : GenericDataRepitory<ManagerSalesModel.Product, EntytiModel.Product>
    {
        protected override EntytiModel.Product ObjectToEntity(ManagerSalesModel.Product item)
        {
            throw new System.NotImplementedException();
        }

        protected override ManagerSalesModel.Product EntityToObject(EntytiModel.Product item)
        {
            throw new System.NotImplementedException();
        }
    }
}