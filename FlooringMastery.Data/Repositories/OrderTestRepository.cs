using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FlooringMastery.Data.Repositories
{
    public class OrderTestRepository : IOrderRepository
    {
        private string _dirPath = "";

        private List<Order> _orderList = new List<Order>();

        public OrderTestRepository(string dirPath)
        {
            _dirPath = dirPath;

            string[] orderFiles = Directory.GetFiles(dirPath);

            foreach (string orderFile in orderFiles)
            {
                DateTime fileDate = FileToDate(orderFile);

                using (StreamReader sr = new StreamReader(orderFile))
                {
                    string line;
                    sr.ReadLine();

                    while ((line = sr.ReadLine()) != null)
                    {
                        Order orderSelect = new Order();
                        string[] columns = line.Split(',');

                        orderSelect.OrderDate = fileDate;
                        orderSelect.OrderNumber = int.Parse(columns[0]);
                        orderSelect.CustomerName = columns[1];
                        orderSelect.State = (columns[2]);
                        orderSelect.TaxRate = decimal.Parse(columns[3]);
                        orderSelect.ProductType = columns[4];
                        orderSelect.Area = decimal.Parse(columns[5]);
                        orderSelect.CostPerSquareFoot = decimal.Parse(columns[6]);
                        orderSelect.LaborCostPerSquareFoot = decimal.Parse(columns[7]);

                        _orderList.Add(orderSelect);
                    }
                }
            }
        }

        private DateTime FileToDate(string orderFile)
        {
            DateTime toReturn;
            FileInfo dateFinder = new FileInfo(orderFile);
            string datePart = dateFinder.Name.Substring(7, 8);
            toReturn = DateTime.ParseExact(datePart, "MMddyyyy", CultureInfo.CurrentCulture);
            return toReturn;
        }

        public Order DisplayOrder(int orderNumber, DateTime orderDate)
        {
            Order toReturn = _orderList.SingleOrDefault(o => o.OrderNumber == orderNumber && o.OrderDate == orderDate);
            return toReturn;
        }

        public List<Order> DisplayOrders(DateTime orderDate)
        {
            List<Order> toReturn = _orderList.Where(o => o.OrderDate == orderDate).ToList();
            return toReturn;
        }

        public void AddOrder(Order AddOrder)
        {
            List<Order> orders = DisplayOrders(AddOrder.OrderDate);
            string filePath = FilePathFromDate(AddOrder.OrderDate);

            if (orders == null)
            {
                orders = new List<Order>();
            }

            int newOrderNumber = 1;

            if (orders.Count() > 0)
            {
                int currentOrderNumber = orders.Max(o => o.OrderNumber);
                newOrderNumber = currentOrderNumber + 1;
            }

            AddOrder.OrderNumber = newOrderNumber;
            orders.Add(AddOrder);

            WriteOrderList(orders, AddOrder.OrderDate);
            return;
        }

        private string FilePathFromDate(DateTime orderDate)
        {
            return Path.Combine(_dirPath, "Orders_" + orderDate.ToString("MMddyyyy") + ".txt");
        }

        public void EditOrder(Order EditOrder)
        {
            RemoveOrder(EditOrder);

            List<Order> orders = DisplayOrders(EditOrder.OrderDate);
            string filePath = FilePathFromDate(EditOrder.OrderDate);

            if (orders == null)
            {
                orders = new List<Order>();
            }

            orders.Add(EditOrder);
            WriteOrderList(orders, EditOrder.OrderDate);
            return;
        }

        public void RemoveOrder(Order possiblyNotInList)
        {
            Order orderInList = DisplayOrder(possiblyNotInList.OrderNumber, possiblyNotInList.OrderDate);
            _orderList.Remove(orderInList);

            List<Order> updatedList = DisplayOrders(possiblyNotInList.OrderDate);
            WriteOrderList(updatedList, possiblyNotInList.OrderDate);
        }

        private void WriteOrderList(List<Order> updatedList, DateTime orderDate)
        {
            List<string> orderLines = new List<string>();

            string filePath = FilePathFromDate(orderDate);

            //if (updatedList.Count == 1)
            //{
            //    string header = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
            //    orderLines.Add(header);
            //}

            foreach (var order in updatedList)
            {
                orderLines.Add(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                                order.OrderNumber,
                                order.CustomerName,
                                order.State,
                                order.TaxRate,
                                order.ProductType,
                                order.Area,
                                order.CostPerSquareFoot,
                                order.LaborCostPerSquareFoot,
                                order.MaterialCost,
                                order.LaborCost,
                                order.Tax.ToString("00.00"),
                                order.Total.ToString("00.00")));
            }
            File.WriteAllLines(filePath, orderLines);
        }
    }
}