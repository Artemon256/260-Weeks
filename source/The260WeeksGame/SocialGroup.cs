using System.Collections.Generic;

namespace The260WeeksGame
{
    public class SocialGroup : GameMember
    {
        public override void Turn() {
            
        }

        private static List<SocialGroup> socialGroups = null;
        public static List<SocialGroup> getSocialGroups() {
            if (socialGroups == null) {
                socialGroups = new List<SocialGroup>();
            }
            return socialGroups;
        }
    }
}