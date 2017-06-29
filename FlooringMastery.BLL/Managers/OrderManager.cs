using FlooringMastery.Models;
using FlooringMastery.Models.Models.Responses;
using System;

namespace FlooringMastery.BLL.Managers
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public DisplaySingleOrderResponse DisplayOrder(DateTime orderDate, int orderNumber)
        {
            DisplaySingleOrderResponse response = new DisplaySingleOrderResponse();

            response.OrderDetails = _orderRepository.DisplayOrder(orderNumber, orderDate);

            if (response.OrderDetails == null)
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public DisplayAllOrdersResponse DisplayOrders(DateTime orderDate)
        {
            DisplayAllOrdersResponse response = new DisplayAllOrdersResponse()
            {
                OrderDetails = _orderRepository.DisplayOrders(orderDate)
            };

            if (response.OrderDetails == null)
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public void AddOrder(Order newOrder)
        {
            _orderRepository.AddOrder(newOrder);
        }

        public void EditOrder(Order editOrder)
        {
            _orderRepository.EditOrder(editOrder);
        }

        public void RemoveOrder(Order removeOrder)
        {
            _orderRepository.RemoveOrder(removeOrder);
        }
    }
}