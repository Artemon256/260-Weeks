using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class GameCore
    {
        private bool gameOn; // ???

        public List<GameMember> Members;

        public static President Player;
        public static Random random = new Random();
        public static List<string> FirstNameList = new List<string>(); // TODO: REFACTOR THIS SHIT
        public static List<string> SecondNameList = new List<string>();
        public static List<string> MediaNameList = new List<string>();

        public List<Businessman> Businessmen
        {
            get
            {
                var result = new List<Businessman>();
                foreach (var member in Members)
                    if (member is Businessman)
                        result.Add(member as Businessman);
                return result;
            }
        }
        public List<MassMediaUnit> MassMedia
        {
            get
            {
                var result = new List<MassMediaUnit>();
                foreach (var member in Members)
                    if (member is MassMediaUnit)
                        result.Add(member as MassMediaUnit);
                return result;
            }
        }

        public bool GameOn() // ???
        {
            return gameOn;
        }

        public GameCore()
        {
            Members = new List<GameMember>();

            Player = new President();

            if(FirstNameList.Count == 0)
            {
                InitFirstNameList();
            }
            if(SecondNameList.Count == 0)
            {
                InitSecondNameList();
            }
            if(MediaNameList.Count == 0)
            {
                InitMediaNameList();
            }
        }

        public void StartGame()
        {
            gameOn = true;

            var businessmen = new List<Businessman>();
            var massMedia = new List<MassMediaUnit>();

            int numberOfBusinessmen = random.Next(1, 5); // TODO: A sensible way to define these numbers
            int numberOfMassMedia = random.Next(1, 10);

            for(int i = 0; i < numberOfBusinessmen; i++)
            {
                businessmen.Add(Businessman.GenerateRandom());
            }

            for(int i = 0; i < numberOfMassMedia; i++)
            {
                massMedia.Add(MassMediaUnit.GenerateRandom());
            }

            for(int i = 0; i < numberOfBusinessmen; i++) // Assign an owner to a MassMediaUnit
            {
                for(int j = 0; j < numberOfMassMedia; j++)
                {
                    if(massMedia[j].Owner == null)
                    {
                        if (random.Next(2) == 0 || i == numberOfBusinessmen - 1)
                        {
                            massMedia[j].Owner = businessmen[i];
                        }
                    }
                }
            }

            Members.AddRange(businessmen);
            Members.AddRange(massMedia);
            
            Members.Add(Player); // Player ALWAYS moves AFTER other members
        }
        public void NextTurn()
        {
            foreach (var member in Members)
                member.Turn();
        }

        public static string RandomObjectFromStringList(List<string> from)
        {
            if (from.Count == 0)
                return null;
            return from[random.Next(0, from.Count)];
        }

        // TODO: REFACTOR THAT SHIT
        private void InitFirstNameList()
        {
            FirstNameList.Add("John");
            FirstNameList.Add("Vasily");
            FirstNameList.Add("Ihor");
            FirstNameList.Add("Abraham");
        }

        private void InitSecondNameList()
        {
            SecondNameList.Add("Miller");
            SecondNameList.Add("Simpson");
            SecondNameList.Add("Galushka");
        }

        private void InitMediaNameList()
        {
            MediaNameList.Add("Inter");
            MediaNameList.Add("Pohoda FM");
            MediaNameList.Add("Zhopa UA");
        }
    }
}
