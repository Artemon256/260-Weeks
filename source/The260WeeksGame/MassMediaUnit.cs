﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            foreach (var campaign in campaigns)
            {
                int modifier = 0;

                if (campaign.Mode == Campaign.CampaignMode.Against)
                    modifier = -1;
                if (campaign.Mode == Campaign.CampaignMode.Pro)
                    modifier = 1;

                double random = GameCore.RandomGenerator.NextDouble();

                foreach (var group in GameCore.getInstance().SocialGroups)
                    group.Opinions[campaign.Target] += modifier * random * group.Opinions[this];
                campaign.TurnsLeft--;
            }

            campaigns.RemoveAll(item => item.TurnsLeft == 0);
        }

        private void actPresident() {
            double random = GameCore.RandomGenerator.NextDouble();
            foreach (var group in GameCore.getInstance().SocialGroups)
                group.Opinions[GameCore.getInstance().Player] += Owner.Opinions[GameCore.getInstance().Player] * random * group.Opinions[this];
        }

        public override void Turn() {
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
    }
}
