namespace _260Weeks.GameLogic
{
    public class Businessman : Member
    {
        public double PresidentServicePoints = Params.DefaultPresidentServicePoints;

        public Businessman(string name) : base(name)
        {

        }

        public override void InitOpinions()
        {
            foreach (Member subject in Core.getInstance().Members)
                if (subject != this)
                    Opinions[subject] = Utils.RandomDouble(-Params.OpinionInitRange, Params.OpinionInitRange);
        }

        private void servicePointsToFriendship()
        {
            double random = Utils.RandomDouble();
            double value = Utils.Sigmoid(PresidentServicePoints) * flexibility * random;
            Opinions[Core.getInstance().Player] += value;
            Opinions[Core.getInstance().Player] = Utils.Constrain(Opinions[Core.getInstance().Player], -1, 1);
            PresidentServicePoints -= Utils.Unsigmoid(value);
        }

        public override void Turn()
        {
            AffectOthers();
            servicePointsToFriendship();
        }

        public static Businessman GenerateRandom()
        {
            return new Businessman(Utils.RandomFromList(StringManager.FirstNames) + " " + Utils.RandomFromList(StringManager.LastNames));
        }
    }
}