using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class President : GameMember
    {    
        private string name = "Mr. President";
        public String Name
        {
            get
            {
                return name;
            }
        }

        public override void Turn() {

        }

        public President()
        {
            AbsoluteRating = GameCore.random.Next(-30, 30); // TODO: Remove and make another function which will base on the game difficulty
        }
    }
}
