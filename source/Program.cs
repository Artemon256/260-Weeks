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

        public GameRepresenter()
        { 
        }

        private GameCore.Difficulty ChooseDifficulty()
        {
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("Easy (1)");
            Console.WriteLine("Moderate (2)");
            Console.WriteLine("Medium (3)");
            Console.WriteLine("Hard (4)");
            Console.WriteLine("Nightmare (5)");

            int answer = Convert.ToInt32(Console.ReadLine());

            return (GameCore.Difficulty)answer;
        }

        public void StartGame()
        {
            game = new GameCore(ChooseDifficulty());
            game.StartGame();
        }

        private void ShowPresidentStats()
        {
            Console.WriteLine(String.Format("Rating = {0}", GameCore.Player.AdjustedRating));
        }

        private void ShowBusinessmenStats()
        {
            Console.WriteLine(String.Format("Number of businessmen = {0}", game.Businessmen.Count));

            foreach(var businessman in game.Businessmen)
            {
                string Message = String.Format("\tName = {0}\n", businessman.Name)
                                + String.Format("\tRating = {0}\n", businessman.AdjustedRating.ToString())
                                + String.Format("\tabsFriendship = {0}\n", businessman.AbsoluteLoyalty.ToString())
                                + String.Format("\tadjFriendship = {0}\n", businessman.AdjustedLoyalty.ToString("N5"))
                                + String.Format("\tService points = {0}\n", businessman.ServicePoint.ToString());
                Console.WriteLine(Message); 
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
                                    + String.Format("\tFriendship with owner = {0}\n", media.Owner.AdjustedLoyalty.ToString("N5"))
                                    + String.Format("\tRating = {0}\n", media.AdjustedRating.ToString())
                                    + String.Format("\tIf you want to start a campaign with that media, enter {0}\n", id.ToString());


                    id++;
                    Console.WriteLine(Message);
                }

                Console.WriteLine(String.Format("If you want to exit the panel, enter any number that is greater than or equal to {0}", game.MassMedia.Count));

                int command = 0;

                if (!int.TryParse(Console.ReadLine(), out command))
                {
                    Console.WriteLine("Enter numbers, please!");
                    continue;
                }

                if (command >= game.MassMedia.Count)
                    return;

                var chosenMedia = game.MassMedia[command];

                var target = selectTarget();
                var duration = selectDuration();
                var campaignMode = selectCampaignMode();

                if(chosenMedia.AddCampaign(target, duration, campaignMode))
                {
                    Console.WriteLine("Campaign succefully started!");
                }
                else
                {
                    Console.WriteLine("The owner refused to start the campaign");
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
                Console.WriteLine("End Game (5)");

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
