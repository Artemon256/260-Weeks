using System;
using System.Collections.Generic;

namespace _260Weeks.GameLogic
{
    public class Core
    {
        public List<Member> Members;

        public President Player;

        public PlayerInterface Interface;

        public Core(PlayerInterface Interface)
        {
            this.Interface = Interface;
            instance = this;
        }

        public void Init()
        {

        }

        public void Turn()
        {

        }

        private static Core instance;
        public static Core getInstance()
        {
            if (instance == null)
                throw (new NullReferenceException("getInstance is not allowed before initialization"));
            return instance;
        }
    }
}