using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public interface IOrderRepository
    {
        List<Order> GetOrderInformation(string date);
        void RemoveOrder(string date, int orderNum);
        void EditOrder(Order updatedOrder, string date);
        void AddOrder(Order newOrder, string date);
    }
}
