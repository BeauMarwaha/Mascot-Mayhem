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
        private int totalMovePoints; // how many movement points the unit has total
        private int currMovePoints; // how many move points the unit has now
        private string school; // determines characteristics and color
        private int team; //team number
        private int mapX;
        private int mapY; // both store position on the map
        private bool alive; // if false, unit is dead 
        private string special; // used in UseAbility method, if unit has one

        // methods 
        public Unit(string unitType, int teamNumber) // creates the units with specific params
        {
            unitName = unitType;
            team = teamNumber;

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
            else if (unitType == "Archery Club")
            {
                totalMovePoints = 4;
                totalHealth = 6;
                attack = 3;
                defense = 1;
                special = "Long Shot";
            }
            else if(unitType == "Fraternity" || unitType == "Sorority")
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
                special = null; //has a mascot special
            }
            if (unitType == "Rocky")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
                special = null; //has a mascot special
            }

            alive = true;
            currHealth = totalHealth;
            currMovePoints = totalMovePoints;
        }

        //properties
        public string UnitName
        {
            get { return unitName; }
        }

        public int TotalHealth
        {
            get { return totalHealth; }
        }

        public int CurrHealth
        {
            get { return currHealth; }
            set { currHealth = value; }
        }

        public int Team
        {
            get { return team; }
            set { team = value; }
        }

        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        public int TotalMovePoints
        {
            get { return totalMovePoints; }
        }

        public int CurrMovePoints
        {
            get { return currMovePoints; }
            set { currMovePoints = value; }
        }

        public string School
        {
            get { return school; }
            set { school = value; }
        }

        public int MapX
        {
            get { return mapX; }
            set { mapX = value; }
        }

        public int MapY
        {
            get { return mapY; }
            set { mapY = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public string Special
        {
            get { return special; }
        }


        //will need to look over these methods again*****************************************************************************************************
        public void ChangeHealth(int damage) // changes unit health when attacked
        {
            currHealth = currHealth - damage; 
            if(currHealth <= 0) // damage kills the unit
            {
                currHealth = 0; 
                alive = false; 
            }
        }
        
        public void UseAbility(string ability)
        {
            // in the brackets will contain if statements that check the string passed in and do specific things based on it
        }
        //*********************************************************************************************************************************************************

    }
}
