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
        private string mascotSpecial; //name of mascot's special
        private int minSpecialAttackRange; //mascot's min special attack range
        private int maxSpecialAttackRange; //mascot's max special attack range

        // parameterized constructor 
        public Mascot(string unitName, string ms, int teamNumber, string schoolName) : base(unitName, teamNumber, schoolName)
        {
            mascotSpecial = ms;

            //sets up mascot ability
            if (mascotSpecial == "Super Slam") //UofR Rocky special
            {
                minSpecialAttackRange = 1;
                maxSpecialAttackRange = 1;
            }
            else if (mascotSpecial == "Super Pounce") //RIT Ritchie special
            {
                minSpecialAttackRange = 2;
                maxSpecialAttackRange = 2;
            }
        }

        // method to use a mascots ability
        public void UseMascotAbility()
        {
            //sets up mascot ability
            if (mascotSpecial == "Super Slam") //UofR Rocky special
            {
                
            }
            else if (mascotSpecial == "Super Pounce") //RIT Ritchie special
            {
                
            }
            
        }

        //properties
        public string MascotSpecial
        {
            get { return mascotSpecial; }
        }

        public int MinSpecialAttackRange
        {
            get { return minSpecialAttackRange; }
            set { minSpecialAttackRange = value; }
        }

        public int MaxSpecialAttackRange
        {
            get { return maxSpecialAttackRange; }
            set { maxSpecialAttackRange = value; }
        }
    }
}
