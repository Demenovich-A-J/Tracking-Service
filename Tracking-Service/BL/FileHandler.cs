using System.IO;
using System.Linq;
using System.Transactions;
using DAL.ManagerSalesModel;
using DAL.ReposytoryModel;
using DAL.ReposytoryModel.Interfaces;

namespace BL
{
    public class FileHandler
    {
        private readonly IGenericDataRepository<Customer> _customerRepository;
        private readonly IGenericDataRepository<Manager> _managerRepository;
        private readonly IParser _parser;
        private readonly IGenericDataRepository<Product> _productRepository;
        private readonly IReader _reader;
        private readonly IGenericDataRepository<Sale> _saleRepository;

        public FileHandler(IParser parser, IReader reader)
        {
            _parser = parser;
            _reader = reader;
            _saleRepository = new SaleRepository();
            _managerRepository = new ManagerRepository();
            _customerRepository = new CustomerRepository();
            _productRepository = new ProductRepository();
        }

        public void ProcessFile(string folder, string filename)
        {
            var title = _parser.ParseTitle(filename);
            var manager = IfNotExistAdd(new Manager(title.LastName), _managerRepository);

            using (var transaction = new TransactionScope())
            {
                foreach (var sale in from line in _reader.Read(Path.Combine(new[] {folder, filename}))
                    select _parser.Parse(line)
                    into saleInfo
                    let customer = IfNotExistAdd(new Customer(saleInfo.Customer), _customerRepository)
                    let product = IfNotExistAdd(new Product(saleInfo.Product), _productRepository)
                    select new Sale
                    {
                        CustomerId = customer.Id,
                        ManagerId = manager.Id,
                        ProductId = product.Id,
                        Date = saleInfo.Date,
                        Summ = saleInfo.Summ
                    })
                {
                    _saleRepository.Add(sale);
                }

                transaction.Complete();
            }
        }

        private static T IfNotExistAdd<T>(T item, IGenericDataRepository<T> repository) where T : class
        {
            T resulIitem;

            lock (repository)
            {
                resulIitem = repository.GetSingle(x => x == item);
            }

            if (resulIitem != null) return resulIitem;

            lock (repository)
            {
                repository.Add(item);
            }

            return item;
        }
    }
}