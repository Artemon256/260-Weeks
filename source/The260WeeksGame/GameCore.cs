using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class GameCore
    {
        private bool gameOn; // ???

        public static Random random = new Random();
        public List<GameMember> Members = new List<GameMember>();
        public static President Player = new President();
        public static Difficulty GameDifficulty;

        public enum Difficulty {
            Easy,
            Moderate,
            Medium,
            Complicated,
            Hard
        }

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

        public GameCore(Difficulty difficulty)
        {
            GameDifficulty = difficulty;

            FirstNameList = new List<string>(gameStringManager.FirstNames);
            SecondNameList = new List<string>(gameStringManager.SecondNames);
            MediaNameList = new List<string>(gameStringManager.MediaNames);
        }

        public void StartGame()
        {
            gameOn = true;

            var businessmen = new List<Businessman>();
            var massMedia = new List<MassMediaUnit>();

            int numberOfBusinessmen = random.Next(1, 5); // TODO: A sensible way to define these numbers
            int numberOfMassMedia = random.Next(1, 10);

            for (int i = 0; i < numberOfBusinessmen; i++)
            {
                businessmen.Add(Businessman.GenerateRandom());
            }

            for (int i = 0; i < numberOfMassMedia; i++)
            {
                massMedia.Add(MassMediaUnit.GenerateRandom());
            }

            var businessmen_copy = new List<Businessman>(businessmen);
            foreach (var unit in massMedia)
            {
                unit.Owner = businessmen_copy[random.Next(0, businessmen_copy.Count)];
                businessmen_copy.Remove(unit.Owner);
            }

            Members.AddRange(businessmen);
            Members.AddRange(massMedia);
            
           // Members.Add(Player); // Player ALWAYS moves AFTER other members
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

        public static GameStringManager gameStringManager = new GameStringManager();
    }
}
