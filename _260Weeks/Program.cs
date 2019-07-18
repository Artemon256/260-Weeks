using System;
using _260Weeks.GameLogic;

namespace _260Weeks
{
    class ConsoleInterface : PlayerInterface
    {
        public void Init()
        {

        }

        public bool Turn()
        {
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleInterface Interface = new ConsoleInterface();
            Interface.Init();
            while (Interface.Turn()) ;
        }
    }
}
