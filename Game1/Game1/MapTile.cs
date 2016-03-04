using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class MapTile
    {
        // Attributes
        string terrainType;  // gives the map tile its terrain
        int movmentCost;  // gives the map tile its movement cost consistant with terrain type
        int defBonus;  // gives a defensive bonus consistant with terrain type
        bool filled;  // variable to toggle whether a unit is standing on the tile or not
        int xCord;  // x-coordinate of the map tile
        int yCord;  // y-coordinate of the map tile

        // Methods
        public MapTile(int x, int y, string terrain, int movement, int def)  // Parameterized constructor. Pulls from Map class
        {
            terrainType = terrain;
            movmentCost = movement;
            defBonus = def;
            xCord = x;
            yCord = y;
        }

        public string TerrainType
        {
            get { return terrainType; }
        }

        public int MovementCost
        {
            get { return MovementCost; }
        }

        public int DefBonus
        {
            get { return defBonus; }
        }

        public bool Filled
        {
            get { return filled; }
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
