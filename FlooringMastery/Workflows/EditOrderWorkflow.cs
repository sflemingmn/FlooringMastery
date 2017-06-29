using FlooringMastery.BLL.Factories;
using FlooringMastery.BLL.Managers;
using FlooringMastery.Models;
using FlooringMastery.Models.Models.Responses;
using System;

namespace FlooringMastery.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            Console.WriteLine();
            Console.WriteLine(ConsoleIO.SeperatorBar);
            Console.WriteLine(" EDIT ORDER MENU ");
            Console.WriteLine(ConsoleIO.SeperatorBar);
            Console.WriteLine();

            DateTime validatedDate = ValidateDate();
            int validatedOrderNumber = ValidateOrderNumber();

            DisplaySingleOrderResponse response = DisplayOrder(validatedDate, validatedOrderNumber, manager);
            if (response.Success)
            {
                ChangeName(response);
                ChangeState(response);
                ChangeProduct(response);
                ChangeArea(response, manager);
            }
            else
            {
                Console.WriteLine(ConsoleIO.ErrorMessage);
                Console.WriteLine(response.Message);
                Console.ReadKey();
            }
        }

        public DateTime ValidateDate()
        {
            DateTime validatedDateTime = new DateTime();
            bool validDate = false;

            while (!validDate)
            {
                Console.Write(" Enter Order Date In MM/DD/YYYY Format: ");
                string orderDate = Console.ReadLine();

                if (!string.IsNullOrEmpty(orderDate))
                {
                    if (DateTime.TryParse(orderDate, out validatedDateTime))
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
            return validatedDateTime;
        }

        public int ValidateOrderNumber()
        {
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
            return validatedOrderNumber;
        }

        public DisplaySingleOrderResponse DisplayOrder(DateTime validatedDateTime, int validatedOrderNumber, OrderManager manager)
        {
            DisplaySingleOrderResponse response = manager.DisplayOrder(validatedDateTime, validatedOrderNumber);

            if (response.Success)
            {
                Console.WriteLine();
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(" ORDER FOUND - DETAILS BELOW ");
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine();
                Console.WriteLine($" Order Number: {response.OrderDetails.OrderNumber.ToString()} | Order Date: {validatedDateTime.ToString("MM/dd/yyyy")}");
                Console.WriteLine($" Customer Name: {response.OrderDetails.CustomerName}");
                Console.WriteLine($" State: {response.OrderDetails.State}");
                Console.WriteLine($" Product: {response.OrderDetails.ProductType}");
                Console.WriteLine($" Materials: {response.OrderDetails.MaterialCost:c}");
                Console.WriteLine($" Labor: {response.OrderDetails.LaborCost:c}");
                Console.WriteLine($" Tax: {response.OrderDetails.Tax:c}");
                Console.WriteLine($" Total: {response.OrderDetails.Total:c}");

                Console.WriteLine();
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(" ENTER ORDER UPDATES TO ANY OF THE FOUR EDITABLE FIELD BELOW ");
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(" PRESS ENTER TO SKIP OVER CHANGING ANY FIELD ");
                Console.WriteLine(ConsoleIO.SeperatorBar);
            }
            return response;
        }

        public void ChangeName(DisplaySingleOrderResponse response)
        {
            Console.WriteLine();
            Console.Write($" Customer Name: ");

            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                response.OrderDetails.CustomerName = newName;
            }
        }

        public void ChangeState(DisplaySingleOrderResponse response)
        {
            bool validState = false;
            while (!validState)
            {
                Console.Write(" Enter State (with abbreviation or full name): ");
                string newState = (Console.ReadLine().ToUpper());

                if (!string.IsNullOrEmpty(newState))
                {
                    TaxManager taxManager = TaxManagerFactory.Create();
                    Tax tax = taxManager.TaxByState(newState);

                    if (tax != null)
                    {
                        validState = true;
                        response.OrderDetails.State = newState;
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

        public void ChangeProduct(DisplaySingleOrderResponse response)
        {
            bool validProduct = false;
            while (!validProduct)
            {
                Console.Write(" Product Choice: ");
                string newProductType = (Console.ReadLine().ToUpper());

                if (!string.IsNullOrEmpty(newProductType))
                {
                    ProductManager productManager = ProductManagerFactory.Create();
                    Product product = productManager.ProductType(newProductType);

                    if (product != null)
                    {
                        validProduct = true;
                        response.OrderDetails.ProductType = newProductType;
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

        public void ChangeArea(DisplaySingleOrderResponse response, OrderManager manager)
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
                        response.OrderDetails.Area = area;
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

        public void ConfirmChange(DisplaySingleOrderResponse response, OrderManager manager)
        {
            ConsoleIO.ConfirmOrCancelFooter();

            var input = ConsoleIO.YesNoValidation();

            if (input == "Y")
            {
                manager.EditOrder(response.OrderDetails);
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