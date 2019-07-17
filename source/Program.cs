using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using The260WeeksGame;

namespace Proto0
{
    class GameRepresenter
    {
        class MenuOption
        {
            public string Description { get; }
            public Action TheAction { get; }

            public MenuOption(string description, Action theAction)
            {
                Description = description;
                TheAction = theAction;
            }
        }

        private List<MenuOption> menuOptions;
        private GameCore game;
        private GameParams gameParams;

        public GameRepresenter()
        {
            menuOptions = new List<MenuOption>();
        }

        private GameParams.DifficultyLevel ChooseDifficulty()
        {
            Console.Clear();

            Console.WriteLine("Choose difficulty:\n");
            Console.WriteLine("Easy (0)");
            Console.WriteLine("Moderate (1)");
            Console.WriteLine("Medium (2)");
            Console.WriteLine("Hard (3)");
            Console.WriteLine("Nightmare (4)");

            int answer = 0;
            if (!Int32.TryParse(Console.ReadLine(), out answer))
                answer = 2;

            return (GameParams.DifficultyLevel)answer;
        }



        public void StartGame()
        {
            game = GameCore.getInstance();
            gameParams = GameParams.getInstance();

            gameParams.Difficulty = ChooseDifficulty();
            gameParams.NumberOfBusinessmen = GameCore.RandomGenerator.Next(1, 10);
            gameParams.NumberOfMassMedia = GameCore.RandomGenerator.Next(1, 10);

            game.StartGame();


            menuOptions.Add(new MenuOption("Show president's stats", ShowPresidentStats));
            menuOptions.Add(new MenuOption("Show businessmen's stats", ShowBusinessmenStats));
            menuOptions.Add(new MenuOption("Show media stats", ShowMediaStats));
            menuOptions.Add(new MenuOption("Pass to next turn", NextTurn));
            menuOptions.Add(new MenuOption("Show all opinions", ShowAllOpinions));
            menuOptions.Add(new MenuOption("Help on cheat codes", ShowCheatCodeHelp));
        }

        public void Play()
        {

            while (game.GameOn())
            {
                if (!ShowMenu())
                    return;
            }
        }

        private void ShowPause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private bool ShowMenu()
        {
            Console.Clear();

            Console.WriteLine($"Current turn = {game.CurrentTurn}");
            Console.WriteLine("Choose an option:\n");

            for (int i = 0; i < menuOptions.Count; i++)
            {
                MenuOption option = menuOptions[i];
                Console.WriteLine($"{option.Description} ({i})");
            }

            Console.WriteLine($"Exit ({menuOptions.Count})");

            string answer = Console.ReadLine();

            int command;

            if (!int.TryParse(answer, out command))
            {
                if (CheatCodeParser.getInstance().ParseCheatCode(answer))
                    Console.WriteLine("Cheat code activated!");
                else
                    Console.WriteLine("Wrong command!");
                ShowPause();
                return true;
            }

            if (command == menuOptions.Count)
                return false;

            menuOptions[command].TheAction();

            return true;
        }

        #region Implementation of menu actions

        private void showGameMembersOpinions(GameMember whoseOpinion)
        {
            foreach (GameMember aboutWhom in game.Members)
            {
                if (whoseOpinion.Opinions.ContainsKey(aboutWhom))
                {
                    string message = whoseOpinion.Name + "  (" + whoseOpinion.GetType().Name + ")  " + GameMember.Adjust(whoseOpinion.Opinions[aboutWhom]).ToString("N7") + " => " + aboutWhom.Name + "  (" + aboutWhom.GetType().Name + ")";

                    Console.WriteLine(message);
                }
            }
        }

        private void showOpinionsAboutGameMember(GameMember aboutWhom)
        {
            foreach (GameMember whoseOpinion in game.Members)
            {
                if (whoseOpinion.Opinions.ContainsKey(aboutWhom))
                {
                    string message = whoseOpinion.Name + "  (" + whoseOpinion.GetType().Name + ")  " + GameMember.Adjust(whoseOpinion.Opinions[aboutWhom]).ToString("N7") + " => " + aboutWhom.Name + "  (" + aboutWhom.GetType().Name + ")";

                    Console.WriteLine(message);
                }
            }
        }

        private void ShowAllOpinions()
        {
            Console.Clear();

            foreach (GameMember whoseOpinion in game.Members)
            {
                showGameMembersOpinions(whoseOpinion);
                Console.WriteLine();
            }

            ShowPause();
        }

        private void ShowPresidentStats()
        {
            Console.Clear();

            Console.WriteLine("ID = {0}", game.Player.Id);
            showOpinionsAboutGameMember(GameCore.getInstance().Player);

            ShowPause();
        }

        private void ShowBusinessmenStats()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine(String.Format("Number of businessmen = {0}", game.Businessmen.Count));

                int id = 0;

