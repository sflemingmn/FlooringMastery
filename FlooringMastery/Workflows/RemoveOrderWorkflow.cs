using FlooringMastery.BLL.Factories;
using FlooringMastery.BLL.Managers;
using FlooringMastery.Models.Models.Responses;
using System;

namespace FlooringMastery.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(ConsoleIO.SeperatorBar);
            Console.WriteLine(" REMOVE ORDER MENU ");
            Console.WriteLine(ConsoleIO.SeperatorBar);
            Console.WriteLine();

            DateTime validatedDateTime = new DateTime();
            bool validDate = false;

            while (!validDate)
            {
                Console.Write(" Enter Order Date In MM/DD/YYYY Format: ");
                string date = Console.ReadLine();

                if (!string.IsNullOrEmpty(date))
                {
                    if (DateTime.TryParse(date, out validatedDateTime))
                    {
                        validDate = true;
                    }
                    else
                    {
                        Console.WriteLine(" Please enter a VALID date format. ");
                    }
                }
                else
                {
                    Console.WriteLine(" Please enter a date. ");
                }
            }

            bool validOrderNumber = false;
            int validatedOrderNumber = 0;

            while (!validOrderNumber)
            {
                Console.Write(" Enter a Order Number: ");
                string orderNumber = Console.ReadLine();

                if (!string.IsNullOrEmpty(orderNumber))
                {
                    if (int.TryParse(orderNumber, out validatedOrderNumber))
                    {
                        validOrderNumber = true;
                    }
                    else
                    {
                        Console.WriteLine(" Please enter a VALID order number. ");
                    }
                }
                else
                {
                    Console.WriteLine(" Please enter an order number. ");
                }
            }

            DisplaySingleOrderResponse response = manager.DisplayOrder(validatedDateTime, validatedOrderNumber);

            if (response.Success)
            {
                Console.WriteLine();
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(" ORDER FOUND - DETAILS BELOW ");
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine();
                Console.WriteLine($" Order Number: {response.OrderDetails.OrderNumber.ToString()} | Order Date: {validatedDateTime.ToString("MM/dd/yyyy")} ");
                Console.WriteLine($" Customer Name: {response.OrderDetails.CustomerName} ");
                Console.WriteLine($" State: {response.OrderDetails.State} ");
                Console.WriteLine($" Product: {response.OrderDetails.ProductType} ");
                Console.WriteLine($" Materials: {response.OrderDetails.MaterialCost:c} ");
                Console.WriteLine($" Labor: {response.OrderDetails.LaborCost:c} ");
                Console.WriteLine($" Tax: {response.OrderDetails.Tax:c} ");
                Console.WriteLine($" Total: {response.OrderDetails.Total:c} ");

                ConsoleIO.ConfirmOrCancelFooter();

                var input = ConsoleIO.YesNoValidation();

                if (input == "Y")
                {
                    manager.RemoveOrder(response.OrderDetails);
                }
            }
            else
            {
                Console.WriteLine(ConsoleIO.ErrorMessage);
                Console.WriteLine(response.Message);
                Console.ReadKey();
            }
        }
    }
}