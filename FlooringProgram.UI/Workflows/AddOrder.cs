using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Workflows
{
    public class AddOrder
    {

        private Order _newOrder;
        private string _formattedDate;
        private OrderOperations _orderOps;

        public void Execute(OrderOperations orderOps)
        {
            _orderOps = orderOps;
            _newOrder = new Order();
            _newOrder.ProductInfo = new Product();
            GetCustomerNameFromUser();
            GetCustomerState();
            GetProductType();
            GetArea();
            BuildOrder();
            DisplayNewOrder();
        }


        public void GetCustomerNameFromUser()
        {
            string input = "";
            do
            {
                Console.Clear();
                Console.Write("Enter Customer Name: ");
                input = Console.ReadLine();
            } while (input == "");

            _newOrder.CustomerName = input;
        }

        public void GetCustomerState()
        {
            string input = "";

            var ops = new TaxOperations();
            var stateTaxInfo = ops.GetTaxInfo();

            var states = stateTaxInfo.Select(state => state.StateAbbreviation).ToList();

            do
            {
                Console.Clear();
                Console.WriteLine("Customer State Options");
                Console.WriteLine("----------------------");
                Console.WriteLine();
                foreach (var state in states)
                {
                    Console.Write("{0} ", state);
                }
                Console.WriteLine();
                Console.Write("\nEnter Customer State: ");

                input = Console.ReadLine().ToUpper();

                if (states.Contains(input))
                {
                    _newOrder.State = input;
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("That was not a valid state name.");
                Console.Write("\nPress enter to continue...");
                ErrorLogOperations.LogError(string.Format("Add Order: Invalid State entered: {0}", input));
                Console.ReadLine();
            } while (true);
        }

        public void GetProductType()
        {
            string input = "";

            var ops = new ProductOperations();
            var productInfo = ops.GetProductInfo();

            var products = productInfo.Select(product => product.ProductType.ToLower()).ToList();

            do
            {

                Console.Clear();
                Console.WriteLine("Product Type Options");
                Console.WriteLine("--------------------");
                Console.WriteLine();
                foreach (var product in products)
                {
                    Console.Write("{0} ", product.ToLower());
                }
                Console.WriteLine();
                Console.Write("\nEnter Product Type: ");
                input = Console.ReadLine().ToLower();

                if (products.Contains(input))
                {
                    _newOrder.ProductInfo.ProductType = input;
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("That was not a valid product type.");
                Console.Write("\nPress enter to continue...");
                ErrorLogOperations.LogError(string.Format("Add Order: Invalid Product Type entered: {0}", input));
                Console.ReadLine();
            } while (true);
        }

        public void GetArea()
        {
            string input = "";
            decimal area = 0;
            do
            {
                Console.Clear();
                Console.Write("Enter Area in Sq Ft: ");
                input = Console.ReadLine();

                if (Decimal.TryParse(input, out area) && area > 0)
                {
                    _newOrder.Area = area;
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("That was not a valid entry.");
                Console.Write("\nPress enter to continue...");
                ErrorLogOperations.LogError(string.Format("Add Order: Invalid Area entered: {0}", input));
                Console.ReadLine();

            } while (true);
        }

        public void BuildOrder()
        {
            var taxOps = new TaxOperations();
            var stateTaxInfo = taxOps.GetTaxInfo();

            var prodOps = new ProductOperations();
            var productInfo = prodOps.GetProductInfo();

            var result =
                productInfo.FirstOrDefault(p => p.ProductType.ToLower() == _newOrder.ProductInfo.ProductType.ToLower());
            var tax = stateTaxInfo.FirstOrDefault(p => p.StateAbbreviation.ToUpper() == _newOrder.State.ToUpper());

            _newOrder.ProductInfo.CostPerSquareFoot = result.CostPerSquareFoot;
            _newOrder.ProductInfo.LaborCostPerSquareFoot = result.LaborCostPerSquareFoot;
            _newOrder.TaxRate = tax.TaxRate;
            _newOrder.MaterialCost = _newOrder.Area*_newOrder.ProductInfo.CostPerSquareFoot;
            _newOrder.LaborCost = _newOrder.Area*_newOrder.ProductInfo.LaborCostPerSquareFoot;
            _newOrder.Tax = (_newOrder.MaterialCost + _newOrder.LaborCost)*(_newOrder.TaxRate/100);
            _newOrder.Total = _newOrder.MaterialCost + _newOrder.LaborCost + _newOrder.Tax;
            _newOrder.OrderNumber = NewOrderNumber();
        }

        public int NewOrderNumber()
        {
            DateTime date = new DateTime();
            date = DateTime.Now;
            _formattedDate = date.ToString("MMddyyyy");


            int newOrderNum = _orderOps.CreateNewOrderNumber(_formattedDate);

            return newOrderNum;
        }

        public void DisplayNewOrder()
        {
            string input;
            do
            {
                Console.Clear();
                PrintOrderInformation();
                Console.WriteLine();
                Console.Write("Are you sure - (Y)es or (N)o: ");
                input = Console.ReadLine().ToUpper();
            } while (!(input == "N" || input == "Y"));

            if (input == "Y")
            {
                SubmitOrder();
            }
        }

        public void PrintOrderInformation()
        {
            Console.Clear();
            Console.WriteLine("Order Number:\t{0}", _newOrder.OrderNumber);
            Console.WriteLine("Customer Name:\t{0}", _newOrder.CustomerName);
            Console.WriteLine("Customer State:\t{0}", _newOrder.State);
            Console.WriteLine("Tax Rate:\t{0:P}", _newOrder.TaxRate/100);
            Console.WriteLine("Product Type:\t{0}", _newOrder.ProductInfo.ProductType);
            Console.WriteLine("Area:\t\t{0:##,###} Sq Ft", _newOrder.Area);
            Console.WriteLine("Cost/SqFt:\t{0:C}", _newOrder.ProductInfo.CostPerSquareFoot);
            Console.WriteLine("Labor/SqFt:\t{0:C}", _newOrder.ProductInfo.LaborCostPerSquareFoot);
            Console.WriteLine("Material Cost:\t{0:C}", _newOrder.MaterialCost);
            Console.WriteLine("Labor Cost:\t{0:C}", _newOrder.LaborCost);
            Console.WriteLine("Tax:\t\t{0:C}", _newOrder.Tax);
            Console.WriteLine("Total:\t\t{0:C}", _newOrder.Total);
        }

        public void SubmitOrder()
        {

            _orderOps.AddOrder(_newOrder, _formattedDate);

            Console.Clear();
            Console.WriteLine("Congratulations! Your order was submitted");
            Console.WriteLine();
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }
    }
}
