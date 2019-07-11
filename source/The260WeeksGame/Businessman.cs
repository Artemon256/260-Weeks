using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    public class Businessman : GameMember
    {

        public int ServicePoint;

        // public int AbsoluteLoyalty;
        // public double AdjustedLoyalty
        // {
        //     get
        //     {
        //         return adjust(AbsoluteLoyalty);
        //     }
        // }

        public Businessman(string name, int absoluteRating, int absoluteLoyalty, int servicePoint)
        {
            this.name = name;
            // AbsoluteLoyalty = absoluteLoyalty;
            ServicePoint = servicePoint;
        }

        public override void Turn() {
            // AbsoluteLoyalty += (int) Math.Round(ServicePoint * 0.2);
            ServicePoint = (int) Math.Round(ServicePoint * 0.8);
        }

        public static Businessman GenerateRandom()
        {
            GameCore core = GameCore.getInstance();
            var firstName = core.RandomObjectFromList(GameStringManager.getInstance().FirstNames);
            var secondName = core.RandomObjectFromList(GameStringManager.getInstance().SecondNames); 

            var fullName = firstName + " " + secondName;
            var rating = GameCore.RandomGenerator.Next(-10, 30);
            int loyalty = 0;

            switch (GameParams.getInstance().Difficulty)
            {
                case GameParams.DifficultyLevel.Easy:
                    loyalty = GameCore.RandomGenerator.Next(5, 20);
                    break;
                case GameParams.DifficultyLevel.Moderate:
                    loyalty = GameCore.RandomGenerator.Next(-5, 20);
                    break;
                case GameParams.DifficultyLevel.Medium:
                    loyalty = GameCore.RandomGenerator.Next(-10, 10);
                    break;
                case GameParams.DifficultyLevel.Hard:
                    loyalty = GameCore.RandomGenerator.Next(-20, 5);
                    break;
                case GameParams.DifficultyLevel.Nightmare:
                    loyalty = GameCore.RandomGenerator.Next(-20, -5);
                    break;
            }

            Businessman result = new Businessman(fullName, rating, loyalty, 50); // Service points on start are equal to 50 until we develop ways to earn them
            GameStringManager.getInstance().SecondNames.Remove(secondName);
            return result;
        }
    }
}
