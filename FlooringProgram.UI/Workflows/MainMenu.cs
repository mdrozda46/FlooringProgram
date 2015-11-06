using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;

namespace FlooringProgram.UI.Workflows
{
    public class MainMenu
    {
        private OrderOperations _orderOps = new OrderOperations();

        public void Execute()
        {
            string input = "";
            int inputNum = 0;

            do
            {
                Console.Clear();
                Console.WriteLine(" WELCOME TO MIKE AND PATRICK'S FLOORING STORE!");
                Console.WriteLine(" ----------------------------------------------");
                Console.WriteLine(" 1. Display Orders");
                Console.WriteLine(" 2. Add Order");
                Console.WriteLine(" 3. Edit Order");
                Console.WriteLine(" 4. Remove an Order");
                Console.WriteLine(" 5. Quit");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("\tEnter Choice: ");

                input = Console.ReadLine();

                int.TryParse(input, out inputNum);

                if (inputNum >= 1 && inputNum <= 4)
                {
                    ProcessChoice(inputNum);
                }
                
                else if ( inputNum == 5)
                {
                    Console.WriteLine("Closing program...");
                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry!");
                    Console.WriteLine("\nPress enter to continue...");
                    Console.ReadLine();
                }

            } while (inputNum != 5);
        }

        private void ProcessChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    DisplayOrders dowf = new DisplayOrders();
                    dowf.Execute(_orderOps);
                    break;
                case 2:
                    AddOrder aowf = new AddOrder();
                    aowf.Execute(_orderOps);
                    break;
                case 3:
                    EditOrder eowf = new EditOrder();
                    eowf.Execute(_orderOps);
                    break;
                case 4:
                    RemoveOrder rowf = new RemoveOrder();
                    rowf.Execute(_orderOps);
                    break;
                default:
                    break;
            }
        }
    }
}
