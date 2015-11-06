using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.OrderRepositories;
using FlooringProgram.Models;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    class OrderRepositoryTests
    {
        [Test]
        public void GetOrderInfoFromRepoTest()
        {
            var orderRepo = new TestOrderRepository();
            var orderList = orderRepo.GetOrderInformation("06012014");
            var numOrders = orderList.Count;

            var order = orderList.FirstOrDefault(o => o.CustomerName == "Jim");

            //Validating that all information is accurate
            Assert.AreEqual(order.OrderNumber, 1);
            Assert.AreEqual(order.TaxRate, 6.25);
            Assert.AreEqual(order.ProductInfo.ProductType, "Wood");
            Assert.AreEqual(order.Area, 100.00);
            Assert.AreEqual(order.ProductInfo.CostPerSquareFoot, 5.15);
            Assert.AreEqual(order.ProductInfo.LaborCostPerSquareFoot, 4.75);
            Assert.AreEqual(order.MaterialCost, 515.00);
            Assert.AreEqual(order.LaborCost, 475.00);
            Assert.AreEqual(order.Tax, 61.88);
            Assert.AreEqual(order.Total, 1051.88);
            Assert.AreEqual(order.State, "OH");

            //Validating total number of products is returned
            Assert.AreEqual(numOrders, 3);
        }

        [Test]
        public void EditOrderTest()
        {
            var orderRepo = new TestOrderRepository();
            var orderList = orderRepo.GetOrderInformation("06012014");
            
            var order = orderList.FirstOrDefault(o => o.OrderNumber == 1);

            order.CustomerName = "Mike";
            orderRepo.EditOrder(order, "06012014");

            var editedOrderList = orderRepo.GetOrderInformation("06012014");
            var editedOrder = editedOrderList.FirstOrDefault(o => o.OrderNumber == 1);
            var numOrders = editedOrderList.Count;

            //Validating edited order name change.
            Assert.AreEqual(editedOrder.CustomerName, "Mike");
           
            //Validating total number of products is returned
            Assert.AreEqual(numOrders, 3);
        }

        [Test]
        public void RemoveOrderTest()
        {
            var orderRepo = new TestOrderRepository();
            orderRepo.RemoveOrder("06012014", 1);

            var orderList = orderRepo.GetOrderInformation("06012014");
            var numOrders = orderList.Count();

            Assert.AreEqual(numOrders, 2);
        }

        [Test]
        public void AddOrderTest()
        {
            var orderRepo = new TestOrderRepository();
            Order newOrder = new Order();
            newOrder.ProductInfo = new Product();

            newOrder.OrderNumber = 4;
            newOrder.CustomerName = "Dave";
            newOrder.TaxRate = (decimal) 6.25;
            newOrder.ProductInfo.ProductType = "Wood";
            newOrder.Area = (decimal) 100.00;
            newOrder.ProductInfo.CostPerSquareFoot = (decimal) 5.15;
            newOrder.ProductInfo.LaborCostPerSquareFoot = (decimal) 4.75;
            newOrder.MaterialCost = (decimal) 515.00;
            newOrder.LaborCost = (decimal) 475.00;
            newOrder.Tax = (decimal) 61.88;
            newOrder.Total = (decimal) 1051.88;
            newOrder.State = "OH";

            orderRepo.AddOrder(newOrder, "06012014");

            var orderList = orderRepo.GetOrderInformation("06012014");
            var numOrders = orderList.Count();

            Assert.AreEqual(numOrders, 4);
        }

    }
}
