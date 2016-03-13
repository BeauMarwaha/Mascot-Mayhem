using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Mascot:Unit
    {
        // attributes
        string mascotSpecial;

        // parameterized constructor 
        public Mascot(string unitName, string ms) : base(unitName)
        {
            mascotSpecial = ms;
        }

        // mehthod to use a mascots ability
        public void UseMascotAbility()
        {
            if (mascotSpecial == "Super hit") // names of specials are just place holders
            {
                // code to change health of enemy unit
                //actionPoints = actionPoints - 1; // general code to deduct an action point
            }
            else if (mascotSpecial == "Super heal")
            {
                // code to add health to a cetain unit
                //actionPoints = actionPoints - 1; // deduct action point
            }

            // etc. etc.
        }
    }
}
