using System;
using System.Collections.Generic;

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
        public static T RandomFromList<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
                return default(T);
            return list[RandomInt(list.Count)];
        }
        public static double Sigmoid(double value)
        {
            const double stretchingConst = 0.35;
            return 2 / (1 + Math.Exp(-stretchingConst * value)) + 1;
        }
        public static double Unsigmoid(double value)
        {
            const double stretchingConst = 0.35;
            return Math.Log(2 / (value + 1) - 1) / -stretchingConst;
        }
    }
}