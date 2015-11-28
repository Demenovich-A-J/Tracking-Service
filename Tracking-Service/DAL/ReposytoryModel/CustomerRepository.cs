using DAL.ManagerSalesModel;
using DAL.ReposytoryModel.AbstractClasses;

namespace DAL.ReposytoryModel
{
    public class CustomerRepository : GenericDataRepitory<ManagerSalesModel.Customer, EntytiModel.Customer>
    {
        protected override EntytiModel.Customer ObjectToEntity(ManagerSalesModel.Customer item)
        {
            throw new System.NotImplementedException();
        }

        protected override ManagerSalesModel.Customer EntityToObject(EntytiModel.Customer item)
        {
            throw new System.NotImplementedException();
        }
    }
}