using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data.ProductRepositories
{
    public class TestProductRepository : IProductRepository
    {

        private const string _filePath = @"DataFilesTest\Products.txt";
        private List<Product> _productList = new List<Product>();

        public List<Product> GetProductInformation()
        {

            if (File.Exists(_filePath))
            {

                var reader = File.ReadAllLines(_filePath);

                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');

                    var product = new Product();

                    product.ProductType = columns[0];
                    product.CostPerSquareFoot = Decimal.Parse(columns[1]);
                    product.LaborCostPerSquareFoot = Decimal.Parse(columns[2]);

                    _productList.Add(product);

                }
            }
            return _productList;
        }
    }
}