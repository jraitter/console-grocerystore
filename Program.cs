using System;
using escape_corona.Controllers;
using escape_corona.Interfaces;

namespace escape_corona
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            IGameController gc = new GameController();
            gc.Run();
        }
    }
}
