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

        public static Businessman GenerateRandom()
        {
            Businessman result = new Businessman(GameCore.RandomObjectFromStringList(GameCore.FirstNameList) + " " + GameCore.RandomObjectFromStringList(GameCore.SecondNameList)
                                                ,GameCore.random.Next(-100, 100), GameCore.random.Next(0, 100), GameCore.random.Next(0, 10));

            return result;
        }
    }
}
