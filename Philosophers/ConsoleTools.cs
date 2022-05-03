using System;
using System.Collections.Generic;
using System.Text;

namespace Philosophers
{
    public static class ConsoleTools
    {
        /// <summary>
        /// Asks the user to choose between two keys, and keeps asking until the input is valid
        /// </summary>
        /// <param name="k1"></param>
        /// <param name="k2"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ConsoleKey GetUserChoice(ConsoleKey k1, ConsoleKey k2, string message)
        {
            while (true)
            {
                Console.WriteLine(message);

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == k1 || key.Key == k2)
                {
                    return key.Key;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid choice.");

                }
            }
        }

        /// <summary>
        /// Asks the user to input a double, and keeps asking until the input is valid
        /// </summary>
        /// <param name="phrase"></param>
        /// <param name="maxDecimals"></param>
        /// <returns></returns>
        public static double GetUserInputDouble(string phrase, int maxDecimals = -1)
        {
            string userInput = "";

            while (true)
            {
                Console.WriteLine(phrase);


                userInput = Console.ReadLine();

                // Empty input (only pressed enter for example)
                if (userInput.Length <= 0)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                // Check that it only contains numbers
                if (!IsInputDouble(userInput))
                {
                    Console.WriteLine("Invalid input: can only contain numbers and one comma or dot");
                    continue;
                }
                else
                {
                    break;
                }

                // If asked to, removes excess decimals
                if (maxDecimals != -1)
                {
                    // Count the numbers after the comma.
                    int dIndex = userInput.IndexOf(',');

                    if (dIndex >= 0)
                    {
                        // Limit to a few decimals, first of all its pointless, second also avoid
                        // too big numbers and consequent errors
                        if (userInput.Length - dIndex - 1 > 5)
                        {
                            // Just fix it instead of giving error
                            userInput = userInput.Remove(dIndex + 6);
                        }
                    }
                }


            }

            return double.Parse(userInput);
        }

        /// <summary>
        /// Checks that the input conforms to a Double
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsInputDouble(string input)
        {
            input = input.Replace('.', ',');

            foreach (char c in input)
            {
                // Also accept commas
                if (c == ',')
                {
                    continue;
                }

                // check that it's a number (unicode)
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Requests the user to enter an integer with the corresponding request string, and
        /// makes sure the input is sanitized
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static int GetUserInputInteger(string phrase = "")
        {
            string userInput = "";

            while (true)
            {
                if (phrase != "")
                {
                    Console.WriteLine(phrase);
                }

                userInput = Console.ReadLine();

                // Empty input (only pressed enter for example)
                if (userInput.Length <= 0)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                // Check that it only contains numbers
                if (!IsInputOnlyDigits(userInput))
                {
                    Console.WriteLine("Invalid input: must only contain numbers");
                    continue;
                }
                else
                {
                    break;
                }
            }

            return int.Parse(userInput);
        }

        /// <summary>
        /// Returns true if the string only contains digits
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static bool IsInputOnlyDigits(string input)
        {
            foreach (char c in input)
            {
                // check that it's a number (unicode)
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
