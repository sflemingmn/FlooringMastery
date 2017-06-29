using FlooringMastery.BLL.Factories;
using FlooringMastery.BLL.Managers;
using FlooringMastery.Models;
using System;
using System.Globalization;

namespace FlooringMastery.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            Console.WriteLine();
            Console.WriteLine(ConsoleIO.SeperatorBar);
            Console.WriteLine(" ADD ORDER MENU ");
            Console.WriteLine(ConsoleIO.SeperatorBar);
            Console.WriteLine();

            Order newOrder = new Order();
            newOrder.OrderDate = OrderDate();
            OrderCustomerName(newOrder);
            OrderState(newOrder);
            OrderProductType(newOrder);
            OrderArea(newOrder);
            ConfirmNewOrder(newOrder, manager);
        }

        public DateTime OrderDate()
        {
            bool validDate = false;

            DateTime validatedDate = new DateTime();

            while (!validDate)
            {
                Console.Write(" Enter Order Date In MM/DD/YYYY Format: ");

                string validDateFormat = (Console.ReadLine());

                if (!string.IsNullOrEmpty(validDateFormat))
                {
                    if (DateTime.TryParse(validDateFormat, out validatedDate))
                    {
                        validDate = true;
                    }
                    else
                    {
                        Console.WriteLine(" Please enter a valid date in MM/DD/YYYY format. ");
                    }
                }
                else
                {
                    validDate = true;
                }
            }
            return validatedDate;
        }

        public void OrderCustomerName(Order order)
        {
            Console.Write(" Customer Name: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                order.CustomerName = newName;
            }
        }

        public void OrderState(Order order)
        {
            bool validState = false;
            while (!validState)
            {
                Console.Write(" Enter State: ");
                string newState = (Console.ReadLine());
                if (!string.IsNullOrEmpty(newState))
                {
                    newState = newState.ToUpper();
                    TaxManager taxManager = TaxManagerFactory.Create();
                    Tax tax = taxManager.TaxByState(newState);
                    if (tax != null)
                    {
                        validState = true;
                        order.State = newState;
                        order.TaxRate = tax.TaxRate;
                    }
                    else
                    {
                        Console.WriteLine(" Please enter a VALID state. ");
                    }
                }
                else
                {
                    validState = true;
                }
            }
        }

        public void OrderProductType(Order order)
        {
            bool validProduct = false;
            while (!validProduct)
            {
                Console.Write(" Product Choice: ");
                string newProductType = (Console.ReadLine());
                if (!string.IsNullOrEmpty(newProductType))
                {
                    ProductManager productManager = ProductManagerFactory.Create();
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    newProductType = textInfo.ToTitleCase(newProductType);
                    var product = productManager.ProductType(newProductType);
                    if (product != null)
                    {
                        validProduct = true;
                        order.ProductType = newProductType;
                        order.CostPerSquareFoot = product.CostPerSquareFoot;
                        order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                    }
                    else
                    {
                        Console.WriteLine(" Please enter a VALID product. ");
                    }
                }
                else
                {
                    validProduct = true;
                }
            }
        }

        public void OrderArea(Order order)
        {
            bool validArea = false;
            while (!validArea)
            {
                Console.Write(" Area: ");
                string newArea = (Console.ReadLine());
                if (!string.IsNullOrEmpty(newArea))
                {
                    decimal area = decimal.Parse(newArea);
                    if (area >= 100)
                    {
                        validArea = true;
                        order.Area = area;
                    }
                    else
                    {
                        Console.WriteLine(" Please enter an area that meets the minimum of 100 SQ FT. ");
                    }
                }
                else
                {
                    validArea = true;
                }
            }
        }

        public void ConfirmNewOrder(Order order, OrderManager manager)
        {
            ConsoleIO.ConfirmOrCancelFooter();
            var input = ConsoleIO.YesNoValidation();
            if (input == "Y" || input == "y")
            {
                manager.AddOrder(order);
            }
        }
    }
}