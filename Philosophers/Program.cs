using System;

namespace Philosophers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Philosophers p = new Philosophers();
            p.ActivateDinner();


            Console.ReadLine();
        }
    }
}
