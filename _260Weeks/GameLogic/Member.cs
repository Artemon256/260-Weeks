using System.Collections.Generic;

namespace _260Weeks.GameLogic
{
    public abstract class Member
    {
        public string Name
        {
            get
            {
                return name;
            }
        }

        public uint ID
        {
            get
            {
                return id;
            }
        }

        public Dictionary<Member, double> Opinions;

        private Dictionary<Member, double> tempOpinions;

        private string name;

        private uint id;

        public Member(string name)
        {
            this.name = name;
            this.id = Core.IDManager.ID;
            Opinions = new Dictionary<Member, double>();
        }

        public void Commit()
        {
            Opinions = new Dictionary<Member, double>(tempOpinions);
        }

        public void Rollback()
        {
            tempOpinions = new Dictionary<Member, double>(Opinions);
        }

        public void AffectOpinion(Member sender, Member subject, double delta)
        {
            if (this is President || this is MassMediaUnit)
                return;
            if (subject == this || subject == sender)
                return;
            tempOpinions[subject] += Opinions[sender] * delta;
        }

        public void AffectOthers()
        {
            if (this is President || this is MassMediaUnit)
                return;
            foreach (Member target in Core.getInstance().Members)
                foreach (Member subject in Core.getInstance().Members)
                    target.AffectOpinion(this, subject, Opinions[subject]);
        }

        public abstract void InitOpinions();

        public abstract void Turn();
    }
}