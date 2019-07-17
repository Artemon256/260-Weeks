using System.Collections.Generic;

namespace The260WeeksGame
{
    public class MassMediaUnit : GameMember
    { 
        
        public class Campaign
        {
            public enum CampaignMode
            {
                Against,
                Pro
            }
            public GameMember Target;
            public int TurnsLeft;
            public CampaignMode Mode;

            public Campaign(GameMember target, int duration, CampaignMode mode)
            {
                Target = target;
                TurnsLeft = duration;
                Mode = mode;
            }
        }

        private List<Campaign> campaigns;

        private Businessman owner;
        public double passiveInfluence = 1;
        public double campaignInfluence = 1;

        public Businessman Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        public MassMediaUnit(string name) : base(name)
        {
            campaigns = new List<Campaign>();
        }

        public bool AddCampaign(GameMember target, int duration, Campaign.CampaignMode mode)
        {
            if (Owner.ServicePoint < duration)
                return false;
            Owner.ServicePoint -= duration;

            campaigns.Add(new Campaign(target, duration, mode));

            return true;
        }
        
        private void actCampaigns()
        {
            foreach (Campaign campaign in campaigns)
            {
                int modifier = 0;

                if (campaign.Mode == Campaign.CampaignMode.Against)
                    modifier = -1;
                if (campaign.Mode == Campaign.CampaignMode.Pro)
                    modifier = 1;

                foreach (SocialGroup group in GameCore.getInstance().SocialGroups)
                    group.RevaluateOpinion(this, campaign.Target, modifier * campaignInfluence);
                campaign.TurnsLeft--;
            }

            campaigns.RemoveAll(item => item.TurnsLeft == 0);
        }

        private void actPresident() {
            foreach (SocialGroup group in GameCore.getInstance().SocialGroups)
                group.RevaluateOpinion(this, GameCore.getInstance().Player, passiveInfluence * Adjust(Owner.Opinions[GameCore.getInstance().Player]));
        }

        public override void Turn()
        {
            base.Turn();
            actCampaigns();
            actPresident();
        }

        public static MassMediaUnit GenerateRandom() // TODO: Remove and make another function which will base on the game difficulty
        {
            MassMediaUnit result = new MassMediaUnit(GameCore.getInstance().RandomObjectFromList(GameStringManager.getInstance().MediaNames));
            GameStringManager.getInstance().MediaNames.Remove(result.name);
            return result;
        }

        public override void GenerateOpinions() {
            
        }

        public override void RevaluateOpinion(GameMember sender, GameMember target, double delta) {
            
        }
    }
}
