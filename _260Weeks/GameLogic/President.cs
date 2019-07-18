namespace _260Weeks.GameLogic
{
    public class President : Member
    {
        public President() : base("Mr. President")
        {
        }

        public override void Turn()
        {
            Core.getInstance().Interface.Turn();
        }

        protected override void initOpinions()
        {
            // President does not have opinions because is controlled by a player
        }
    }
}