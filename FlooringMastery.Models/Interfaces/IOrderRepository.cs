using System;
using System.Collections.Generic;

namespace FlooringMastery.Models
{
    public interface IOrderRepository
    {
        Order DisplayOrder(int orderNumber, DateTime orderDate);

        List<Order> DisplayOrders(DateTime orderDate);

        void AddOrder(Order addOrder);

        void EditOrder(Order editOrder);

        void RemoveOrder(Order removeOrder);
    }
}