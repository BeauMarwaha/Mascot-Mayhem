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
        public void LoadCollege(string name, Dictionary<string, Texture2D> sprites)
        {
            collegeName = name;
            unitSprites = sprites;

            if(name == "RIT")
            {
                units = new Unit[10];
                units[0] = new Unit("Hockey");
                units[1] = new Unit("Hockey");
                units[2] = new Unit("Hockey");
                units[3] = new Unit("Hockey");
                units[4] = new Unit("Football");
                units[5] = new Unit("Football");
                units[6] = new Unit("Outdoor Club");
                units[7] = new Unit("Fraternity");
                units[8] = new Unit("Sorority");
                units[9] = new Unit("EMS Club");
                mascot = new Mascot("Ritchie", "Super hit");
            }
            else if (name == "UofR")
            {
                units = new Unit[10];
                units[0] = new Unit("Lacrosse");
                units[1] = new Unit("Lacrosse");
                units[2] = new Unit("Lacrosse");
                units[3] = new Unit("Lacrosse");
                units[4] = new Unit("Football");
                units[5] = new Unit("Football");
                units[6] = new Unit("Outdoor Club");
                units[7] = new Unit("Fraternity");
                units[8] = new Unit("Sorority");
                units[9] = new Unit("EMS Club");
                mascot = new Mascot("Rocky", "Super heal");
            }
        }

        //draws the college units onto the screen
        public void DrawCollegeUnits(SpriteBatch sB, GraphicsDevice gD)
        {
            foreach(Unit unit in units)
            {
                sB.Draw(unitSprites[unit.UnitName], new Rectangle(unit.MapX * gD.Viewport.Width / 10, unit.MapY * gD.Viewport.Height / 10, gD.Viewport.Width / 10, gD.Viewport.Height / 10), Color.White);
            }
        }

    }
}
