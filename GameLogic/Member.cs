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

        protected double flexibility = 0.05;

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
            double dummy;
            if (!Opinions.TryGetValue(subject, out dummy) || !Opinions.TryGetValue(sender, out dummy))
                return;
            double trust = Utils.RandomDouble();
            tempOpinions[subject] += Opinions[sender] * delta * flexibility * trust;
            tempOpinions[subject] = Utils.Constrain(tempOpinions[subject], -1, 1);
        }

        public void AffectOthers()
        {
            if (this is President || this is MassMediaUnit)
                return;
            foreach (KeyValuePair<Member, double> targetEntry in Opinions)
                foreach (KeyValuePair<Member, double> subjectEntry in Opinions)
                {
                    if (targetEntry.Key == this || subjectEntry.Key == this || targetEntry.Key == subjectEntry.Key)
                        continue;
                    targetEntry.Key.AffectOpinion(this, subjectEntry.Key, subjectEntry.Value);
                }
        }

        public abstract void InitOpinions();

        public abstract void Turn();
    }
}