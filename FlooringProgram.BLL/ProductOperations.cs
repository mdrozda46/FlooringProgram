using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.ProductRepositories;
using FlooringProgram.Models;

namespace FlooringProgram.BLL
{
    public class ProductOperations
    {
        private IProductRepository _productRepo;

        public List<Product> GetProductInfo()
        {
            _productRepo = ProductRepositoryFactory.CreateProductRepository();
            return _productRepo.GetProductInformation();
        }

    }
}
