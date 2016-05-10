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
        private int specialAttack; //how much damage the mascot's special can do 

        // parameterized constructor 
        public Mascot(string unitName, string ms, int teamNumber, string schoolName) : base(unitName, teamNumber, schoolName)
        {
            mascotSpecial = ms;

            //sets up mascot ability
            if (mascotSpecial == "Super Slam") //UofR Rocky special
            {
                //a very powerful attack that also deals some damge to Rocky themself
                minSpecialAttackRange = 1;
                maxSpecialAttackRange = 1;
                specialAttack = 10;
            }
            else if (mascotSpecial == "Super Pounce") //RIT Ritchie special
            {
                //a powerful leaping attack that allows ritchie to attack 1 extra space away
                minSpecialAttackRange = 2;
                maxSpecialAttackRange = 2;
                specialAttack = 7;
            }
        }

        //handles some special things with mascot special abilities
        public void UseMascotAbility()
        {
            //do something depending on mascot ability
            if (mascotSpecial == "Super Slam") //UofR Rocky special
            {
                //deal some self-damage
                CurrHealth -= 2;
            }
            else if (mascotSpecial == "Super Pounce") //RIT Ritchie special
            {
                //nothing special occurs with this one
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
        }

        public int MaxSpecialAttackRange
        {
            get { return maxSpecialAttackRange; }
        }

        public int SpecialAttack
        {
            get { return specialAttack; }
        }
    }
}
