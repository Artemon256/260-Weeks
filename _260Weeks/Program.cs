using System;
using System.Collections.Generic;
using _260Weeks.GameLogic;

namespace _260Weeks
{
    class ConsoleInterface : PlayerInterface
    {
        private Core core;

        public ConsoleInterface()
        {
            core = new Core(this);
            core.Init(8, 4);
        }

        private void commandShowOpinions(string[] parts)
        {
            string help = "Usage: show opinions [about|of] <id>";
            if (parts.Length < 4)
            {
                Console.WriteLine(help);
                Console.ReadKey();
                return;
            }
            uint id = 0;
            if (!uint.TryParse(parts[3], out id))
                Console.WriteLine(help);
            else
            {
                Console.WriteLine($"{core.GetMemberById(id).Name} ({core.GetMemberById(id).GetType().Name} / {id})");
                Console.WriteLine("===");
                switch (parts[2])
                {
                    case "of":
                        if (core.GetMemberById(id) is President || core.GetMemberById(id) is MassMediaUnit)
                        {
                            Console.WriteLine("Not applicable");
                            break;
                        }
                        foreach (KeyValuePair<Member, double> entry in core.GetMemberById(id).Opinions)
                            Console.WriteLine($"{entry.Key.Name} ({entry.Key.GetType().Name} / {entry.Key.ID}): {entry.Value}");
                        break;
                    case "about":
                        foreach (Member member in core.Members)
                        {
                            double opinion = 0;
                            if (!member.Opinions.TryGetValue(core.GetMemberById(id), out opinion))
                                continue;
                            Console.WriteLine($"{member.Name} ({member.GetType().Name} / {member.ID}): {opinion}");
                        }
                        break;
                    default:
                        Console.WriteLine(help);
                        break;
                }
            }
            Console.ReadKey();
        }

        private void commandShowIdlist()
        {
            foreach (Member member in Core.getInstance().Members)
                Console.WriteLine($"{member.Name} ({member.GetType().Name}): {member.ID}");
            Console.ReadKey();
        }

        private void commandShow(string[] parts)
        {
            string help =
@"Usage: show [idlist|opinions]
ids      in-game IDs
opinions    opinions of/about member";
            if (parts.Length < 2)
            {
                Console.WriteLine(help);
                Console.ReadKey();
                return;
            }
            switch (parts[1])
            {
                case "ids":
                    commandShowIdlist();
                    return;
                case "opinions":
                    commandShowOpinions(parts);
                    return;
                default:
                    Console.WriteLine(help);
                    break;
            }
            Console.ReadKey();
        }

        private void commandSet(string[] parts)
        {

        }

        public void PlayerTurn()
        {
            string help =
@"Available commands:
show    show statistics on smth
set     set in-game value (not implemented)
turn    pass to next turn
exit    game over";
            bool session = true;
            while (session)
            {
                Console.Clear();
                Console.WriteLine($"Turn: {core.TurnNumber}");
                Console.WriteLine(help);
                string command = Console.ReadLine();
                string[] parts = command.Split(" ");
                if (parts.Length == 0)
                    continue;
                switch (parts[0])
                {
                    case "turn":
                        session = false;
                        break;
                    case "exit":
                        session = false;
                        core.GameOver();
                        break;
                    case "show":
                        commandShow(parts);
                        break;
                    case "set":
                        commandSet(parts);
                        break;
                }
            }
        }

        public bool Turn()
        {
            return core.Turn();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleInterface Interface = new ConsoleInterface();
            bool gameOn = true;
            while (gameOn)
                gameOn = Interface.Turn();
        }
    }
}
