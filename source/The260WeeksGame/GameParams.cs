using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    public class GameParams
    {
        public int NumberOfBusinessmen;
        public int NumberOfMassMedia;
        public GameCore.Difficulty Difficulty;

        public GameParams(int numberOfBusinessmen, int numberOfMassMedia, GameCore.Difficulty difficulty)
        {
            NumberOfBusinessmen = numberOfBusinessmen;
            NumberOfMassMedia = numberOfMassMedia;
            Difficulty = difficulty;
        }
    }
}
