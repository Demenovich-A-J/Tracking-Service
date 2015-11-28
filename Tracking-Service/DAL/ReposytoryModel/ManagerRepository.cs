using DAL.ReposytoryModel.AbstractClasses;

namespace DAL.ReposytoryModel
{
    public class ManagerRepository : GenericDataRepitory<ManagerSalesModel.Manager,EntytiModel.Manager>
    {
        protected override EntytiModel.Manager ObjectToEntity(ManagerSalesModel.Manager item)
        {
            return new EntytiModel.Manager
            {
                Name = item.Name
            };
        }

        protected override ManagerSalesModel.Manager EntityToObject(EntytiModel.Manager item)
        {
            return new ManagerSalesModel.Manager(item.Name);
        }
    }
}