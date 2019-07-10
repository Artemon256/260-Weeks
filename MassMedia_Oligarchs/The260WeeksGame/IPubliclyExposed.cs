using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The260WeeksGame
{
    interface IPubliclyExposed
    {
        double PublicPopularity
        {
            get;
            set;
        }
        
        string Name
        {
            get;
        } 
    }
}
