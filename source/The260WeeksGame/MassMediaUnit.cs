using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class MassMediaUnit : GameMember
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
        private string name = "";
        private int politicalInfluence;


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
        public string Name
        {
            get
            {
                return name;
            }
        }
        public int PoliticalInfluence
        {
            get
            {
                return politicalInfluence;
            }
            set
            {
                politicalInfluence = value;
            }
        }

        public MassMediaUnit(string name, double absoluteRating)
        {
            this.name = name;

            AbsoluteRating = absoluteRating;
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
            for(int i = 0; i < campaigns.Count; i++)
            {
                int modifier = 0;

                if (campaigns[i].Mode == Campaign.CampaignMode.Against)
                    modifier = -1;
                if (campaigns[i].Mode == Campaign.CampaignMode.Pro)
                    modifier = 1;

                double random = GameCore.random.NextDouble();

                campaigns[i].Target.AbsoluteRating += Math.Ceiling(modifier * AbsoluteRating * random * 0.2); // TODO: 0.2 coefficient must be bind to the game difficulty
                campaigns[i].TurnsLeft--;
            }

            campaigns.RemoveAll(item => item.TurnsLeft == 0);
        }

        private void actPresident() {
            double random = GameCore.random.NextDouble();
            GameCore.Player.AbsoluteRating += Math.Round(AbsoluteRating * Owner.AdjustedLoyalty * random * 0.5); // TODO: 0.5 coefficient must be bind to the game difficulty
        }

        public override void Turn() {
            actCampaigns();
            actPresident();
        }

        public static MassMediaUnit GenerateRandom() // TODO: Remove and make another function which will base on the game difficulty
        {
            MassMediaUnit result = new MassMediaUnit(GameCore.RandomObjectFromStringList(GameCore.MediaNameList), GameCore.random.Next(0, 30));
            GameCore.MediaNameList.Remove(result.name);
            return result;
        }
    }
}
