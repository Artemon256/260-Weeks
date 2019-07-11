using System;

namespace The260WeeksGame
{
    public abstract class GameMember
    {
        protected string name="";

        public string Name
        {
            get
            {
                return name;
            }
        }

        public double AbsoluteRating = 0;
        public double AdjustedRating
        {
            get
            {
                return adjust((double) AbsoluteRating);
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