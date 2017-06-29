using System;

namespace FlooringMastery
{
    public class ConsoleIO
    {
        public const string SeperatorBar = " ===================================================================== ";

        public const string ErrorMessage = " NO ORDER MATCHING ORDER FOUND * PRESS ENTER TO TRY AGAIN ";

        public const string ReturnToMainMenu = " PRESS ENTER TO RETURN TO MAIN MENU ";

        public const string PressAnyKeyToContinue = " PRESS ANY KEY TO CONTINUE ";

        public const string OrderListFormat = "{0,-14} {1,-6} {2,-11} {3,-9} {4,-7} {5,10}";

        public static void OrderListHeader(string header)
        {
            Console.WriteLine(SeperatorBar);
            Console.WriteLine(OrderListFormat, " DATE", "NUMBER", "NAME", "STATE", "AREA", "TOTAL");
            Console.WriteLine(SeperatorBar);
        }

        public static void ConfirmOrCancelFooter()
        {
            Console.WriteLine();
            Console.WriteLine(SeperatorBar);
            Console.WriteLine(" Press Y TO CONFIRM AND RETURN TO THE MAIN MENU");
            Console.WriteLine(SeperatorBar);
            Console.WriteLine(" Press N TO CANCEL AND RETURN TO THE MAIN MENU");
            Console.WriteLine(SeperatorBar);
            Console.WriteLine();
            Console.Write(" ENTER SELECTION: ");
        }

        public static string YesNoValidation()
        {
            while (true)
            {
                string input = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine(" You must enter Y or N ");
                    Console.WriteLine(PressAnyKeyToContinue);
                    Console.ReadKey();
                }
                else
                {
                    if (input != "Y" && input != "N")
                    {
                        Console.WriteLine(" You must enter Y or N ");
                        Console.WriteLine(PressAnyKeyToContinue);
                        Console.ReadKey();
                        continue;
                    }
                    return input;
                }
            }
        }
    }
}