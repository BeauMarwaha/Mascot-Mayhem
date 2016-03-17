using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework; //for draw method
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class College
    {
        // attributes
        // array for units to be loaded
        private Unit[] units;

        // mascot unit to load
        private Mascot mascot;

        // name for the college
        private string collegeName;

        //unit Texture2D's
        private Dictionary<string, Texture2D> unitSprites;

        //default constructor
        public College()
        {
            collegeName = "Empty College";
        }

        //param constructor 
        public College(Unit[] team, Mascot masc, string name)
        {
            units = team;
            mascot = masc;
            collegeName = name;
        }
        
        //properties
        public Unit[] Units
        {
            get { return units; }
        }

        public Mascot Mascot
        {
            get { return mascot; }
        }

        public string CollegeName
        {
            get { return collegeName; }
        }

        //loads predefined college teams
        public void LoadCollege(string name, Dictionary<string, Texture2D> sprites, int team)
        {
            collegeName = name;
            unitSprites = sprites;

            if(name == "RIT")
            {
                units = new Unit[10];
                units[0] = new Unit("Hockey", team);
                units[1] = new Unit("Hockey", team);
                units[2] = new Unit("Hockey", team);
                units[3] = new Unit("Hockey", team);
                units[4] = new Unit("Football", team);
                units[5] = new Unit("Football", team);
                units[6] = new Unit("Outdoor Club", team);
                units[7] = new Unit("Fraternity", team);
                units[8] = new Unit("Sorority", team);
                units[9] = new Unit("EMS Club", team);
                mascot = new Mascot("Ritchie", "Super hit", team);
            }
            else if (name == "UofR")
            {
                units = new Unit[10];
                units[0] = new Unit("Lacrosse", team);
                units[1] = new Unit("Lacrosse", team);
                units[2] = new Unit("Lacrosse", team);
                units[3] = new Unit("Lacrosse", team);
                units[4] = new Unit("Football", team);
                units[5] = new Unit("Football", team);
                units[6] = new Unit("Outdoor Club", team);
                units[7] = new Unit("Fraternity", team);
                units[8] = new Unit("Sorority", team);
                units[9] = new Unit("EMS Club", team);
                mascot = new Mascot("Rocky", "Super heal", team);
            }
        }

        //draws the college units onto the screen
        public void DrawCollegeUnits(SpriteBatch sB, GraphicsDevice gD, int turn)
        {
            foreach(Unit unit in units)
            {
                if(unit.Team == 1) //flip image
                {
                    var origin = new Vector2(unitSprites[unit.UnitName].Width / 20f, unitSprites[unit.UnitName].Height / 20f); //for use with image flipping
                    sB.Draw(unitSprites[unit.UnitName], new Rectangle(unit.MapX * gD.Viewport.Width / 10, unit.MapY * gD.Viewport.Height / 10, gD.Viewport.Width / 10, gD.Viewport.Height / 10), null, TurnCheck(unit, turn), 0f, origin, SpriteEffects.FlipHorizontally, 0f);
                }
                else //don't flip image
                {
                    sB.Draw(unitSprites[unit.UnitName], new Rectangle(unit.MapX * gD.Viewport.Width / 10, unit.MapY * gD.Viewport.Height / 10, gD.Viewport.Width / 10, gD.Viewport.Height / 10), TurnCheck(unit, turn));
                }
            }
        }

        //darken units when it isn't their turn or they have already used their move this turn
        public Color TurnCheck(Unit unit, int turn)
        {
            if (unit.Team != turn) //check if its not that unit's team's turn
            {
                return Color.Gray; //darken
            }
            else if (unit.TurnDone) //check if it's that unit's turn but they've already done their action this turn
            {
                return Color.LightGray; //slightly darken
            }

            return Color.White;
        }
    }
}
