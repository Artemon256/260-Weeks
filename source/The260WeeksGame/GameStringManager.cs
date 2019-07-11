using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace The260WeeksGame
{
    class GameStringManager
    {
        private readonly string FirstNamesFile = @"First Names.txt";
        private readonly string SecondNamesFile = @"Second Names.txt";
        private readonly string MediaNamesFile = @"Media Names.txt";

        public List<string> FirstNames;
        public List<string> SecondNames;
        public List<string> MediaNames;

        private Assembly assembly;

        private void ReadResource(List<string> receiver, string fileName)
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

        public GameStringManager()
        {
            FirstNames = new List<string>();
            SecondNames = new List<string>();
            MediaNames = new List<string>();

            assembly = Assembly.GetExecutingAssembly();

            ReadResource(FirstNames, FirstNamesFile);
            ReadResource(SecondNames, SecondNamesFile);
            ReadResource(MediaNames, MediaNamesFile);
        }
    }
}
