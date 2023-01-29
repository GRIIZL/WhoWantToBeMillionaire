using System;
using System.Text;

namespace Кто_хочет_стать_миллионером_НАСЛЕДОВВАНИЕ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Game game = new Game();
            game.StartGame();
        }
    }
}