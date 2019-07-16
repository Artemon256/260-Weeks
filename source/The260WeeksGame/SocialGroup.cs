using System;
using System.Xml;
using System.Collections.Generic;

namespace The260WeeksGame
{
    public class SocialGroup : GameMember
    {
        public int Population;
        public bool VoteEligible;
        public double OverallMassMediaOpinion, OverallBusinessmenOpinion;

        public SocialGroup(string name) : base(name)
        {

        }

        public override void Turn()
        {

        }

        public override void RevaluateOpinion(GameMember sender, GameMember target, double delta)
        {
            if (this == target)
                return;

            double senderRating;
            if (!Opinions.TryGetValue(sender, out senderRating))
                return;

            senderRating = Adjust(senderRating);

            if (senderRating < 0) // If this social group doesn't like this media
            {
                if (GameCore.RandomGenerator.Next(0, 2) == 0) // Don't change opinion with a chance of 50%
                    return;
            }

            senderRating += 2; // The best way to keep using negative rating so that influence still proportional on rating 

            double random = GameCore.RandomDouble(0, 0.2);
            Opinions[target] += delta * senderRating * random;

            Opinions[target] = ConstraintOpinion(Opinions[target]);
        }

        public override void GenerateOpinions()
        {
            foreach (GameMember subject in GameCore.getInstance().Members)
            {
                if (subject is SocialGroup || subject is President)
                    continue; // Social groups have predefined opinions about other groups and president
                if (subject is Businessman)
                    Opinions[subject] = OverallBusinessmenOpinion + subject.Opinions[this];
                if (subject is MassMediaUnit)
                    Opinions[subject] = OverallMassMediaOpinion + Opinions[(subject as MassMediaUnit).Owner];
            }
        }

        private static void ParseOpinions(XmlNode opinionXML, SocialGroup group, Dictionary<string, SocialGroup> groups)
        {
            string subject = opinionXML.Attributes.GetNamedItem("subject").Value;
            double value = Convert.ToDouble(opinionXML.InnerText);
            switch (subject)
            {
                case "Mass Media":
                    group.OverallMassMediaOpinion = value;
                    break;
                case "Businessmen":
                    group.OverallBusinessmenOpinion = value;
                    break;
                case "President":
                    group.Opinions.Add(GameCore.getInstance().Player, value);
                    break;
                default:
                    group.Opinions.Add(groups[subject], value);
                    break;
            }
        }

        private static void ParsePopulation(XmlNode populationXML, SocialGroup group)
        {
            group.Population = Convert.ToInt32(populationXML.InnerText);
        }

        private static void ParseVoteEligible(XmlNode voteEligibleXML, SocialGroup group)
        {
            group.VoteEligible = Convert.ToBoolean(voteEligibleXML.InnerText);
        }

        private static List<SocialGroup> socialGroups = null;
        public static List<SocialGroup> getSocialGroups()
        {
            if (socialGroups == null)
            {
                socialGroups = new List<SocialGroup>();

                XmlDocument parameters = new XmlDocument();

                parameters.LoadXml(GameStringManager.getInstance().SocialGroups);

                XmlNode groupsXML = parameters.GetElementsByTagName("groups")[0];

                Dictionary<string, SocialGroup> groups = new Dictionary<string, SocialGroup>();

                foreach (XmlNode groupXML in groupsXML.ChildNodes)
                {
                    if (groupXML.Name != "group")
                        continue;

                    string name = groupXML.Attributes.GetNamedItem("name").Value;

                    groups.Add(name, new SocialGroup(name));
                }

                foreach (XmlNode groupXML in groupsXML.ChildNodes)
                {
                    if (groupXML.Name != "group")
                        continue;

                    string name = groupXML.Attributes.GetNamedItem("name").Value;

                    SocialGroup group = groups[name];

                    foreach (XmlNode infoXML in groupXML.ChildNodes)
                    {
                        if (infoXML is XmlComment)
                            continue;

                        switch (infoXML.Name)
                        {
                            case "opinion":
                                ParseOpinions(infoXML, group, groups);
                                break;
                            case "population":
                                ParsePopulation(infoXML, group);
                                break;
                            case "vote_eligible":
                                ParseVoteEligible(infoXML, group);
                                break;
                        }

                    }
                }

                foreach (KeyValuePair<string, SocialGroup> entry in groups)
                    socialGroups.Add(entry.Value);
            }
            return socialGroups;
        }
    }
}