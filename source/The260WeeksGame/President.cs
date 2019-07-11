using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    public class President : GameMember
    {    
      

        public override void Turn() {

        }

        public President()
        {
            name = "Mr. President";

            switch (GameParams.getInstance().Difficulty)
            {
                case GameParams.DifficultyLevel.Easy:
                    AbsoluteRating = GameCore.RandomGenerator.Next(15, 30);
                    break;
                case GameParams.DifficultyLevel.Moderate:
                    AbsoluteRating = GameCore.RandomGenerator.Next(5, 20);
                    break;
                case GameParams.DifficultyLevel.Medium:
                    AbsoluteRating = GameCore.RandomGenerator.Next(-5, 10);
                    break;
                case GameParams.DifficultyLevel.Hard:
                    AbsoluteRating = GameCore.RandomGenerator.Next(-10, 5);
                    break;
                case GameParams.DifficultyLevel.Nightmare:
                    AbsoluteRating = GameCore.RandomGenerator.Next(-15, -5);
                    break;
            }
        }
    }
}
