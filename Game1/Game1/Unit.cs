using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Author(s): Beau Marwaha, Cody Freeman
//Purpose: Represents a generic unit
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
        private int mapX; //units x pos on the map
        private int mapY; //units y pos on the map
        private bool alive; // if false, unit is dead 
        private bool turnDone; //if a unit's turn is done
        private int minAttackRange; //units min attack range
        private int maxAttackRange; //units max attack range

        //param constructor
        public Unit(string unitType, int teamNumber, string schoolName) // creates the units with specific params
        {
            unitName = unitType;
            team = teamNumber;
            school = schoolName; 

            if(unitType == "Hockey")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
                minAttackRange = 1;
                maxAttackRange = 1;
            }
            else if(unitType == "Lacrosse")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
                minAttackRange = 1;
                maxAttackRange = 1;
            }
            else if(unitType == "Football")
            {
                totalMovePoints = 4;
                totalHealth = 15;
                attack = 3;
                defense = 0;
                minAttackRange = 1;
                maxAttackRange = 1;
            }
            else if(unitType == "Outdoor Club")
            {
                totalMovePoints = 4;
                totalHealth = 7;
                attack = 4;
                defense = 0;
                minAttackRange = 1;
                maxAttackRange = 1;
            }
            else if (unitType == "Archery Club")
            {
                totalMovePoints = 4;
                totalHealth = 6;
                attack = 4;
                defense = 1;
                minAttackRange = 2;
                maxAttackRange = 3;
            }
            else if(unitType == "Fraternity" || unitType == "Sorority")
            {
                totalMovePoints = 4;
                totalHealth = 5;
                attack = 5;
                defense = 0;
                minAttackRange = 1;
                maxAttackRange = 1;
            }
            else if(unitType == "EMS Club")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 2;
                defense = 1;
                minAttackRange = 1;
                maxAttackRange = 1;
            }
            if (unitType == "Ritchie")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
                minAttackRange = 1;
                maxAttackRange = 1;
            }
            if (unitType == "Rocky")
            {
                totalMovePoints = 4;
                totalHealth = 10;
                attack = 5;
                defense = 1;
                minAttackRange = 1;
                maxAttackRange = 1;
            }

            alive = true;
            currHealth = totalHealth;
            currMovePoints = totalMovePoints;
            turnDone = false;
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

        public bool TurnDone
        {
            get { return turnDone; }
            set { turnDone = value; }
        }

        public int MinAttackRange
        {
            get { return minAttackRange; }
            set { minAttackRange = value; }
        }

        public int MaxAttackRange
        {
            get { return maxAttackRange; }
            set { maxAttackRange = value; }
        }
        
    }
}