                foreach (Businessman businessman in game.Businessmen)
                {
                    string message = String.Format("\tName = {0}\n", businessman.Name)
                                    + String.Format("\tID = {0}\n", businessman.Id)
                                    + String.Format("\tTo show list of HIS opinions, enter {0}\n", id)
                                    + String.Format("\tTo show list of opinions ABOUT HIM, enter {0}\n", id + 1);



                    message += String.Format("\tService points = {0}\n", businessman.ServicePoint.ToString());
                    Console.WriteLine(message);

                    id += 2;
                }

                Console.WriteLine(String.Format("If you want to exit the panel, enter any number that is greater than or equal to {0}", 2 * game.Businessmen.Count));

                int command = 0;

                if (!int.TryParse(Console.ReadLine(), out command))
                {
                    Console.WriteLine("Enter numbers, please!");
                    continue;
                }

                if (command >= game.Businessmen.Count * 2)
                    return;

                Businessman chosenBusinessman = game.Businessmen[command / 2];

                if (command % 2 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("List of {0}'s opinions:\n", chosenBusinessman.Name);
                    showGameMembersOpinions(chosenBusinessman);
                    ShowPause();
                    continue;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("List of opinions about {0}:\n", chosenBusinessman.Name);
                    showOpinionsAboutGameMember(chosenBusinessman);
                    ShowPause();
                    continue;
                }
            }
        }

        private GameMember selectTarget()
        {
            Console.Clear();

            Console.WriteLine("Choose the target:");

            for (int i = 0; i < game.Members.Count; i++)
            {
                Console.WriteLine(game.Members[i].Name + " (" + i.ToString() + ")");
            }

            int targetId = Convert.ToInt32(Console.ReadLine());

            return game.Members[targetId];
        }

        private int selectDuration()
        {
            Console.Clear();

            Console.Write("Duration of the campaign:");

            return Convert.ToInt32(Console.ReadLine());
        }

        private MassMediaUnit.Campaign.CampaignMode selectCampaignMode()
        {
            Console.Clear();

            Console.WriteLine("Select mode:");

            Console.WriteLine("Against (0)");
            Console.WriteLine("Pro (1)");

            int mode = Convert.ToInt32(Console.ReadLine());

            return (MassMediaUnit.Campaign.CampaignMode)mode;
        }

        private void ShowMediaStats()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(String.Format("Number of media = {0}", game.MassMedia.Count));

                int id = 0;

                foreach (MassMediaUnit media in game.MassMedia)
                {
                    string Message = String.Format("\tName = {0}\n", media.Name)
                                    + String.Format("\tID = {0}\n", media.Id)
                                    + String.Format("\tOwner = {0}   (Opinion about you = {1})\n", media.Owner.Name, GameMember.Adjust(media.Owner.Opinions[game.Player]))
                                    + String.Format("\tIf you want list of opinions ABOUT IT, enter {0}\n", id)
                                    + String.Format("\tIf you want to start a campaign with that media, enter {0}\n", id + 1);


                    id += 2;
                    Console.WriteLine(Message);
                }

                Console.WriteLine(String.Format("If you want to exit the panel, enter any number that is greater than or equal to {0}", 2 * game.MassMedia.Count));

                int command = 0;

                if (!int.TryParse(Console.ReadLine(), out command))
                {
                    Console.WriteLine("Enter numbers, please!");
                    continue;
                }

                if (command >= 2 * game.MassMedia.Count)
                    return;

                MassMediaUnit chosenMedia = game.MassMedia[command / 2];

                if (command % 2 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("List of opinions about {0}:\n", chosenMedia.Name);
                    showOpinionsAboutGameMember(chosenMedia);
                    ShowPause();
                }
                else
                {
                    GameMember target = selectTarget();
                    int duration = selectDuration();
                    MassMediaUnit.Campaign.CampaignMode campaignMode = selectCampaignMode();

                    if (chosenMedia.AddCampaign(target, duration, campaignMode))
                    {
                        Console.WriteLine("Campaign succefully started!");
                    }
                    else
                    {
                        Console.WriteLine("The owner refused to start the campaign");
                    }
                }


                Console.ReadKey();

            }
        }

        private void ShowCheatCodeHelp()
        {
            Console.Clear();

            Console.WriteLine("Here is some help on cheat codes:\n");
            //  Console.WriteLine("NOTICE: If a name contains spaces, replace them with '_' symbol\n(e.g. Instead of 'Mr. President' you should write 'Mr._President'\n");

            Console.WriteLine("opinion $whoseOpinion_id$ $aboutWhom_id$ $value$");
            Console.WriteLine("\t\t--- Sets $whoseOpinion$ about $aboutWhom$ to $value$ (in unadjusted format)");
            Console.WriteLine();

            Console.WriteLine("owner $media_id$ $newOwner_id$");
            Console.WriteLine("\t\t--- Sets $newOwner$ as an owner of $media$");
            Console.WriteLine();

            ShowPause();
        }

        private void NextTurn()
        {
            game.NextTurn();

            ShowPause();
        }

        #endregion
    }




    class Program
    {
        static void Main(string[] args)
        {
            GameRepresenter gr = new GameRepresenter();

            gr.StartGame();
            gr.Play();
        }
    }
}
