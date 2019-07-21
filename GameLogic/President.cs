namespace _260Weeks.GameLogic
{
    public class President : Member
    {
        public President() : base(Params.DefaultPresidentName)
        {
        }

        public override void Turn()
        {
            Core.getInstance().Interface.PlayerTurn();
        }

        public override void InitOpinions()
        {
            // President does not have opinions because is controlled by a player
        }
    }
}