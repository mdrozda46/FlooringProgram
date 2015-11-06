using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace FlooringProgram.Data.OrderRepositories
{
    public class TestOrderRepository : IOrderRepository
    {

        private const string _filePath = @"DataFilesTest\Orders_";
        private List<Order> orders = new List<Order>();
        private Dictionary<string, Order> ordersDic = new Dictionary<string, Order>();

        public TestOrderRepository()
        {

        string datedFilePath = _filePath + "06012014" + ".txt";

            if (File.Exists(datedFilePath))
            {

                var reader = File.ReadAllLines(datedFilePath);

                for (int i = 1; i < reader.Length; i++)
                {
                    string name = reader[i].Substring(reader[i].IndexOf("\"") + 1, (reader[i].LastIndexOf("\"") - reader[i].IndexOf("\"")) - 1);
                    reader[i] = reader[i].Replace(name, " ");

                    var columns = reader[i].Split(',');

                    var order = new Order();
                    order.ProductInfo = new Product();

                    order.OrderNumber = int.Parse(columns[0]);
                    order.CustomerName = name;
                    order.State = columns[2];
                    order.TaxRate = decimal.Parse(columns[3]);
                    order.ProductInfo.ProductType = columns[4];
                    order.Area = decimal.Parse(columns[5]);
                    order.ProductInfo.CostPerSquareFoot = decimal.Parse(columns[6]);
                    order.ProductInfo.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    order.MaterialCost = decimal.Parse(columns[8]);
                    order.LaborCost = decimal.Parse(columns[9]);
                    order.Tax = decimal.Parse(columns[10]);
                    order.Total = decimal.Parse(columns[11]);

                    ordersDic.Add("06012014" + order.OrderNumber.ToString(), order);
                }
            }
        }

        public List<Order> GetOrderInformation(string date)
        {
            List<Order> orderList = new List<Order>();
            foreach (var order in ordersDic)
            {
                if (order.Key.Substring(0, 8) == date)
                {
                    orderList.Add(order.Value);
                }
            }
            return orderList;
        }

        public void EditOrder(Order updatedOrder, string date)
        {
            ordersDic.Remove(date + updatedOrder.OrderNumber.ToString());
            ordersDic.Add(date + updatedOrder.OrderNumber.ToString(), updatedOrder);
        }

        public void RemoveOrder(string date, int orderNum)
        {
            ordersDic.Remove(date + orderNum.ToString());
        }

        public void AddOrder(Order newOrder, string date)
        { 
            ordersDic.Add(date + newOrder.OrderNumber.ToString(), newOrder);
        }
    }
}



