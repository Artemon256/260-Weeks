using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class Businessman : IPubliclyExposed
    {
        //private List<MassMediaUnit> massMedia;
        private double publicPopularity;
        private int absoluteFriendship;
        private int servicePoint;
        private string name = "";

        

        public double PublicPopularity
        {
            get
            {
                return publicPopularity;
            }

            set
            {
                publicPopularity = value;
            }
        }
        public int AbsoluteFriendship
        {
            get
            {
                return absoluteFriendship;
            }

            set
            {
                absoluteFriendship = value; 
            }
        }
        public int ServicePoint
        {
            get
            {
                return servicePoint;
            }
            set
            {
                servicePoint = value;
            }
        }
        public double AdjustedFriendship
        {
            get
            {
                return GameCore.AdjustFriendship(absoluteFriendship);
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }



        public Businessman(string name, int publicPopularity, int absoluteFriendship, int servicePoint)
        {
            this.name = name;
            PublicPopularity = publicPopularity;
            AbsoluteFriendship = absoluteFriendship;
            ServicePoint = servicePoint;
        }

        public static Businessman GenerateRandom()
        {
            Businessman result = new Businessman(GameCore.RandomObjectFromStringList(GameCore.FirstNameList) + " " + GameCore.RandomObjectFromStringList(GameCore.SecondNameList)
                                                ,GameCore.random.Next(-100, 100), GameCore.random.Next(0, 100), GameCore.random.Next(0, 10));

            return result;
        }
    }
}
