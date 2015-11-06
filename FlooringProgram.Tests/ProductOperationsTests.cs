using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class ProductOperationsTests
    {
        [Test]
        public void GetProductInfoTest()
        {

            var ops = new ProductOperations();
            var products = ops.GetProductInfo();
            var product = products.FirstOrDefault();
            var numProducts = products.Count();

            //Validating that all information is accurate
            Assert.AreEqual(product.ProductType, "rock");
            Assert.AreEqual(product.CostPerSquareFoot, 5.00);
            Assert.AreEqual(product.LaborCostPerSquareFoot, 2.50);

            //Validating total number of products is returned
            Assert.AreEqual(numProducts, 3);
        }
    }
}
