namespace _260Weeks.GameLogic
{
    public class Businessman : Member
    {
        public Businessman(string name) : base(name)
        {

        }

        public override void InitOpinions()
        {
            foreach (Member subject in Core.getInstance().Members)
                if (subject != this)
                    Opinions[subject] = Utils.RandomDouble(-1, 1);
        }

        public override void Turn()
        {

        }

        public static Businessman GenerateRandom()
        {
            return new Businessman("no name");
        }
    }
}