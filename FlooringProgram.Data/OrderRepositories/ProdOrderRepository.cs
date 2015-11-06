using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data.OrderRepositories
{
    public class ProdOrderRepository : IOrderRepository
    {

        private const string _filePath = @"DataFiles\Orders_";
        private List<Order> orders = new List<Order>();

        public List<Order> GetOrderInformation(string date)
        {
            orders.Clear();
            string datedFilePath = _filePath + date + ".txt";

            if (File.Exists(datedFilePath))
            {

                var reader = File.ReadAllLines(datedFilePath);

                for (int i = 1; i < reader.Length; i++)
                {
                    string name = reader[i].Substring(reader[i].IndexOf("\"") + 1, (reader[i].LastIndexOf("\"")-reader[i].IndexOf("\""))-1);
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

                    orders.Add(order);
                }
            }
            return orders;
        }

        public void EditOrder(Order updatedOrder, string date)
        {
            orders = GetOrderInformation(date);

            string datedFilePath = _filePath + date + ".txt";

            File.Delete(datedFilePath);

            using (var writer = File.CreateText(datedFilePath))
            {
                writer.Write(
                    "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");


                foreach (var order in orders)
                {
                    if (order.OrderNumber != updatedOrder.OrderNumber)
                    {
                        writer.WriteLine();
                        writer.Write("{0},", order.OrderNumber);
                        writer.Write("{0},", "\"" + order.CustomerName + "\"");
                        writer.Write("{0},", order.State);
                        writer.Write("{0},", order.TaxRate);
                        writer.Write("{0},", order.ProductInfo.ProductType);
                        writer.Write("{0},", order.Area);
                        writer.Write("{0},", order.ProductInfo.CostPerSquareFoot);
                        writer.Write("{0},", order.ProductInfo.LaborCostPerSquareFoot);
                        writer.Write("{0},", order.MaterialCost);
                        writer.Write("{0},", order.LaborCost);
                        writer.Write("{0},", order.Tax);
                        writer.Write("{0}", order.Total);
                    }
                    else
                    {
                        writer.WriteLine();
                        writer.Write("{0},", updatedOrder.OrderNumber);
                        writer.Write("{0},", "\"" + updatedOrder.CustomerName + "\"");
                        writer.Write("{0},", updatedOrder.State);
                        writer.Write("{0},", updatedOrder.TaxRate);
                        writer.Write("{0},", updatedOrder.ProductInfo.ProductType);
                        writer.Write("{0},", updatedOrder.Area);
                        writer.Write("{0},", updatedOrder.ProductInfo.CostPerSquareFoot);
                        writer.Write("{0},", updatedOrder.ProductInfo.LaborCostPerSquareFoot);
                        writer.Write("{0},", updatedOrder.MaterialCost);
                        writer.Write("{0},", updatedOrder.LaborCost);
                        writer.Write("{0},", updatedOrder.Tax);
                        writer.Write("{0}", updatedOrder.Total);
                    }
                }
            }
        }

        public void RemoveOrder(string date, int orderNum)
        {

            string datedFilePath = _filePath + date + ".txt";

            File.Delete(datedFilePath);

            using (var writer = File.CreateText(datedFilePath))
            {
                writer.Write(
                    "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");

                foreach (var order in orders)
                {

                    if (order.OrderNumber != orderNum)
                    {
                        writer.WriteLine();
                        writer.Write("{0},", order.OrderNumber);
                        writer.Write("{0},", "\"" + order.CustomerName + "\"");
                        writer.Write("{0},", order.State);
                        writer.Write("{0},", order.TaxRate);
                        writer.Write("{0},", order.ProductInfo.ProductType);
                        writer.Write("{0},", order.Area);
                        writer.Write("{0},", order.ProductInfo.CostPerSquareFoot);
                        writer.Write("{0},", order.ProductInfo.LaborCostPerSquareFoot);
                        writer.Write("{0},", order.MaterialCost);
                        writer.Write("{0},", order.LaborCost);
                        writer.Write("{0},", order.Tax);
                        writer.Write("{0}", order.Total);
                    }
                }
            }


        }

        public void AddOrder(Order newOrder, string date)
        {

            string datedFilePath = _filePath + date + ".txt";

            if (!(File.Exists(datedFilePath)))
            {
                using (var writer = File.CreateText(datedFilePath))
                {
                    writer.Write(
                        "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                }
            }

            using (var writer = File.AppendText(datedFilePath))
            {
                writer.WriteLine();
                writer.Write("{0},", newOrder.OrderNumber);
                writer.Write("{0},", "\"" + newOrder.CustomerName + "\"");
                writer.Write("{0},", newOrder.State);
                writer.Write("{0},", newOrder.TaxRate);
                writer.Write("{0},", newOrder.ProductInfo.ProductType);
                writer.Write("{0},", newOrder.Area);
                writer.Write("{0},", newOrder.ProductInfo.CostPerSquareFoot);
                writer.Write("{0},", newOrder.ProductInfo.LaborCostPerSquareFoot);
                writer.Write("{0},", newOrder.MaterialCost);
                writer.Write("{0},", newOrder.LaborCost);
                writer.Write("{0},", newOrder.Tax);
                writer.Write("{0}", newOrder.Total);
            }
        }
    }
}

