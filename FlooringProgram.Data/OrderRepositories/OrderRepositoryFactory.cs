using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data.OrderRepositories
{
    public static class OrderRepositoryFactory
    {
        public static IOrderRepository CreateOrderRepository()
        {
            var mode = ConfigurationManager.AppSettings["mode"].ToString().ToUpper();

            switch (mode)
            {
                case "TEST":
                    return new TestOrderRepository();
                    
                default:
                    return new ProdOrderRepository();
                    
            }
        }
    }
}
