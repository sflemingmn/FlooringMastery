using FlooringMastery.Workflows;
using System;

namespace FlooringMastery
{
    public class MainMenu
    {
        public static void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(" FLOORING ORDERING MAIN MENU ");
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine(" 1) DISPLAY Order(s) ");
                Console.WriteLine(" 2) ADD Order ");
                Console.WriteLine(" 3) EDIT Order ");
                Console.WriteLine(" 4) REMOVE Order ");
                Console.WriteLine(" 5) QUIT System ");
                Console.WriteLine(ConsoleIO.SeperatorBar);
                Console.WriteLine();
                Console.Write(" ENTER SELECTION BY NUMBER: ");

                string input = Console.ReadLine().ToUpper();

                switch (input)
                {
                    case "1":

                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine(ConsoleIO.SeperatorBar);
                        Console.WriteLine(" ORDER OPTIONS ");
                        Console.WriteLine(ConsoleIO.SeperatorBar);
                        Console.WriteLine(" 1) DISPLAY SINGLE ORDER: By Date & Order Number ");
                        Console.WriteLine(" 2) DISPLAY ALL ORDER: For a Given Date ");
                        Console.WriteLine(" 3) RETURN TO MAIN MENU ");
                        Console.WriteLine(ConsoleIO.SeperatorBar);
                        Console.WriteLine();
                        Console.Write(" ENTER SELECTION BY NUMBER: ");

                        input = Console.ReadLine().ToUpper();

                        switch (input)
                        {
                            case "1":
                                DisplaySingleOrderWorkflow displaySingleOrderWorkflow = new DisplaySingleOrderWorkflow();
                                displaySingleOrderWorkflow.Execute();
                                break;

                            case "2":
                                DisplayAllOrdersWorkflow displayAllOrdersWorkflow = new DisplayAllOrdersWorkflow();
                                displayAllOrdersWorkflow.Execute();
                                break;

                            case "3":
                            case "Q":
                            case "q":
                                break;
                        }
                        break;

                    case "2":
                        AddOrderWorkflow addWorkflow = new AddOrderWorkflow();
                        addWorkflow.Execute();
                        break;

                    case "3":
                        EditOrderWorkflow editWorkflow = new EditOrderWorkflow();
                        editWorkflow.Execute();
                        break;

                    case "4":
                        RemoveOrderWorkflow removeWorkflow = new RemoveOrderWorkflow();
                        removeWorkflow.Execute();
                        break;

                    case "5":
                    case "Q":
                    case "q":
                        return;
                }
            }
        }
    }
}