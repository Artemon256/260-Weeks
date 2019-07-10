using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class MassMediaUnit : IPubliclyExposed
    { 
        class Campaign
        {
            public IPubliclyExposed Target;
            public int TurnsLeft;
            public bool Against;

            public Campaign(IPubliclyExposed target, int turnsLength, bool against)
            {
                Target = target;
                TurnsLeft = turnsLength;
                Against = against;
            }
        }
        private List<Campaign> campaigns;

        private Businessman owner;
        private string name = "";
        private int politicalInfluence;
        private double publicPopularity;



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
        public double PublicPopularity
        {
            get
            {
                return publicPopularity;
            }
            set
            {
                publicPopularity = value;
            }
        }

        public MassMediaUnit(string name, int politicalInfluence, double publicPopularity)
        {
            this.name = name;

            PoliticalInfluence = politicalInfluence;
            PublicPopularity = publicPopularity;
            campaigns = new List<Campaign>();

        }

        public bool AddCampaign(IPubliclyExposed target, int turnsLength, bool against)
        {
            if (Owner.ServicePoint < turnsLength)
                return false;
            Owner.ServicePoint -= turnsLength;

            campaigns.Add(new Campaign(target, turnsLength, against));

            return true;
        }
        
        public void ActCampaigns()
        {
            for(int i = 0; i < campaigns.Count; i++)
            {
                int modif;

                if (campaigns[i].Against)
                    modif = -1;
                else
                    modif = 1;

                campaigns[i].Target.PublicPopularity += modif * politicalInfluence;
                campaigns[i].TurnsLeft--;
            }

            campaigns.RemoveAll(item => item.TurnsLeft == 0);
        }

        public static MassMediaUnit GenerateRandom()
        {
            MassMediaUnit result = new MassMediaUnit(GameCore.RandomObjectFromStringList(GameCore.MediaNameList),
                                                    GameCore.random.Next(0, 100), GameCore.random.Next(-10, 100));
            return result;
        }
    }
}
