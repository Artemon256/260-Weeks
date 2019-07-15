using System;
using System.Collections.Generic;

namespace The260WeeksGame
{
    public class GameCore
    {
        private bool gameOn; // ???
        private int currentTurn; 
        public List<GameMember> Members;
        public President Player;
        public static Random RandomGenerator = new Random();
       

        public List<Businessman> Businessmen
        {
            get
            {
                List<Businessman> result = new List<Businessman>();
                foreach (GameMember member in Members)
                    if (member is Businessman)
                        result.Add(member as Businessman);
                return result;
            }
        }
        public List<MassMediaUnit> MassMedia
        {
            get
            {
                List<MassMediaUnit> result = new List<MassMediaUnit>();
                foreach (GameMember member in Members)
                    if (member is MassMediaUnit)
                        result.Add(member as MassMediaUnit);
                return result;
            }
        }
        public List<SocialGroup> SocialGroups
        {
            get
            {
                List<SocialGroup> result = new List<SocialGroup>();
                foreach (GameMember member in Members)
                    if (member is SocialGroup)
                        result.Add(member as SocialGroup);
                return result;
            }
        }  
        public int CurrentTurn
        {
            get
            {
                return currentTurn;
            }
        }

        public int TotalPopulation
        {
            get
            {
                int result = 0;

                foreach(var group in SocialGroups)
                {
                    result += group.Population;
                }

                return result;
            }
        }

        public bool GameOn() // ???
        {
            return gameOn;
        }
        
        private GameCore() {
            Player = new President();
            Members = new List<GameMember>();
        }

        public void StartGame()
        {
            gameOn = true;
            currentTurn = 1;

            List<Businessman> businessmen = new List<Businessman>();
            List<MassMediaUnit> massMedia = new List<MassMediaUnit>();

            for (int i = 0; i < GameParams.instance.NumberOfBusinessmen; i++)
            {
                businessmen.Add(Businessman.GenerateRandom());
            }

            for (int i = 0; i < GameParams.instance.NumberOfMassMedia; i++)
            {
                massMedia.Add(MassMediaUnit.GenerateRandom());
            }

            foreach (MassMediaUnit media in massMedia)
            {
                media.Owner = RandomObjectFromList(businessmen);
            }

            // ORDER IS IMPORTANT, GROUPS DEFINE THEIR OPINIONS BASED ON OPINIONS OF BUSINESSMEN AND MASS MEDIA
            Members.AddRange(businessmen);
            Members.AddRange(massMedia);
            Members.AddRange(SocialGroup.getSocialGroups());
            Members.Add(Player); // Player ALWAYS moves AFTER other members

            foreach(Businessman businessman in businessmen)
            {
                businessman.GenerateOpinions();
            }
            
            foreach(SocialGroup group in SocialGroups)
            {
                group.GenerateOpinions();
            }
        }

        public void NextTurn()
        {
            currentTurn++;

            foreach (GameMember member in Members)
                member.Turn();
        }

        public static double RandomDouble(double min, double max) {
            return RandomGenerator.NextDouble() * (max - min) + min;
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
