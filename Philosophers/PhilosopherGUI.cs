using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace Philosophers
{
    public class PhilosopherGUI
    {
        Point[] points = new Point[]
        {
            new Point(10, 5),
            new Point(18, 9),
            new Point(14, 13),
            new Point(6, 13),
            new Point(2, 9)
        };

        Philosophers philData;


        private string[] philosopherAscii;

        public void Initialise(Philosophers _philData)
        {
            Console.Clear();
            philData = _philData;
            

            philosopherAscii = new string[]
            {
                @" O      ",
                @"( )     "
            };

            PrintArray(philosopherAscii, points[0].X, points[0].Y, null, ConsoleColor.White);
            PrintArray(philosopherAscii, points[1].X, points[1].Y, null, ConsoleColor.White);
            PrintArray(philosopherAscii, points[2].X, points[2].Y, null, ConsoleColor.White);
            PrintArray(philosopherAscii, points[3].X, points[3].Y, null, ConsoleColor.White);
            PrintArray(philosopherAscii, points[4].X, points[4].Y, null, ConsoleColor.White);

            Console.SetCursorPosition(5, 20);
            Console.WriteLine($"Press any key to start!");
            Console.ReadKey();
            philData.Start();
        }

        public void Start()
        {
            Console.Clear();
            Console.SetCursorPosition(5, 1);
            Console.Write("White = Trying to eat.  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Green");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" = Eating.  ");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Blue");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" = Meditating.  ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Red");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" = Dead.");


            Thread guiThread = new Thread(new ThreadStart(MainGUILoop));
            guiThread.Start();
        }

        /// <summary>
        /// Updates the status of the philosophers every 100ms, and display them accordingly.
        /// White with ... = Trying to eat.
        /// Green = Eating, with time remaining.
        /// Blue = Meditating, with time remaining.
        /// Red = Dead.
        /// </summary>
        private void MainGUILoop()
        {
            while (philData.IsRunning)
            {
                foreach (Philosopher philosopher in philData.philosophers)
                {
                    switch (philosopher.Status)
                    {
                        case PhilosopherStatus.Eating:

                            PrintArray(philosopherAscii, points[philosopher.ID].X, points[philosopher.ID].Y, null, ConsoleColor.Green);
                            string eatingTimeString = philosopher.EatingTimeRemaining.ToString();

                            // Set the cursor to the correct place to print the time remaining
                            Console.SetCursorPosition(points[philosopher.ID].X + 4, points[philosopher.ID].Y + 1);

                            Console.Write(FormatTimeString(eatingTimeString));
                            break;

                        case PhilosopherStatus.Meditating:

                            PrintArray(philosopherAscii, points[philosopher.ID].X, points[philosopher.ID].Y, null, ConsoleColor.Blue);
                            string meditatingTimeString = philosopher.MeditationTimeRemaining.ToString();

                            // Set the cursor to the correct place to print the time remaining
                            Console.SetCursorPosition(points[philosopher.ID].X + 4, points[philosopher.ID].Y + 1);

                            Console.Write(FormatTimeString(meditatingTimeString));
                            break;

                        case PhilosopherStatus.Dead:
                            PrintArray(philosopherAscii, points[philosopher.ID].X, points[philosopher.ID].Y, null, ConsoleColor.Red);

                            // Set the cursor to the correct place to print the time remaining
                            Console.SetCursorPosition(points[philosopher.ID].X + 4, points[philosopher.ID].Y + 1);

                            Console.Write("    "); // clear
                            break;

                        case PhilosopherStatus.Idle:
                            PrintArray(philosopherAscii, points[philosopher.ID].X, points[philosopher.ID].Y, null, ConsoleColor.White);

                            // Set the cursor to the correct place to print the time remaining
                            Console.SetCursorPosition(points[philosopher.ID].X + 4, points[philosopher.ID].Y + 1);

                            Console.Write("..."); 
                            break;
                    }              
                }

                // Update statistics:

                

                Thread.Sleep(100);
            }

            
        }

        public void PrintException(string error)
        {
            Console.SetCursorPosition(5, 20);
            Console.WriteLine();
        }

        /// <summary>
        /// Formats a time string with 2 decimals
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string FormatTimeString(string input)
        {
            switch (input.Length)
            {
                case 1:
                    return input;

                    case 2:
                    return $"0.0" + input[0];

                case 3:
                    return $"0." + input[0] + input[1];

                case 4:
                    return $"{input[0]}." + input[1] + input[2];

                case 5:
                    return $"{input[0]}{input[1]}." + input[2];

                default:
                    return "";

            }
        }

        /// <summary>
        /// Prints the given string array to the console in the specified location, skipping over eventual blacklisted characters. Blacklist can be null.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="blacklist"></param>
        public void PrintArray(string[] array, int x, int y, List<char> blacklist, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            if (color == ConsoleColor.Black)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            for (int i = 0; i < array.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);

                for (int j = 0; j < array[i].Length; j++)
                {
                    if (blacklist != null && blacklist.Contains(array[i][j]))
                    {
                        // If we need to skip this, we'll increment the cursor position manually by 1
                        int posLeft = Console.CursorLeft;
                        int posTop = Console.CursorTop;

                        Console.SetCursorPosition(posLeft + 1, posTop);
                    }
                    else
                    {
                        Console.Write(array[i][j]);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;

        }

    }
}
