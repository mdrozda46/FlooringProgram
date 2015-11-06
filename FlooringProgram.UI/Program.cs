using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using FlooringProgram.UI.Workflows;

namespace FlooringProgram.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            var menu = new MainMenu();
            menu.Execute();

        }
    }
}
