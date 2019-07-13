using System;
using System.Collections.Generic;

namespace The260WeeksGame
{
    public abstract class GameMember
    {
        protected string name="";
        public Dictionary<GameMember, double> Opinions;

        public GameMember(string name) {
            this.name = name;
            Opinions = new Dictionary<GameMember, double>();
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public static double Adjust(double value)
        {
            double result = (Math.Atan(Math.Exp(0.06 * value)) * 4)/Math.PI - 1;
            
            if (result <= -1)
                return -1 + double.Epsilon;

            if(result >= 1)
                return 1 - double.Epsilon;

            return result;
        }

        public static double Unadjust(double value)
        {
            if (value <= -1)
                return -double.NegativeInfinity;
            if (value >= 1)
                return double.PositiveInfinity;

            return Math.Log(Math.Tan((value + 1) * Math.PI * 0.25)) / 0.06;
        }

        public abstract void GenerateOpinions();
        public abstract void Turn();
    }
}