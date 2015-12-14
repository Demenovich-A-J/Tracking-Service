using System.ServiceProcess;

namespace ManagerSale_Service_
{
    internal static class Program
    {
        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ManagerSaleService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}