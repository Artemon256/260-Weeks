namespace _260Weeks.GameLogic
{
    public class Businessman : Member
    {
        public Businessman(string name) : base(name)
        {

        }

        protected override void initOpinions()
        {
            foreach (Member subject in Core.getInstance().Members)
                Opinions[subject] = Utils.RandomDouble(-1, 1);
        }

        public override void Turn()
        {

        }
    }
}