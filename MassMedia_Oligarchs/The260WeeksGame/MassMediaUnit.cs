using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class MassMediaUnit : GameMember
    { 
        class Campaign
        {
            public GameMember Target;
            public int TurnsLeft;
            public bool Against;

            public Campaign(GameMember target, int turnsLength, bool against)
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

        public MassMediaUnit(string name, int politicalInfluence, double absoluteRating)
        {
            this.name = name;

            PoliticalInfluence = politicalInfluence;
            AbsoluteRating = absoluteRating;
            campaigns = new List<Campaign>();

        }

        public bool AddCampaign(GameMember target, int turnsLength, bool against)
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

                campaigns[i].Target.AbsoluteRating += modif * politicalInfluence;
                campaigns[i].TurnsLeft--;
            }

            campaigns.RemoveAll(item => item.TurnsLeft == 0);
        }

        public override void Turn() {

        }

        public static MassMediaUnit GenerateRandom()
        {
            MassMediaUnit result = new MassMediaUnit(GameCore.RandomObjectFromStringList(GameCore.MediaNameList),
                                                    GameCore.random.Next(0, 100), GameCore.random.Next(-10, 100));
            return result;
        }
    }
}
