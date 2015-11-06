using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.ProductRepositories;
using NUnit.Framework;

namespace FlooringProgram.Tests
{   
    [TestFixture]
    public class ProductRepositoryTests
    {
        [Test]
        public void GetProductInfoFromRepoTest()
        {
            var productRepo = new TestProductRepository();
            var productList = productRepo.GetProductInformation();
            var numProducts = productList.Count;

            var product = productList.FirstOrDefault(p => p.ProductType == "rock");

            //Validating that all information is accurate
            Assert.AreEqual(product.CostPerSquareFoot, 5.00);
            Assert.AreEqual(product.LaborCostPerSquareFoot, 2.50);

            //Validating total number of products is returned
            Assert.AreEqual(numProducts, 3);
        }
    }
}
