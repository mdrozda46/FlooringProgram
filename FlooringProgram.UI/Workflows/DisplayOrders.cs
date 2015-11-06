using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Workflows
{
    public class DisplayOrders
    {
        private List<Order> _orders;
        private string _formattedDate;
        private OrderOperations _orderOps;

        public void Execute(OrderOperations orderOps)
        {
            _orderOps = orderOps;
            _formattedDate = GetDateFromUser();
            DisplayOrder(_formattedDate);  
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
                ErrorLogOperations.LogError(string.Format("Display Orders: Invalid Date entered: {0}", input));
                Console.ReadLine();

            } while (true);
        }

        public void DisplayOrder(string date)
        {
            
            var response = _orderOps.GetOrders(date);

            if (response.Success)
            {
                _orders = response.OrderList;
                PrintOrderInformation(_orders);

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
            do
            {
                Console.Clear();
                Console.WriteLine("Orders from {0}/{1}/{2}", _formattedDate.Substring(0, 2), _formattedDate.Substring(2, 2), _formattedDate.Substring(4, 4));
                Console.WriteLine("-----------------------");
                foreach (var order in OrderList)
                {
                    Console.WriteLine("Order Number: {0}, Customer Name: {1}, Total: {2:C}", order.OrderNumber, order.CustomerName, order.Total);
                }
                Console.Write("\n\nPress enter order number or (M)ain menu: ");
                string input = Console.ReadLine();

                int inputOrderNum;
                int.TryParse(input, out inputOrderNum);

                if (input.ToUpper() == "M")
                {
                    return;
                }

                else if (OrderList.Any(o => o.OrderNumber == inputOrderNum))
                {
                    PrintOrderInformation(OrderList.FirstOrDefault(o => o.OrderNumber == inputOrderNum));
                    return;
                }
                else
                {
                    Console.Write("\nInvalid Order Number.");
                    Console.WriteLine();
                    Console.Write("\nPress Enter to continue...");
                    Console.ReadLine();
                }

            } while (true);
            
        }

        public void PrintOrderInformation(Order order)
        {
            Console.Clear();
            Console.WriteLine("Order Number:\t{0}", order.OrderNumber);
            Console.WriteLine("Customer Name:\t{0}", order.CustomerName);
            Console.WriteLine("Customer State:\t{0}", order.State);
            Console.WriteLine("Tax Rate:\t{0:P}", order.TaxRate / 100);
            Console.WriteLine("Product Type:\t{0}", order.ProductInfo.ProductType);
            Console.WriteLine("Area:\t\t{0:##,###} Sq Ft", order.Area);
            Console.WriteLine("Cost/SqFt:\t{0:C}", order.ProductInfo.CostPerSquareFoot);
            Console.WriteLine("Labor/SqFt:\t{0:C}", order.ProductInfo.LaborCostPerSquareFoot);
            Console.WriteLine("Material Cost:\t{0:C}", order.MaterialCost);
            Console.WriteLine("Labor Cost:\t{0:C}", order.LaborCost);
            Console.WriteLine("Tax:\t\t{0:C}", order.Tax);
            Console.WriteLine("Total:\t\t{0:C}", order.Total);

            Console.WriteLine();
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
