using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using The260WeeksGame;

namespace Proto0
{
    class GameRepresenter
    {
        private GameCore game;
        private GameParams gameParams;

        public GameRepresenter()
        { 
        }

        private GameParams.DifficultyLevel ChooseDifficulty()
        {
            Console.Clear();
            
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("Easy (1)");
            Console.WriteLine("Moderate (2)");
            Console.WriteLine("Medium (3)");
            Console.WriteLine("Hard (4)");
            Console.WriteLine("Nightmare (5)");

            int answer = 0;
            if (!Int32.TryParse(Console.ReadLine(), out answer))
                answer = 2;

            return (GameParams.DifficultyLevel)answer;
        }

        private void showGameMembersOpinions(GameMember whoseOpinion)
        {
            foreach (var aboutWhom in game.Members)
            {
                if (whoseOpinion.Opinions.ContainsKey(aboutWhom))
                {
                    string message = whoseOpinion.Name + "  (" + whoseOpinion.GetType().Name + ")  " + GameMember.Adjust(whoseOpinion.Opinions[aboutWhom]).ToString("N4") + " => " + aboutWhom.Name + "  (" + aboutWhom.GetType().Name + ")";

                    Console.WriteLine(message);
                }
            }
        }

        private void showOpinionsAboutGameMember(GameMember aboutWhom)
        {
            foreach (var whoseOpinion in game.Members)
            {
                if (whoseOpinion.Opinions.ContainsKey(aboutWhom))
                {
                    string message = whoseOpinion.Name + "  (" + whoseOpinion.GetType().Name + ")  " + GameMember.Adjust(whoseOpinion.Opinions[aboutWhom]).ToString("N4") + " => " + aboutWhom.Name + "  (" + aboutWhom.GetType().Name + ")";

                    Console.WriteLine(message);
                }
            }
        }

        private void showAllOpinions()
        {
            Console.Clear();

            foreach(var whoseOpinion in game.Members)
            {
                showGameMembersOpinions(whoseOpinion);
                Console.WriteLine();
            }
        }

        public void StartGame()
        {
            game = GameCore.getInstance();
            gameParams = GameParams.getInstance();

            gameParams.Difficulty = ChooseDifficulty();
            gameParams.NumberOfBusinessmen = GameCore.RandomGenerator.Next(1, 10);
            gameParams.NumberOfMassMedia = GameCore.RandomGenerator.Next(1, 10);            

            game.StartGame();

            //showAllOpinions();


        }

        private void ShowPresidentStats()
        {
            showOpinionsAboutGameMember(GameCore.getInstance().Player);
        }

        private void ShowBusinessmenStats()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine(String.Format("Number of businessmen = {0}", game.Businessmen.Count));

                int id = 0;

                foreach (var businessman in game.Businessmen)
                {
                    string message = String.Format("\tName = {0}\n", businessman.Name)
                                    +String.Format("\tTo show list of HIS opinions, enter {0}\n", id)
                                    +String.Format("\tTo show list of opinions ABOUT HIM, enter {0}\n", id+1);
                          
      

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

                if(command % 2 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("List of {0}'s opinions:\n", chosenBusinessman.Name);
                    showGameMembersOpinions(chosenBusinessman);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("List of opinions about {0}:\n", chosenBusinessman.Name);
                    showOpinionsAboutGameMember(chosenBusinessman);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }
            }
        } 

        private GameMember selectTarget()
        {
            Console.Clear();

            Console.WriteLine("Choose the target:");

            for(int i = 0; i < game.Members.Count; i++)
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

                foreach (var media in game.MassMedia)
                {
                    string Message = String.Format("\tName = {0}\n", media.Name)
                                    + String.Format("\tOwner = {0}\n", media.Owner.Name)
                                    + String.Format("\tIf you want list of opinions ABOUT IT, enter {0}\n", id)
                                    + String.Format("\tIf you want to start a campaign with that media, enter {0}\n", id + 1);


                    id+=2;
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

                var chosenMedia = game.MassMedia[command / 2];

                if(command % 2 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("List of opinions about {0}:\n", chosenMedia.Name);
                    showOpinionsAboutGameMember(chosenMedia);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    var target = selectTarget();
                    var duration = selectDuration();
                    var campaignMode = selectCampaignMode();

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

        public void Play()
        {
            
            while (game.GameOn())
            {
                Console.Clear();

                Console.WriteLine("What do you want to show?");
                Console.WriteLine("President stats (1)");
                Console.WriteLine("Info about businessmen (2)");
                Console.WriteLine("Info about mass media (3)");
                Console.WriteLine("Pass to next turn (4)");
                Console.WriteLine("Show all opinions (5)");
                Console.WriteLine("End Game (6)");

                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        ShowPresidentStats();
                        break;
                    case "2":
                        ShowBusinessmenStats();
                        break;
                    case "3":
                        ShowMediaStats();
                        break;
                    case "4":
                        game.NextTurn();
                        break;
                    case "5":
                        showAllOpinions();
                        break;
                    case "6":
                        Console.WriteLine("Goodbye!\n Press any key to continue...");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Wrong Command!");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }       
        }
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
