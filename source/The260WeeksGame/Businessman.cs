using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class Businessman : GameMember
    {

        public int ServicePoint;

        public int AbsoluteLoyalty;
        public double AdjustedLoyalty
        {
            get
            {
                return adjust(AbsoluteLoyalty);
            }
        }

        

        public Businessman(string name, int absoluteRating, int absoluteLoyalty, int servicePoint)
        {
            this.name = name;
            AbsoluteRating = absoluteRating;
            AbsoluteLoyalty = absoluteLoyalty;
            ServicePoint = servicePoint;
        }

        public override void Turn() {
            AbsoluteLoyalty += (int) Math.Round(ServicePoint * 0.2);
            ServicePoint = (int) Math.Round(ServicePoint * 0.8);
        }

        public static Businessman GenerateRandom()
        {
            var firstName = GameCore.RandomObjectFromList(GameCore.FirstNameList);
            var secondName = GameCore.RandomObjectFromList(GameCore.SecondNameList); 

            var fullName = firstName + " " + secondName;
            var rating = GameCore.random.Next(-10, 30);
            int loyalty = 0;

            switch (GameCore.GameDifficulty)
            {
                case GameCore.Difficulty.Easy:
                    loyalty = GameCore.random.Next(5, 20);
                    break;
                case GameCore.Difficulty.Moderate:
                    loyalty = GameCore.random.Next(-5, 20);
                    break;
                case GameCore.Difficulty.Medium:
                    loyalty = GameCore.random.Next(-10, 10);
                    break;
                case GameCore.Difficulty.Hard:
                    loyalty = GameCore.random.Next(-20, 5);
                    break;
                case GameCore.Difficulty.Nightmare:
                    loyalty = GameCore.random.Next(-20, -5);
                    break;
            }

            Businessman result = new Businessman(fullName, rating, loyalty, 50); // Service points on start are equal to 50 until we develop ways to earn them
            GameCore.SecondNameList.Remove(secondName);
            return result;
        }
    }
}
