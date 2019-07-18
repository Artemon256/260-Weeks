using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace _260Weeks.GameLogic
{
    public static class StringManager
    {
        private static string firstNamesResource = "_260Weeks.GameLogic.Strings.FirstNames.res";
        private static string lastNamesResource = "_260Weeks.GameLogic.Strings.LastNames.res";
        private static string massMediaTitleResource = "_260Weeks.GameLogic.Strings.MassMediaTitles.res";

        public static List<string> FirstNames, LastNames, MassMediaTitles;

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
        }
    }
}