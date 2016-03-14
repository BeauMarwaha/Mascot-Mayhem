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
        private string unitName; // type of unit being created; used in the Constructor
        private int totalHealth; // max hit points for a given unit 
        private int currHealth; // how much health is remaining
        private int attack; // how much damage the unit can do 
        private int defense; // inherit protection from attacks
        private int move; // uses action points; number of spaces the unit can move on a given turn 
        private int totalMovePoints; // how many movement points the unit has total
        private int currMovePoints; // how many move points the unit has now
        private string school; // determines characteristics and color
        private int mapX;
        private int mapY; // both store position on the map
        private Boolean alive; // if false, unit is dead 
        private string special; // used in UseAbility method, if unit has one

        // methods 
        public Unit(string unitType) // creates the units with specific params
        {
            unitName = unitType;

            if(unitType == "Hockey")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
                special = null; //no special
            }
            else if(unitType == "Lacrosse")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
                special = null; //no special
            }
            else if(unitType == "Football")
            {
                totalMovePoints = 4;
                totalHealth = 15;
                attack = 3;
                defense = 0;
                special = "Bulk Up";
            }
            else if(unitType == "Outdoor Club")
            {
                totalMovePoints = 4;
                totalHealth = 7;
                attack = 4;
                defense = 0;
                special = "Movement Stage";
            }
            else if(unitType == "Frat" || unitType == "Sorority")
            {
                totalMovePoints = 4;
                totalHealth = 5;
                attack = 2;
                defense = 0;
                special = "Newfound Strength";
            }
            else if(unitType == "EMS Club")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 2;
                defense = 1;
                special = "Heal";
            }
            if (unitType == "Ritchie")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
            }
            if (unitType == "Rocky")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
            }

            alive = true;
            currHealth = totalHealth;
            currMovePoints = totalMovePoints;
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
            return attack; 
        }

        public void ChangeHealth(int damage) // changes unit health when attacked
        {
            currHealth = currHealth - damage; 
            if(currHealth <= 0) // damage kills the unit
            {
                currHealth = 0; 
                alive = false; 
            }
        }

        public int Move(int actionPoints)
        {
            // check actionPoints remaining 
            if (currMovePoints <= 0)
            {
                return 0; // cannot move due to lack of actionPoints
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
