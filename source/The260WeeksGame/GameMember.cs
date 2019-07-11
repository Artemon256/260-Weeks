using System;
using System.Collections.Generic;

namespace The260WeeksGame
{
    public abstract class GameMember
    {
        protected string name="";
        public Dictionary<GameMember, double> Opinions;

        public string Name
        {
            get
            {
                return name;
            }
        }

        protected static double adjust(double value)
        {
            double result = Math.Atan(Math.Exp(0.06 * value)) * 1.27 - 1;

            if (result < -1)
                return -1;

            if(result > 1)
                return 1;

            return result;
        }

        public abstract void Turn();
    }
}