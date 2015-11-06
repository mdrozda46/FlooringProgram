using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.OrderRepositories;
using FlooringProgram.Models;

namespace FlooringProgram.Data.ProductRepositories
{
    public class ProductRepositoryFactory
    {
        public static IProductRepository CreateProductRepository()
        {
            var mode = ConfigurationManager.AppSettings["mode"].ToString().ToUpper();

            switch (mode)
            {
                case "TEST":
                    return new TestProductRepository();
                    
                default:
                    return new ProdProductRepository();
                    
            }
        }
    }
}