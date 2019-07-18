using System;

namespace _260Weeks.GameLogic
{
    public static class Utils
    {
        private static Random random = new Random();
        public static double Map(double value, double inMin, double inMax, double outMin, double outMax)
        {
            return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        }
        public static double Constrain(double value, double min, double max)
        {
            if (value <= min)
                return min;
            if (value >= max)
                return max;
            return value;
        }
        public static int RandomInt()
        {
            return random.Next();
        }
        public static int RandomInt(int max)
        {
            return random.Next(max);
        }
        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }
        public static double RandomDouble()
        {
            return random.NextDouble();
        }
        public static double RandomDouble(double max)
        {
            return random.NextDouble() * max;
        }
        public static double RandomDouble(double min, double max)
        {
            return Map(random.NextDouble(), 0, 1, min, max);
        }
    }
}