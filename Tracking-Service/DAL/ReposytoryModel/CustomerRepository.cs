using DAL.ManagerSalesModel;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;

namespace DAL.ReposytoryModel
{
    public class CustomerRepository : GenericDataRepitory<Customer,CustomerSet>
    {
        protected override CustomerSet ObjectToEntity(Customer item)
        {
            throw new System.NotImplementedException();
        }

        protected override Customer EntityToObject(CustomerSet item)
        {
            throw new System.NotImplementedException();
        }
    }
}