using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class Businessman : GameMember
    {
        private string name = "";

        public int ServicePoint;

        public int AbsoluteLoyalty;
        public double AdjustedLoyalty
        {
            get
            {
                return adjust(AbsoluteLoyalty);
            }
        }

        public string Name
        {
            get
            {
                return name;
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

        }

        public static Businessman GenerateRandom() // TODO: Remove and make another function which will base on the game difficulty
        {
            var firstName = GameCore.RandomObjectFromStringList(GameCore.FirstNameList);
            var secondName = GameCore.RandomObjectFromStringList(GameCore.SecondNameList);
            Businessman result = new Businessman(firstName + " " + secondName, GameCore.random.Next(-30, 30), 
                                                GameCore.random.Next(-30, 30), GameCore.random.Next(0, 10));
            GameCore.SecondNameList.Remove(secondName);
            return result;
        }
    }
}
