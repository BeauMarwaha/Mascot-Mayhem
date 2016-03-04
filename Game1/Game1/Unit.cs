using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    // class for a basic unit; contains all neccessary attributes and methods
    class Unit
    {
        // attributes
        private string unitType; // type of unit being created; used in the Constructor
        private int health; // max hit points for a given unit 
        private int attack; // how much damage the unit can do 
        private int defense; // inherit protection from attacks
        private int move; // uses action points; number of spaces the unit can move on a given turn 
        private int actionPoints; // how many points the unit has on a turn; how many it uses for an action
        private string school; // determines characteristics and color
        private int mapX;
        private int mapY; // both store position on the map
        private string debuff; // given by certain tiles; stores name of debuff/description
        private int debuffInt; // what the numnber of the debuff is 
        private Boolean alive; // if false, unit is dead 
        private string special; // used in UseAbility method, if unit has one

        // methods 
        public Unit(string unitType) // creates the units with specific params
        {
            // sets units stats
        }

        public int GetPositionX()
        {
            return mapX;
        }

        public int GetPositionY() // gets where the unit is on the map, along with GetPositionX
        {
            return mapY; 
        }
        public int Attack() // used when attacking a unit; returns an int 
        {
            int damageDelt = attack - debuffInt;
            return damageDelt;  
        }

        public void ChangeHealth(int damage) // changes unit health when attacked
        {
            health = health - damage; 
            if(health <= 0) // damage kills the unit
            {
                health = 0; 
                alive = false; 
            }
        }

        public int Move(int actionPoints)
        {
            // check actionPoints remaining 
            if (actionPoints <= 0)
            {
                return; // cannot move due to lack of actionPoints
            }
            else
            { return move; }
        }

        public void UseAbility(string ability)
        {
            // in the brackets will contain if statements that check the string passed in and do specific things based on it
        }

    }
}
