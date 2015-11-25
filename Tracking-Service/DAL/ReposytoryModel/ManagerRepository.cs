using DAL.ManagerSalesModel;
using DAL.ReposytoryModel.AbstractClasses;
using EntytiModel;

namespace DAL.ReposytoryModel
{
    public class ManagerRepository : GenericDataRepitory<Manager,ManagerSet>
    {
        protected override ManagerSet ObjectToEntity(Manager item)
        {
            throw new System.NotImplementedException();
        }

        protected override Manager EntityToObject(ManagerSet item)
        {
            throw new System.NotImplementedException();
        }
    }
}