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

        public void StartGame()
        {
            game = new GameCore(GameCore.Difficulty.Moderate);
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

        private void ShowMediaStats()
        {
            Console.WriteLine(String.Format("Number of media = {0}", game.MassMedia.Count));

            int id = 0;

            foreach (var media in game.MassMedia)
            {
                string Message = String.Format("\tName = {0}\n", media.Name)
                                + String.Format("\tOwner = {0}\n", media.Owner.Name)
                                + String.Format("\tFriendship with owner = {0}\n", media.Owner.AdjustedLoyalty.ToString("N5"))
                                + String.Format("\tRating = {0}\n", media.AdjustedRating.ToString())
                                + String.Format("\tIf you want to start a campaign with that media, enter {0}\n", id.ToString());

                
                
                Console.WriteLine(Message);
            }

            Console.WriteLine("If you wish to close the panel, press any key except for those which stand for starting a campaign\n");

            ConsoleKeyInfo key = Console.ReadKey();

            if(key.KeyChar < game.MassMedia.Count)
            {

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
