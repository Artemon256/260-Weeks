using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    public class GameCore
    {
        private bool gameOn; // ??? 
        public List<GameMember> Members;
        public President Player;
        public static Random RandomGenerator = new Random();

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
        
        private GameCore() {

        }

        public void StartGame()
        {
            gameOn = true;

            Members = new List<GameMember>();
            Player = new President();

            var businessmen = new List<Businessman>();
            var massMedia = new List<MassMediaUnit>();

            for (int i = 0; i < GameParams.instance.NumberOfBusinessmen; i++)
            {
                businessmen.Add(Businessman.GenerateRandom());
            }

            for (int i = 0; i < GameParams.instance.NumberOfMassMedia; i++)
            {
                massMedia.Add(MassMediaUnit.GenerateRandom());
            }

            foreach (var unit in massMedia)
            {
                unit.Owner = RandomObjectFromList(businessmen);
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

        public T RandomObjectFromList<T>(List<T> list)
        {
            if (list.Count == 0)
                return default(T);
            return list[RandomGenerator.Next(0, list.Count)];
        }

        private static GameCore instance = null;
        public static GameCore getInstance() {
            if (instance == null)
                instance = new GameCore();
            return instance;
        }
    }
}
