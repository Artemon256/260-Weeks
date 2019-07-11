using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class President : GameMember
    {    
        private string name = "Mr. President";
        public String Name
        {
            get
            {
                return name;
            }
        }

        public override void Turn() {

        }

        public President()
        {
            switch (GameCore.GameDifficulty)
            {
                case GameCore.Difficulty.Easy:
                    AbsoluteRating = GameCore.random.Next(15, 30);
                    break;
                case GameCore.Difficulty.Moderate:
                    AbsoluteRating = GameCore.random.Next(5, 20);
                    break;
                case GameCore.Difficulty.Medium:
                    AbsoluteRating = GameCore.random.Next(-5, 10);
                    break;
                case GameCore.Difficulty.Hard:
                    AbsoluteRating = GameCore.random.Next(-10, 5);
                    break;
                case GameCore.Difficulty.Nightmare:
                    AbsoluteRating = GameCore.random.Next(-15, -5);
                    break;
            }
        }
    }
}
