using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Author(s): Sean Hasse, Beau Marwaha
//Purpose: Represents a individual Map Tile
namespace Game1
{
    class MapTile
    {
        // Attributes
        private string terrainType;  // gives the map tile its terrain
        private int movementCost;  // gives the map tile its movement cost consistant with terrain type
        private int defBonus;  // gives a defensive bonus consistant with terrain type
        private bool filled;  // variable to toggle whether a unit is standing on the tile or not
        private int xCord;  // x-coordinate of the map tile
        private int yCord;  // y-coordinate of the map tile


        // Methods
        public MapTile(int x, int y, string terrain)  // Parameterized constructor. Pulls from Map class
        {
            xCord = x;
            yCord = y;
            filled = false;

            if (terrain == "Field")
            {
                terrainType = terrain;
                movementCost = 1;
                defBonus = 1;
            }
            else if (terrain == "River")
            {
                terrainType = terrain;
                movementCost = 100; //Impassable
                defBonus = 0; //arbitrary since tile cannont be entered
            }
            else if (terrain == "Pavement")
            {
                terrainType = terrain;
                movementCost = 1;
                defBonus = 0;
            }
            else if (terrain == "Forest")
            {
                terrainType = terrain;
                movementCost = 2;
                defBonus = 2;
            }
            else if (terrain == "Win Tile")
            {
                terrainType = terrain;
                movementCost = 1;
                defBonus = 0;
            }
        }

        public string TerrainType
        {
            get { return terrainType; }
        }

        public int MovementCost
        {
            get { return movementCost; }
        }

        public int DefBonus
        {
            get { return defBonus; }
        }

        public bool Filled
        {
            get { return filled; }
            set { filled = value; }
        }

        public int XCord
        {
            get { return xCord; }
        }

        public int YCord
        {
            get { return yCord; }
        }
    }
}
