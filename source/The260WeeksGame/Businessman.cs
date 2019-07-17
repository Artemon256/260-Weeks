using System;

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
            foreach (GameMember subject in GameCore.getInstance().Members)
            {
                if (subject == this)
                    continue;
                if (subject is President)
                    switch (GameParams.getInstance().Difficulty)
                    {
                        case GameParams.DifficultyLevel.Easy:
                            Opinions[subject] = Unadjust(GameCore.RandomDouble(-0.1, 1));
                            break;
                        case GameParams.DifficultyLevel.Moderate:
                            Opinions[subject] = Unadjust(GameCore.RandomDouble(-0.3, 0.7));
                            break;
                        case GameParams.DifficultyLevel.Medium:
                            Opinions[subject] = Unadjust(GameCore.RandomDouble(-0.5, 0.5));
                            break;
                        case GameParams.DifficultyLevel.Hard:
                            Opinions[subject] = Unadjust(GameCore.RandomDouble(-0.7, 0.3));
                            break;
                        case GameParams.DifficultyLevel.Nightmare:
                            Opinions[subject] = Unadjust(GameCore.RandomDouble(-1, 0.1));
                            break;
                    }
                else
                    Opinions[subject] = Unadjust(GameCore.RandomDouble(-1, 1));
            }
        }

        public override void Turn()
        {
            base.Turn();

            int transferedPoint = (int)Math.Round(ServicePoint * 0.2);

            Opinions[GameCore.getInstance().Player] += transferedPoint;
            Opinions[GameCore.getInstance().Player] = ConstrainOpinion(Opinions[GameCore.getInstance().Player]);

            ServicePoint -= transferedPoint;
        }

        public static Businessman GenerateRandom()
        {
            GameCore core = GameCore.getInstance();
            string firstName = core.RandomObjectFromList(GameStringManager.getInstance().FirstNames);
            string secondName = core.RandomObjectFromList(GameStringManager.getInstance().SecondNames); 

            string fullName = firstName + " " + secondName;

            Businessman result = new Businessman(fullName, 50); // Service points on start are equal to 50 until we develop ways to earn them
            
            GameStringManager.getInstance().SecondNames.Remove(secondName);
            return result;
        }

        public override void RevaluateOpinion(GameMember sender, GameMember target, double delta) {
            
        }
    }
}
