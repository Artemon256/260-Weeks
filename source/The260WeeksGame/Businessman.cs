using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    public class Businessman : GameMember
    {

        public int ServicePoint;

        public Businessman(string name, int servicePoint) : base(name)
        {
            ServicePoint = servicePoint;
        }

        public override void GenerateOpinions()
        {
            foreach (var subject in GameCore.getInstance().Members)
            {
                switch (GameParams.getInstance().Difficulty)
                {
                    case GameParams.DifficultyLevel.Easy:
                        Opinions[subject] = Unadjust(GameCore.RandomDouble(-0.2, 1));
                        break;
                    case GameParams.DifficultyLevel.Moderate:
                }
            }
        }

        public override void Turn() {
            Opinions[GameCore.getInstance().Player] += (int) Math.Round(ServicePoint * 0.2);
            ServicePoint = (int) Math.Round(ServicePoint * 0.8);
        }

        public static Businessman GenerateRandom()
        {
            GameCore core = GameCore.getInstance();
            var firstName = core.RandomObjectFromList(GameStringManager.getInstance().FirstNames);
            var secondName = core.RandomObjectFromList(GameStringManager.getInstance().SecondNames); 

            var fullName = firstName + " " + secondName;

            Businessman result = new Businessman(fullName, 50); // Service points on start are equal to 50 until we develop ways to earn them
            
            GameStringManager.getInstance().SecondNames.Remove(secondName);
            return result;
        }
    }
}
