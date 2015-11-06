using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Data.OrderRepositories;
using FlooringProgram.Models;

namespace FlooringProgram.BLL
{

    public class OrderOperations
    {
        private IOrderRepository _repo;

        public OrderOperations()
        {
            _repo = OrderRepositoryFactory.CreateOrderRepository();
        }

        public Response GetOrders(string date)
        {
            var response = new Response();

            var orders = _repo.GetOrderInformation(date);

            if (!orders.Any())
            {
                response.Success = false;
                response.Message = "There are no orders from this date.";
            }
            else
            {
                response.Success = true;
                response.OrderList = orders;
            }

            return response;
        }

        public Response GetSpecificOrder(string date, int orderNum)
        {
            Response response = new Response();
            response = GetOrders(date);
            
            if (response.Success == false)
            {
                return response;
            }
            else
            {
                var result = response.OrderList.Any(n => n.OrderNumber == orderNum);
                if (!result)
                {
                    response.Message = "Your order number cannot be found.";
                    response.Success = false;
                    return response;
                }
                else
                {
                    IEnumerable<Order> specificOrder = (from o in response.OrderList
                        where o.OrderNumber == orderNum
                        select o).ToList();
                    

                    response.OrderList.Clear();
                    foreach (var order in specificOrder)
                    {
                        response.OrderList.Add(order);
                    }
                    return response;
                }
            }
        }

        public Response EditOrder(Order newOrder, string date)
        {
            Response response = new Response();

                _repo.EditOrder(newOrder, date);
                response.Message = "Order has been updated.";
                return response;
        }

        public Response RemoveOrder(string date, int orderNum)
        {
            Response response = new Response();
            response = GetOrders(date);

            var result = response.OrderList.Any(n => n.OrderNumber == orderNum);

            if (response.Success == false)
            {
                return response;
            }
            else if (!result)
            {
                response.Message = "Your order number cannot be found.";
                return response;
            }
            else
            {
                _repo.RemoveOrder(date, orderNum);
                response.Message = "Order has been removed.";
                return response;
            }
        }

        public int CreateNewOrderNumber(string date)
        {
            Response response = GetOrders(date);

            if (response.Success == false)
            {
                return 1;
            }
            else
            {
                var result = response.OrderList.Max(o => o.OrderNumber);
                return result+1;
            }
        }

        public void AddOrder(Order newOrder, string date)
        {
           _repo.AddOrder(newOrder, date);
        }
    }
}
