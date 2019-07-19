using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace _260Weeks.GameLogic
{
    public static class StringManager
    {
        private static string firstNamesResource = "_260Weeks.GameLogic.Strings.FirstNames.txt";
        private static string lastNamesResource = "_260Weeks.GameLogic.Strings.LastNames.txt";
        private static string massMediaTitleResource = "_260Weeks.GameLogic.Strings.MassMediaTitles.txt";
        private static string socialGroupsResource = "_260Weeks.GameLogic.Strings.SocialGroups.xml";

        public static List<string> FirstNames, LastNames, MassMediaTitles;
        public static string SocialGroups;

        private static List<string> readToList(string resource)
        {
            List<string> result = new List<string>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(resource);
            StreamReader reader = new StreamReader(stream);
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                result.Add(line);
            }
            return result;
        }

        private static string readToString(string resource)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(resource);
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        static StringManager()
        {
            FirstNames = readToList(firstNamesResource);
            LastNames = readToList(lastNamesResource);
            MassMediaTitles = readToList(massMediaTitleResource);
            SocialGroups = readToString(socialGroupsResource);
        }
    }
}