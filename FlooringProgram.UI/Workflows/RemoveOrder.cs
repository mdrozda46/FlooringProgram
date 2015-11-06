using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Workflows
{
    public class RemoveOrder
    {
        private List<Order> _orders;
        private string _formattedDate;
        private int _orderNumber;
        private OrderOperations _orderOps;

        public void Execute(OrderOperations orderOps)
        {
            _orderOps = orderOps;
            _formattedDate = GetDateFromUser();
            GetOrderNumberFromUser();
            DisplayOrder();
            
        }

        public string GetDateFromUser()
        {
            do
            {
                Console.Clear();
                Console.Write("Enter a date (MM/DD/YYYY): ");
                string input = Console.ReadLine();

                DateTime date = new DateTime();

                if (DateTime.TryParse(input, out date))
                {
                    return date.ToString("MMddyyyy");
                }

                Console.WriteLine();
                Console.WriteLine("That was not a valid date.");
                Console.Write("\nPress enter to continue...");
                ErrorLogOperations.LogError(string.Format("Remove Order: Invalid Date entered: {0}", input));
                Console.ReadLine();

            } while (true);
        }

        public void GetOrders(string date)
        {
            
            var response = _orderOps.GetOrders(date);

            if (response.Success)
            {
                _orders = response.OrderList;
                
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(response.Message);
                Console.Write("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        public void GetOrderNumberFromUser()
        {
            do
            {
                Console.Clear();
                Console.Write("Enter an Order Number: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out _orderNumber))
                {
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("That was not a valid order number.");
                Console.Write("\nPress enter to continue...");
                ErrorLogOperations.LogError(string.Format("Remove Order: Invalid Order Number entered: {0}", input));
                Console.ReadLine();

            } while (true);
        }

        public void DisplayOrder()
        {

            
            var response = _orderOps.GetSpecificOrder(_formattedDate, _orderNumber);

            if (response.Success)
            {
                _orders = response.OrderList;
                ConfirmRemove(_formattedDate, _orderNumber);

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(response.Message);
                Console.Write("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        public void PrintOrderInformation(List<Order> OrderList)
        {
            Console.Clear();
            foreach (var order in OrderList)
            {
                Console.WriteLine("Order Number:\t{0}", order.OrderNumber);
                Console.WriteLine("Customer Name:\t{0}", order.CustomerName);
                Console.WriteLine("Customer State:\t{0}", order.State);
                Console.WriteLine("Tax Rate:\t{0:P}", order.TaxRate/100);
                Console.WriteLine("Product Type:\t{0}", order.ProductInfo.ProductType);
                Console.WriteLine("Area:\t\t{0:##,###} Sq Ft", order.Area);
                Console.WriteLine("Cost/SqFt:\t{0:C}", order.ProductInfo.CostPerSquareFoot);
                Console.WriteLine("Labor/SqFt:\t{0:C}", order.ProductInfo.LaborCostPerSquareFoot);
                Console.WriteLine("Material Cost:\t{0:C}", order.MaterialCost);
                Console.WriteLine("Labor Cost:\t{0:C}", order.LaborCost);
                Console.WriteLine("Tax:\t\t{0:C}", order.Tax);
                Console.WriteLine("Total:\t\t{0:C}", order.Total);
            }
            
        }

        public void ConfirmRemove(string date, int orderNum)
        {
            string input;
            do
            {
                Console.Clear();
                PrintOrderInformation(_orders);
                Console.WriteLine();
                Console.Write("Are you sure - (Y)es or (N)o: ");
                input = Console.ReadLine().ToUpper();
            } while (!(input == "N" || input == "Y"));

            var mode = ConfigurationManager.AppSettings["mode"].ToString().ToUpper();

            if (input == "Y")
            {
                _orderOps.RemoveOrder(date, orderNum);
            }       
        }
    }
}
