using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    public class GameEvent
    {
        private string name = "";
        private string text = "";

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }
        }

        public Action NextTurnAction;

        public GameEvent(string name, string text, Action nextTurnAction)
        {
            this.name = name;
            this.text = text;

            NextTurnAction = nextTurnAction;
        }
    }
}
