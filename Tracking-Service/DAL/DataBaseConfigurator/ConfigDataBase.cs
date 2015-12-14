using EntytiModel;

namespace DAL.DataBaseConfigurator
{
    public static class ConfigDataBase
    {
        public static void Config()
        {
            using (var context = new ManagerSaleDBEntities())
            {
                context.Database.CreateIfNotExists();
            }
        }
    }
}