using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class GameCore
    {
        private bool gameOn;

        private List<Businessman> businessmen;
        private List<MassMediaUnit> massMedia;

        public List<Businessman> Businessmen
        {
            get
            {
                return businessmen;
            }
        }
        public List<MassMediaUnit> MassMedia
        {
            get
            {
                return massMedia;
            }
        }

        public bool GameOn()
        {
            return gameOn;
        }

        public President Player;

        public GameCore()
        {
            if(FirstNameList.Count == 0)
            {
                InitFirstNameList();
            }
            if(SecondNameList.Count == 0)
            {
                InitSecondNameList();
            }
            if(MediaNameList.Count == 0)
            {
                InitMediaNameList();
            }
        }

        public void StartGame()
        {
            gameOn = true;
            Player = new President();
            businessmen = new List<Businessman>();
            massMedia = new List<MassMediaUnit>();

            int numberOfBusinessmen = random.Next(1,5);
            int numberOfMassMedia = random.Next(1, 10);

            for(int i = 0; i < numberOfBusinessmen; i++)
            {
                businessmen.Add(Businessman.GenerateRandom());
            }

            for(int i = 0; i < numberOfMassMedia; i++)
            {
                massMedia.Add(MassMediaUnit.GenerateRandom());
            }

            for(int i = 0; i < numberOfBusinessmen; i++)
            {
                for(int j = 0; j < numberOfMassMedia; j++)
                {
                    if(massMedia[j].Owner == null)
                    {
                        if (random.Next(2) == 0 || i == numberOfBusinessmen - 1)
                        {
                            massMedia[j].Owner = businessmen[i];
                        }
                    }
                }
            }
        }
        public void NextTurn()
        {
            foreach(var media in massMedia)
            {
                double P = Convert.ToDouble(media.PoliticalInfluence) / 100;
                double D = media.Owner.AdjustedFriendship;
                double R = random.NextDouble() / 2 + 0.5;

                Player.PublicPopularity += P * D * R;

                media.ActCampaigns();
            }
        }

        public static Random random = new Random();
        public static double AdjustFriendship(int absoluteFriendShip)
        {
            double result = Math.Atan(Math.Exp(0.06 * Convert.ToDouble(absoluteFriendShip))) * 1.27 - 1;

            if (result < -1)
                return -1;

            if(result > 1)
                return 1;

            return result;
        }
        public static List<string> FirstNameList = new List<string>();
        public static List<string> SecondNameList = new List<string>();
        public static List<string> MediaNameList = new List<string>();

        public static string RandomObjectFromStringList(List<string> from)
        {
            if (from.Count == 0)
                return null;
            return from[random.Next(0, from.Count)];
        }

        private void InitFirstNameList()
        {
            FirstNameList.Add("John");
            FirstNameList.Add("Vasily");
            FirstNameList.Add("Ihor");
            FirstNameList.Add("Abraham");
        }

        private void InitSecondNameList()
        {
            SecondNameList.Add("Miller");
            SecondNameList.Add("Simpson");
            SecondNameList.Add("Galushka");
        }

        private void InitMediaNameList()
        {
            MediaNameList.Add("Inter");
            MediaNameList.Add("Pohoda FM");
            MediaNameList.Add("Zhopa UA");
        }
    }
}
