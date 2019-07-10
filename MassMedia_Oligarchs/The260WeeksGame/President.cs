using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    class President : IPubliclyExposed
    {
    
        private string name = "Mr. President";
        private double publicPopularity;

        public double PublicPopularity
        {
            get
            {
                return publicPopularity;
            }

            set
            {
                publicPopularity = value;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

        public President()
        {
            PublicPopularity = 50;
        }
    }
}
