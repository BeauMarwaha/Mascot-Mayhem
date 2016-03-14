using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class College
    {
        // attributes
        // array for units to be loaded
        private Unit[] units;

        // mascot unit to load
        private Mascot mascot;

        // name for the college
        private string collegeName;

        // default constructor 
        public College(Unit[] team, Mascot masc, string name)
        {
            units = team;
            mascot = masc;
            collegeName = name;
        }
        
        //properties
        public Unit[] Units
        {
            get { return units; }
        }

        public Mascot Mascot
        {
            get { return mascot; }
        }

        public string CollegeName
        {
            get { return collegeName; }
        }
         
    }
}
