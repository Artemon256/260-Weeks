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
            string help = "Usage: show opinions <about|of> <id|name>";
            if (parts.Length < 4)
            {
                Console.WriteLine(help);
                Console.ReadKey();
                return;
            }

            uint id = 0;
            Member target;
            if (uint.TryParse(parts[3], out id))
                target = core.GetMemberById(id);
            else
                target = core.GetMemberByName(parts[3]);

            if (target == null)
            {
                Console.WriteLine(help);
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"{target.Name} ({target.GetType().Name} / {target.ID})");
            Console.WriteLine("===");
            switch (parts[2])
            {
                case "of":
                    if (target is President || target is MassMediaUnit)
                    {
                        Console.WriteLine("Not applicable");
                        break;
                    }
                    foreach (KeyValuePair<Member, double> entry in target.Opinions)
                        Console.WriteLine($"{entry.Key.Name} ({entry.Key.GetType().Name} / {entry.Key.ID}): {entry.Value}");
                    break;
                case "about":
                    foreach (Member member in core.Members)
                    {
                        double opinion = 0;
                        if (!member.Opinions.TryGetValue(target, out opinion))
                            continue;
                        Console.WriteLine($"{member.Name} ({member.GetType().Name} / {member.ID}): {opinion}");
                    }
                    break;
                default:
                    Console.WriteLine(help);
                    break;
            }
            Console.ReadKey();
        }

        private void commandShowlist()
        {
            foreach (Member member in Core.getInstance().Members)
                Console.WriteLine($"{member.Name} ({member.GetType().Name} / {member.ID})");
            Console.ReadKey();
        }

        private void commandShow(string[] parts)
        {
            string help =
@"Usage: show <list|opinions>
list        list of game members
opinions    opinions of/about member";
            if (parts.Length < 2)
            {
                Console.WriteLine(help);
                Console.ReadKey();
                return;
            }
            switch (parts[1])
            {
                case "list":
                    commandShowlist();
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
            SocialGroup sg = new SocialGroup("Proletariat");
            sg.InitOpinions();
            return;
            ConsoleInterface Interface = new ConsoleInterface();
            bool gameOn = true;
            while (gameOn)
                gameOn = Interface.Turn();
            Console.WriteLine("Game over");
        }
    }
}
