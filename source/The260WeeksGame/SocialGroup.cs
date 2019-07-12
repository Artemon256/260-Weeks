using System;
using System.Xml;
using System.Collections.Generic;

namespace The260WeeksGame
{
    public class SocialGroup : GameMember
    {
        public double OverallMassMediaOpinion, OverallBusinessmenOpinion;

        public SocialGroup(string name) : base(name) {

        }

        public override void Turn() {
            
        }

        public override void GenerateOpinions() {
            foreach (var subject in GameCore.getInstance().Members)
            {
                var random = Unadjust(GameCore.RandomGenerator.NextDouble() * 2 - 1);
                if (subject is SocialGroup || subject is President)
                    continue; // Social groups have predefined opinions about other groups and president
                if (subject is Businessman)
                    Opinions[subject] = OverallBusinessmenOpinion + subject.Opinions[this] + random;
                if (subject is MassMediaUnit)
                    Opinions[subject] = OverallMassMediaOpinion + (subject as MassMediaUnit).Owner.Opinions[this] + random;
            }
        }

        private static List<SocialGroup> socialGroups = null;
        public static List<SocialGroup> getSocialGroups() {
            if (socialGroups == null) {
                socialGroups = new List<SocialGroup>();
                
                var parameters = new XmlDocument();
                
                parameters.LoadXml(GameStringManager.getInstance().SocialGroups);

                var groupsXML = parameters.GetElementsByTagName("groups")[0];

                var groups = new Dictionary<string, SocialGroup>();
                
                foreach (XmlNode groupXML in groupsXML.ChildNodes) {
                    if (groupXML.Name != "group")
                        continue;

                    var name = groupXML.Attributes.GetNamedItem("name").Value;
    
                    groups.Add(name, new SocialGroup(name));
                }

                foreach (XmlNode groupXML in groupsXML.ChildNodes) {
                    if (groupXML.Name != "group")
                        continue;

                    var name = groupXML.Attributes.GetNamedItem("name").Value;

                    var group = groups[name];

                    foreach (XmlNode opinionXML in groupXML.ChildNodes) {
                        if (opinionXML is XmlComment)
                            continue;
                        var subject = opinionXML.Attributes.GetNamedItem("subject").Value;
                        var value = Convert.ToDouble(opinionXML.InnerText);
                        switch (subject) {
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
                }

                foreach (KeyValuePair<string, SocialGroup> entry in groups)
                    socialGroups.Add(entry.Value);
            }
            return socialGroups;
        }
    }
}