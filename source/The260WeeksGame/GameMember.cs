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
            double result = Math.Atan(Math.Exp(0.06 * value)) * 1.27 - 1;

            if (result < -1)
                return -1;

            if(result > 1)
                return 1;

            return result;
        }

        public static double Unadjust(double value)
        {
            if (value <= -1)
                return -double.NegativeInfinity;
            if (value >= 1)
                return double.PositiveInfinity;
            return 16.6666 * Math.Log( Math.Tan( 0.7874 * (value + 1) ) );
        }

        public abstract void GenerateOpinions();
        public abstract void Turn();
    }
}