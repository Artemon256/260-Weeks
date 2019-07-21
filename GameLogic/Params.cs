using System;

namespace _260Weeks.GameLogic
{
    public static class Params
    {
        private static double SigmoidMaxParameter = 10;

        public static uint
            MaxTurns = 260,
            MaxCampaignsPerUnit = 2,
            DefaultPresidentServicePoints = 20;

        public static string
            DefaultPresidentName = "Mr. President";

        public static double
            DefaultFlexibility = 0.05,
            ChanceBias = 0.1,
            OpinionThreshold = 0.3,
            OpinionInitRange = 0.3;

        public static double SigmoidStretchingParameter = (-Math.Log(2 / (1.99) - 1)) / SigmoidMaxParameter;
    }
}