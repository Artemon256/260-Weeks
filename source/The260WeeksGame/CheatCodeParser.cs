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

            int whoseID = 0;
            int aboutWhomID = 0;

            if (!int.TryParse(command[1], out whoseID))
                return false;

            if (!int.TryParse(command[2], out aboutWhomID))
                return false;

            GameMember whose = GameMember.GetGameMemberById(whoseID);
            GameMember aboutWhom = GameMember.GetGameMemberById(aboutWhomID);
            double value = Convert.ToDouble(command[3]);

            if (whose == null || aboutWhom == null)
                return false;

            whose.Opinions[aboutWhom] = GameMember.ConstrainOpinion(value);

            return true;
        }

        private bool ParseOwnerCheatCode(string[] command)
        {
            if (command.Length != 3)
                return false;

            int mediaId, ownerId;

            if(!int.TryParse(command[1], out mediaId))
                return false;
            
            if(!int.TryParse(command[2], out ownerId))
                return false;

            GameMember mediaMember = GameMember.GetGameMemberById(mediaId);
            GameMember ownerMember = GameMember.GetGameMemberById(ownerId);

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
