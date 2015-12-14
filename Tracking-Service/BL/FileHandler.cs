using System;
using System.IO;
using System.Linq;
using System.Transactions;
using BL.Info;
using BL.Parser.Interfaces;
using BL.Reader.Interfaces;
using DAL.DataBaseConfigurator;
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
        private readonly IGenericDataRepository<DocumentInfo> _documentInfoRepository; 

        public FileHandler(IParser parser, IReader reader)
        {
            _parser = parser;
            _reader = reader;
            _saleRepository = new SaleRepository();
            _managerRepository = new ManagerRepository();
            _customerRepository = new CustomerRepository();
            _productRepository = new ProductRepository();
            _documentInfoRepository = new DocumentInfoRepository();
            ConfigDataBase.Config();
        }

        public void ProcessFile(string folder, string filename)
        {
            var title = _parser.ParseTitle(filename);
            var manager = IfNotExistAdd(new Manager(title.LastName), _managerRepository);
            var documentInfo = _documentInfoRepository.GetSingle(x => x.DocumentName == filename);

            if (documentInfo != null && documentInfo.Status == DocumentInfoStatus.Processed.ToString())
            {
                var ex = new Exception($"File {filename} was already proccesed with status {documentInfo.Status}");
                ex.Data.Add("Status",documentInfo.Status);
                throw ex;
            }

            try
            {
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

                    _documentInfoRepository.Add(new DocumentInfo(filename, DocumentInfoStatus.Processed, title.Date)
                    {
                        ManagerId = manager.Id,
                        Manager = manager
                    });
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                _documentInfoRepository.Add(new DocumentInfo(filename, DocumentInfoStatus.Abrot, title.Date)
                {
                    ManagerId = manager.Id,
                    Manager = manager
                });
                ex.Data.Add("info", $"\nFile {filename} was abrot with status {DocumentInfoStatus.Abrot}");
                throw;
            }
        }

        private static T IfNotExistAdd<T>(T item, IGenericDataRepository<T> repository) where T : class
        {
            T resulIitem;

            lock (repository)
            {
                resulIitem = repository.GetSingle(item);
            }

            if (resulIitem != null) return resulIitem;

            lock (repository)
            {
                repository.Add(item);

                return repository.GetSingle(item);
            }
        }
    }
}