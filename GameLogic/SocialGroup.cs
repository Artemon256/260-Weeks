using System.Collections.Generic;
using System.Xml;

namespace _260Weeks.GameLogic
{
    public class SocialGroup : Member
    {
        public double MassMediaBias = 0, BusinessmenBias = 0;

        public bool VoteEligible = false;

        public uint Population;

        public SocialGroup(string name) : base(name) { }

        public override void Turn()
        {

        }

        public override void InitOpinions()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(StringManager.SocialGroups);

            XmlNode root = null;
            foreach (XmlNode node in document.ChildNodes)
                if (node.Name == "groups")
                    root = node;
            if (root == null)
                throw (new XmlException("Malformed SocialGroups.xml resource"));

            XmlNode groupNode = null;
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node is XmlComment || node is XmlDeclaration)
                    continue;
                if (node.Attributes.GetNamedItem("name").Value == Name)
                {
                    groupNode = node;
                    break;
                }
            }

            if (groupNode == null)
                throw (new XmlException("Malformed SocialGroups.xml resource"));

            foreach (XmlNode node in groupNode.ChildNodes)
            {
                if (node is XmlComment || node is XmlDeclaration)
                    continue;
                if (node.Name == "opinion")
                {
                    string name = node.Attributes.GetNamedItem("subject").Value;
                    double opinion = double.Parse(node.InnerText);

                    if (name == "Mass Media")
                    {
                        MassMediaBias = opinion;
                        continue;
                    }
                    if (name == "Businessmen")
                    {
                        BusinessmenBias = opinion;
                        continue;
                    }
                    if (name == "President")
                    {
                        Opinions[Core.getInstance().Player] = opinion;
                        continue;
                    }

                    SocialGroup subject = Core.getInstance().GetMemberByName(name) as SocialGroup;
                    Opinions[subject] = opinion;
                }
                if (node.Name == "vote_eligible")
                    VoteEligible = bool.Parse(node.InnerText);
                if (node.Name == "population")
                    Population = uint.Parse(node.InnerText);
            }

            foreach (Businessman businessman in Core.getInstance().Businessmen)
                Opinions[businessman] = BusinessmenBias + businessman.Opinions[this];

            foreach (MassMediaUnit media in Core.getInstance().MassMedia)
                Opinions[media] = BusinessmenBias + media.Owner.Opinions[this];
        }

        public static List<SocialGroup> LoadSocialGroups()
        {
            List<SocialGroup> result = new List<SocialGroup>();

            XmlDocument document = new XmlDocument();
            document.LoadXml(StringManager.SocialGroups);

            XmlNode root = null;
            foreach (XmlNode node in document.ChildNodes)
                if (node.Name == "groups")
                    root = node;
            if (root == null)
                throw (new XmlException("Malformed SocialGroups.xml resource"));

            foreach (XmlNode node in root.ChildNodes)
            {
                if (node is XmlComment || node is XmlDeclaration)
                    continue;
                result.Add(new SocialGroup(node.Attributes.GetNamedItem("name").Value));
            }

            return result;
        }
    }
}