using FlooringMastery.BLL.Factories;
using FlooringMastery.BLL.Managers;
using FlooringMastery.Models.Models.Responses;
using System;

namespace FlooringMastery.Workflows
{
    public class DisplayAllOrdersWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            Console.WriteLine();
            Console.WriteLine(ConsoleIO.SeperatorBar);
            Console.WriteLine(" DISPLAY ORDER MENU ");
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

            DisplayAllOrdersResponse response = manager.DisplayOrders(validatedDateTime);

            if (response.Success)
            {
                Console.WriteLine();
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(" BELOW IS A COMPLETE LIST OF ORDERS FOR DATE PROVIDED ");
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(ConsoleIO.OrderListFormat, " Date", "#", "Name", "State", "Area", "Total");
                Console.WriteLine(ConsoleIO.SeperatorBar);

                foreach (var order in response.OrderDetails)
                {
                    Console.WriteLine($"{order.OrderDate.ToString(" MM/dd/yyyy"),-14} {order.OrderNumber.ToString(),-7}{order.CustomerName,-11} {order.State,-9} {order.Area.ToString("0"),-12} {order.Total.ToString("c"),-10}");
                }
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(" END OF LIST ");
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(ConsoleIO.ReturnToMainMenu);
                Console.WriteLine(ConsoleIO.SeperatorBar);
            }
            else
            {
                Console.WriteLine(ConsoleIO.ErrorMessage);
                Console.WriteLine(response.Message);
            }
            Console.ReadKey();
        }
    }
}