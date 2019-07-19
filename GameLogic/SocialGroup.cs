using System;
using System.Xml;

namespace _260Weeks.GameLogic
{
    public class SocialGroup : Member
    {
        public SocialGroup(string name) : base(name)
        {
        }

        public override void Turn()
        {

        }

        public override void InitOpinions()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(StringManager.SocialGroups);

            foreach (XmlNode node in document.ChildNodes)
                Console.WriteLine(node);
        }
    }
}