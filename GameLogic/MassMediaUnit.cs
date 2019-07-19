using System;
using System.Collections.Generic;

namespace _260Weeks.GameLogic
{
    public class MassMediaUnit : Member
    {
        public enum CampaignMode
        {
            Against = -1,
            Pro = 1
        }

        public class Campaign
        {
            public double Delta;

            public MassMediaUnit Media;

            public Member Subject;

            public uint TurnsLeft;

            public Campaign(MassMediaUnit media, Member subject, double delta, uint duration)
            {
                Media = media;
                Subject = subject;
                Delta = delta;
                TurnsLeft = duration;
            }

            public Campaign(MassMediaUnit media, Member subject, CampaignMode mode, uint duration) : this(media, subject, (double)mode, duration)
            {
            }

            public void Act()
            {
                foreach (Member target in Core.getInstance().Members)
                    target.AffectOpinion(Media, Subject, Delta);
                TurnsLeft--;
            }
        }

        public Businessman Owner;

        private List<Campaign> activeCampaigns;

        private Campaign presidentCampaign;

        public MassMediaUnit(string name) : base(name)
        {
            activeCampaigns = new List<Campaign>();
            presidentCampaign = new Campaign(this, Core.getInstance().Player, 0d, Params.MaxTurns);
        }

        public override void Turn()
        {
            if (Owner == null)
                throw (new NullReferenceException($"Mass media' owner is null ({Name})"));
            foreach (Campaign campaign in activeCampaigns)
                campaign.Act();
            presidentCampaign.Delta = Owner.Opinions[Core.getInstance().Player];
            presidentCampaign.Act();
        }

        public override void InitOpinions()
        {
            // Mass media do not have opinions because are owned by businessmen
        }

        public static MassMediaUnit GenerateRandom()
        {
            MassMediaUnit result = new MassMediaUnit(Utils.RandomFromList(StringManager.MassMediaTitles));
            result.Owner = Utils.RandomFromList(Core.getInstance().Businessmen);
            return result;
        }
    }
}