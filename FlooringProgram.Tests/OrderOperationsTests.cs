using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Data.OrderRepositories;
using FlooringProgram.Models;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    public class OrderOperationsTests
    {
        [Test]
        public void GetSpecificOrderTest()
        {

            var ops = new OrderOperations();
            var response = ops.GetSpecificOrder("06012014", 1);
            Console.WriteLine(response.Message);
            Order order = response.OrderList.FirstOrDefault(o => o.OrderNumber == 1);

            Assert.AreEqual(1, order.OrderNumber);
            Assert.AreEqual("Jim", order.CustomerName);
        }

        [Test]
        public void CreateNewOrderNumberTest()
        {
            var ops = new OrderOperations();
            var newOrderNum = ops.CreateNewOrderNumber("06012014");

            Assert.AreEqual(newOrderNum, 4);
        }

        [Test]
        public void GetOrdersTest()
        {
            var ops = new OrderOperations();
            var response = ops.GetOrders("06012014");
            var orderNums = response.OrderList.Count();
            Assert.AreEqual(orderNums, 3);
        }

        [Test]
        public void RemoveOrderOpsTest()
        {
            var ops = new OrderOperations();
            ops.RemoveOrder("06012014", 2);
            ops.RemoveOrder("06012014", 1);
            var response = ops.GetOrders("06012014");
            var orderNums = response.OrderList.Count();


            Assert.AreEqual(orderNums, 1);
        }

        [Test]
        public void EditOrderOpsTest()
        {
            var ops = new OrderOperations();
            var response = ops.GetSpecificOrder("06012014", 1);

            var order = response.OrderList.FirstOrDefault(o => o.OrderNumber == 1);

            order.CustomerName = "Dave";

            ops.EditOrder(order, "06012014");
            response = ops.GetOrders("06012014");
            

            var editedOrder = response.OrderList.FirstOrDefault(o => o.OrderNumber == 1);


            Assert.AreEqual(editedOrder.CustomerName, "Dave");
        }

        [Test]
        public void AddOrderOpsTest()
        {
            var ops = new OrderOperations();
            Order newOrder = new Order();
            newOrder.ProductInfo = new Product();

            newOrder.OrderNumber = 4;
            newOrder.CustomerName = "Dave";
            newOrder.TaxRate = (decimal)6.25;
            newOrder.ProductInfo.ProductType = "Wood";
            newOrder.Area = (decimal)100.00;
            newOrder.ProductInfo.CostPerSquareFoot = (decimal)5.15;
            newOrder.ProductInfo.LaborCostPerSquareFoot = (decimal)4.75;
            newOrder.MaterialCost = (decimal)515.00;
            newOrder.LaborCost = (decimal)475.00;
            newOrder.Tax = (decimal)61.88;
            newOrder.Total = (decimal)1051.88;
            newOrder.State = "OH";

            ops.AddOrder(newOrder, "06012014");

            var response = ops.GetOrders("06012014");
            var orderNum = response.OrderList.Count();

            Assert.AreEqual(orderNum, 4);
        }
    }
}
