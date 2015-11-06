using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Workflows
{
    public class EditOrder
    {
        private List<Order> _orders;
        private string _formattedDate;
        private int _orderNumber;
        private Order _customerOrder;
        private OrderOperations _orderOps;

        public void Execute(OrderOperations orderOps)
        {
            _orderOps = orderOps;
            _formattedDate = GetDateFromUser();
            GetOrderNumberFromUser();
            UpdateOrder();
            
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
                ErrorLogOperations.LogError(string.Format("Edit Order: Invalid Date entered: {0}", input));
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
                ErrorLogOperations.LogError(string.Format("Edit Order: Invalid Order Number entered: {0}", input));
                Console.ReadLine();

            } while (true);
        }

        public void UpdateOrder()
        {
            
            var response = _orderOps.GetSpecificOrder(_formattedDate, _orderNumber);

            if (response.Success)
            {
                _orders = response.OrderList;
                _customerOrder = _orders.FirstOrDefault();

                PrintOrderInformation(_orders);
                Console.Write("\nPress Enter to begin editing the order...");
                Console.ReadLine();
                
                PromptUserForName();
                PromptUserForState();
                PromptUserForPT();
                PromptUserForArea();
                BuildOrder();
                ConfirmEdit();

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
                Console.WriteLine("Tax Rate:\t{0:P}", order.TaxRate / 100);
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

        public void PromptUserForName()
        {
            
            Console.Clear();
            Console.Write("Enter Customer Name ({0}): ", _customerOrder.CustomerName);
            string newName = Console.ReadLine();

            if (newName != "")
            {
                _customerOrder.CustomerName = newName;
            }
        }

        public void PromptUserForPT()
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
                Console.Write("\nEnter Product Type ({0}): ", _customerOrder.ProductInfo.ProductType);
                string newPT = Console.ReadLine().ToLower();

                if (products.Contains(newPT) || newPT == "")
                {
                    if (newPT == "")
                    {
                        return;
                    }
                    else
                    {
                        _customerOrder.ProductInfo.ProductType = newPT;
                        return;
                    }
                }

                Console.WriteLine();
                Console.WriteLine("That was not a valid product type.");
                Console.Write("\nPress enter to continue...");
                ErrorLogOperations.LogError(string.Format("Edit Order: Invalid Product Type entered: {0}", input));
                Console.ReadLine();
            } while (true);
        }

        public void PromptUserForState()
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
                Console.Write("\nEnter State ({0}): ", _customerOrder.State);
                string newState = Console.ReadLine().ToUpper();

                if (states.Contains(newState) || newState == "")
                {
                    if (newState == "")
                    {
                        return;
                    }
                    else
                    {
                        _customerOrder.State = newState;
                        return;
                    }
                }

                Console.WriteLine();
                Console.WriteLine("That was not a valid state name.");
                Console.Write("\nPress enter to continue...");
                ErrorLogOperations.LogError(string.Format("Edit Order: Invalid State entered: {0}", input));
                Console.ReadLine();
            } while (true);
        }

   
        
        public void PromptUserForArea()
        {
            string input = "";
            decimal area;
            do
            {
                Console.Clear();
                Console.Write("Enter Area in Sq Ft ({0}): ", _customerOrder.Area);
                string newArea = Console.ReadLine();

                if ((Decimal.TryParse(newArea, out area) && area > 0 ) || newArea == "")
                {
                    if (newArea == "")
                    {
                        return;
                    }
                    else
                    {
                        _customerOrder.Area = area;
                        return;
                    }

                }

                Console.WriteLine();
                Console.WriteLine("That was not a valid entry.");
                Console.Write("\nPress enter to continue...");
                ErrorLogOperations.LogError(string.Format("Edit Order: Invalid Area entered: {0}", input));
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
                productInfo.FirstOrDefault(p => p.ProductType.ToLower() == _customerOrder.ProductInfo.ProductType.ToLower());
            var tax = stateTaxInfo.FirstOrDefault(p => p.StateAbbreviation.ToUpper() == _customerOrder.State.ToUpper());

            _customerOrder.ProductInfo.CostPerSquareFoot = result.CostPerSquareFoot;
            _customerOrder.ProductInfo.LaborCostPerSquareFoot = result.LaborCostPerSquareFoot;
            _customerOrder.TaxRate = tax.TaxRate;
            _customerOrder.MaterialCost = _customerOrder.Area * _customerOrder.ProductInfo.CostPerSquareFoot;
            _customerOrder.LaborCost = _customerOrder.Area * _customerOrder.ProductInfo.LaborCostPerSquareFoot;
            _customerOrder.Tax = (_customerOrder.MaterialCost + _customerOrder.LaborCost) * (_customerOrder.TaxRate/100);
            _customerOrder.Total = _customerOrder.MaterialCost + _customerOrder.LaborCost + _customerOrder.Tax;
        }

        public void ConfirmEdit()
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
                _orderOps.EditOrder(_customerOrder, _formattedDate);
            }
        }
    }
}