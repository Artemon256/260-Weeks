using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace The260WeeksGame
{
    public class GameStringManager
    {
        private readonly static string FirstNamesFile = "First Names.txt";
        private readonly static string SecondNamesFile = "Second Names.txt";
        private readonly static string MediaNamesFile = "Media Names.txt";
        private readonly static string SocialGroupNamesFile = "Social Groups.xml";

        public List<string> FirstNames;
        public List<string> SecondNames;
        public List<string> MediaNames;

        public string SocialGroups;

        private Assembly assembly;

        private void ReadIntoList(List<string> receiver, string fileName)
        {
            string resourceName = assembly.GetManifestResourceNames()
                                  .Single(str => str.EndsWith(fileName));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    receiver.Add(reader.ReadLine());
                }
            }
        }

        private void ReadIntoString(ref string receiver, string fileName)
        {
            string resourceName = assembly.GetManifestResourceNames()
                                  .Single(str => str.EndsWith(fileName));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                receiver = reader.ReadToEnd();
            }
        }

        private GameStringManager()
        {
            FirstNames = new List<string>();
            SecondNames = new List<string>();
            MediaNames = new List<string>();

            assembly = Assembly.GetExecutingAssembly();

            ReadIntoList(FirstNames, FirstNamesFile);
            ReadIntoList(SecondNames, SecondNamesFile);
            ReadIntoList(MediaNames, MediaNamesFile);
            ReadIntoString(ref SocialGroups, SocialGroupNamesFile);
        }

        private static GameStringManager instance;
        public static GameStringManager getInstance()
        {
            if (instance == null)
                instance = new GameStringManager();
            return instance;
        }
    }
}
