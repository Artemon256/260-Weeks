using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    public class CheatCodeParser
    {
        private GameCore game;

        private bool ParseOpinionCheatCode(string[] command)
        {
            if (command.Length != 4)
                return false;

            GameMember whose = game.GetGameMemberByName(command[1].Replace('_', ' '));
            GameMember aboutWhom = game.GetGameMemberByName(command[2].Replace('_', ' '));
            double value = Convert.ToDouble(command[3]);

            if (whose == null || aboutWhom == null)
                return false;

            whose.Opinions[aboutWhom] = GameMember.ConstraintOpinion(value);

            return true;
        }

        private bool ParseOwnerCheatCode(string[] command)
        {
            if (command.Length != 3)
                return false;

            GameMember mediaMember = game.GetGameMemberByName(command[1].Replace('_', ' '));
            GameMember ownerMember = game.GetGameMemberByName(command[2].Replace('_', ' '));

            if (mediaMember == null || ownerMember == null)
                return false;

            if (!(mediaMember is MassMediaUnit) || !(ownerMember is Businessman))
                return false;

            MassMediaUnit media = (mediaMember as MassMediaUnit);
            Businessman owner = (ownerMember as Businessman);

            media.Owner = owner;

            return true;
        }

        public bool ParseCheatCode(string command)
        {
            string[] items = command.Split(' ');

            if (items.Length == 0)
                return false;

            switch (items[0])
            {
                case "opinion":
                    return ParseOpinionCheatCode(items);
                case "owner":
                    return ParseOwnerCheatCode(items);
                default:
                    return false;
            }
        }

        private CheatCodeParser(GameCore game)
        {
            this.game = game;
        }

        private static CheatCodeParser cheatCodeParser; 

        public static CheatCodeParser getInstance()
        {
            if (cheatCodeParser == null)
                cheatCodeParser = new CheatCodeParser(GameCore.getInstance());
            return cheatCodeParser;
        }
    }
}
