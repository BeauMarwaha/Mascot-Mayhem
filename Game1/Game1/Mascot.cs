using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Author(s): Beau Marwaha, Jared Miller
//Purpose: Represents a mascot unit
namespace Game1
{
    class Mascot:Unit
    {
        // attributes
        private string mascotSpecial;

        // parameterized constructor 
        public Mascot(string unitName, string ms, int teamNumber, string schoolName) : base(unitName, teamNumber, schoolName)
        {
            mascotSpecial = ms;
        }

        // method to use a mascots ability
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

        //properties
        public string MascotSpecial
        {
            get { return mascotSpecial; }
        }
    }
}
