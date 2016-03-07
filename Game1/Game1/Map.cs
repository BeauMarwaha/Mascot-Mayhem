using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Author: Beau Marwaha
//Purpose: Represents a full map to be used in game
namespace Game1
{
    class Map
    {
        //attributes
        private string name;
        //private MapTile[] tiles; //needs maptile class to be coded

        //param constructor
        public Map(string nm) //add MapTile array once its coded
        {
            name = nm;
        }

        //properties
        public string Name
        {
            get { return name; }
        }

        //add getter for MapTiles

        //add PossibleMoves method
    }
}
