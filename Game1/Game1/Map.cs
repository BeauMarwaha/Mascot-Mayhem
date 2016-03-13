﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Author(s): Beau Marwaha
//Purpose: Represents a full map to be used in game
namespace Game1
{
    class Map
    {
        //attributes
        private string name;
        private MapTile[,] tiles;

        //param constructor
        public Map(string nm, MapTile[,] fullTiles) 
        {
            name = nm;
            tiles = fullTiles;
        }

        //properties
        public string Name
        {
            get { return name; }
        }
        
        public MapTile[,] Tiles
        {
            get { return Tiles; }
        }
        
        //calculates possible tile movements based on a certain number of movement points and starting location
        public List<MapTile> PossibleMoves(int mP, int x, int y)
        {
            List<MapTile> possibleMoves = new List<MapTile>();
            int[,] movementCosts = new int[tiles.GetLength(1),tiles.GetLength(2)]; //will hold movement costs for each tile on the board
            
            foreach(MapTile tile in tiles)
            {
                //represents the difference of the tile from the target tile
                int xDiff = tile.XCord - x;
                int yDiff = tile.YCord - y;
                int mvmtCost = 0; //individual tile  movement cost

                if(xDiff < 0) //if the tile's x-cord is below the target tile
                {
                    for (int i = xDiff + x; i <= 0; i++)
                    {
                        mvmtCost += tiles[i, tile.YCord].MovementCost;
                    }
                }
                else if (xDiff > 0) //if the tile's x-cord is above the target tile
                {
                    for (int i = xDiff + x; i >= 0; i--)
                    {
                        mvmtCost += tiles[i, tile.YCord].MovementCost;
                    }
                }

                if (yDiff < 0) //if the tile's x-cord is below the target tile
                {
                    for (int i = yDiff + y + 1; i < 0; i++)
                    {
                        mvmtCost += tiles[x, i].MovementCost;
                    }
                }
                else if (yDiff > 0) //if the tile's x-cord is above the target tile
                {
                    for (int i = yDiff + y - 1; i > 0; i--)
                    {
                        mvmtCost += tiles[x, i].MovementCost;
                    }
                }

                //check if calculated mvmtCost is reachable within provided movement points
                if(mvmtCost <= mP)
                {
                    possibleMoves.Add(tile);
                }
            }

            return possibleMoves;
        }
    }
}
