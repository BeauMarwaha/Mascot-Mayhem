using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO; //needed for file IO
using System;
using System.Collections.Generic; //for dictionary

//Author(s): Beau Marwaha
//Purpose: Runs the game
namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //extra atributes
        enum GameState { Menu, MapSelect, TeamSelect, Game};
        GameState curState;
        MouseState mState;
        MouseState mStatePrev;
        Map mainMap;
        College college1;
        College college2;
        int turn; //college's turn
        int turnPhase; //individual units turn phase
        int selectedUnit; //index of selected unit in college.units
        List<MapTile> possibleMoves;
        List<MapTile> possibleAttacks;

        //used as a dictionary of all unit textures
        Dictionary<string, Texture2D> unitSprites;

        SpriteFont font;
        Texture2D blank;
        Texture2D menu;
        //units
        Texture2D hockeyPlayerPic;
        Texture2D lacrossePlayerPic;
        Texture2D footballPlayerPic;
        Texture2D archeryClubPlayerPic;
        Texture2D outdoorClubPlayerPic;
        Texture2D fraternityPlayerPic;
        Texture2D sororityPlayerPic;
        Texture2D emsClubPlayerPic;
        Texture2D ritchieMascotPic;
        Texture2D rockyMascotPic;

        //tiles
        Texture2D fieldTilePic;
        Texture2D riverTilePic;
        Texture2D pavementTilePic;
        Texture2D forestTilePic;
        Texture2D winTilePic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            curState = GameState.Menu;
            college1 = new College();
            college2 = new College();
            unitSprites = new Dictionary<string, Texture2D>();
            turn = 1;
            turnPhase = 0;
            selectedUnit = -1;
            possibleMoves = new List<MapTile>();
            possibleAttacks = new List<MapTile>();

            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("mainFont");
            blank = Content.Load<Texture2D>("blank");
            menu = Content.Load<Texture2D>("menu");

            //units - NOTE: currently using placeholder pictures
            hockeyPlayerPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Hockey", hockeyPlayerPic);

            lacrossePlayerPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Lacrosse", lacrossePlayerPic);

            footballPlayerPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Football", footballPlayerPic);

            archeryClubPlayerPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Archery Club", archeryClubPlayerPic);

            outdoorClubPlayerPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Outdoor Club", outdoorClubPlayerPic);

            fraternityPlayerPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Fraternity", fraternityPlayerPic);

            sororityPlayerPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Sorority", sororityPlayerPic);

            emsClubPlayerPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("EMS Club", emsClubPlayerPic);

            ritchieMascotPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Ritchie", ritchieMascotPic);

            rockyMascotPic = Content.Load<Texture2D>("SpaceShip");
            unitSprites.Add("Rocky", rockyMascotPic);

            //tiles - NOTE: currently using placeholder pictures
            fieldTilePic = Content.Load<Texture2D>("SpaceCoin");
            riverTilePic = Content.Load<Texture2D>("SpaceCoin");
            pavementTilePic = Content.Load<Texture2D>("SpaceCoin");
            forestTilePic = Content.Load<Texture2D>("SpaceCoin");
            winTilePic = Content.Load<Texture2D>("SpaceCoin");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mState = Mouse.GetState();
            
            //check for the current game state
            switch (curState)
            {
                case GameState.Menu:
                    curState = GameState.MapSelect;
                    break;

                case GameState.MapSelect:
                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 50, 220, 25)))
                    {
                        LoadMap("test");
                        curState = GameState.TeamSelect;
                    }
                    break;

                case GameState.TeamSelect:
                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 50, 300, 25)))
                    {
                        //create default collgege 1(RIT)
                        college1.LoadCollege("RIT", unitSprites, 1);

                        //load default starting positions
                        for(int i = 0; i < 10; i++)
                        {
                            college1.Units[i].MapX = 0;
                            college1.Units[i].MapY = i;
                            mainMap.GetTile(i, 0).Filled = true;
                        }

                        //create default collgege 2(UofR)
                        college2.LoadCollege("UofR", unitSprites, 2);

                        //load default starting positions
                        for (int i = 0; i < 10; i++)
                        {
                            college2.Units[i].MapX = 9;
                            college2.Units[i].MapY = i;
                            mainMap.GetTile(i, 9).Filled = true;
                        }

                        curState = GameState.Game;
                    }
                    break;

                case GameState.Game:
                    if (selectedUnit == -2) //if the option menu is brought up
                    {
                        if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 13, 120, 25))) //resume game
                        {
                            selectedUnit = -1;
                        }
                        else if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 6, 120, 25))) //end turn
                        {
                            if (turn == 1) //if first players turn
                            {
                                //switch turn
                                selectedUnit = -1;
                                turnPhase = 0;
                                turn = 2;

                                //reset all units turn done states
                                foreach(Unit unit in college1.Units)
                                {
                                    unit.TurnDone = false;
                                }
                            }
                            else if (turn == 2) //if second players turn
                            {
                                //switch turn
                                selectedUnit = -1;
                                turnPhase = 0;
                                turn = 1;

                                //reset all units turn done states
                                foreach (Unit unit in college2.Units)
                                {
                                    unit.TurnDone = false;
                                }

                            }
                        }
                        else if(SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 4, 140, 25))) //main menu
                        {
                            curState = GameState.Menu;
                        }
                        else if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 3, 130, 25))) //exit game
                        {
                            this.Exit();
                        }
                    }
                    else //normal play
                    {
                        for (int row = 0; row < 10; row++) //check each map row
                        {
                            for (int col = 0; col < 10; col++) //check each map col
                            {
                                //check for a mouse click in that location
                                if (SingleLeftMouseLocationPress(new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10)))
                                {
                                    if (mainMap.GetTile(row, col).Filled) //if a unit is in that spot
                                    {
                                        if (turn == 1) //if first players turn
                                        {
                                            switch (turnPhase)
                                            {
                                                case 0: //move
                                                    for (int i = 0; i < 10; i++) //check each unit on team 1
                                                    {
                                                        if (college1.Units[i].MapX == col && college1.Units[i].MapY == row) //if the unit is in that space
                                                        {
                                                            selectedUnit = i;
                                                            if (!college1.Units[selectedUnit].TurnDone) //check if that unit has already made their turn
                                                            {
                                                                possibleMoves = mainMap.PossibleMoves(college1.Units[selectedUnit].CurrMovePoints, college1.Units[selectedUnit].MapX, college1.Units[selectedUnit].MapY);
                                                            }

                                                        }
                                                    }
                                                    break;
                                                case 1: //action
                                                    break;
                                            }
                                        }
                                        else if (turn == 2) //if second players turn
                                        {
                                            switch (turnPhase)
                                            {
                                                case 0: //move
                                                    for (int i = 0; i < 10; i++) //check each unit on team 2
                                                    {
                                                        if (college2.Units[i].MapX == col && college2.Units[i].MapY == row) //if the unit is in that space
                                                        {
                                                            selectedUnit = i;
                                                            if (!college2.Units[selectedUnit].TurnDone) //check if that unit has already made their turn
                                                            {
                                                                possibleMoves = mainMap.PossibleMoves(college2.Units[selectedUnit].CurrMovePoints, college2.Units[selectedUnit].MapX, college2.Units[selectedUnit].MapY);
                                                            }
                                                        }
                                                    }
                                                    break;
                                                case 1: //action
                                                    break;
                                            }

                                        }
                                    }
                                    else if (possibleMoves.Contains(mainMap.GetTile(row, col))) //if a movement can be made
                                    {
                                        turnPhase = 1; //switch phase to action phase

                                        //set up possible attacks after movement
                                        if (turn == 1) //if first players turn
                                        {
                                            mainMap.GetTile(college1.Units[selectedUnit].MapY, college1.Units[selectedUnit].MapX).Filled = false;

                                            college1.Units[selectedUnit].MapX = col;
                                            college1.Units[selectedUnit].MapY = row;

                                            mainMap.GetTile(college1.Units[selectedUnit].MapY, college1.Units[selectedUnit].MapX).Filled = true;

                                            possibleAttacks = mainMap.PossibleAttacks(1, college1.Units[selectedUnit].MapX, college1.Units[selectedUnit].MapY);
                                            if (possibleAttacks.Count == 0) //if there are no possible attacks
                                            {
                                                //switch back to move phase so another unit can do their turn
                                                turnPhase = 0; 
                                                //end that units turn
                                                college1.Units[selectedUnit].TurnDone = true;
                                                selectedUnit = -1;
                                            }
                                        }
                                        else if (turn == 2) //if second players turn
                                        {
                                            mainMap.GetTile(college2.Units[selectedUnit].MapY, college2.Units[selectedUnit].MapX).Filled = false;

                                            college2.Units[selectedUnit].MapX = col;
                                            college2.Units[selectedUnit].MapY = row;

                                            mainMap.GetTile(college2.Units[selectedUnit].MapY, college2.Units[selectedUnit].MapX).Filled = true;

                                            possibleAttacks = mainMap.PossibleAttacks(1, college2.Units[selectedUnit].MapX, college2.Units[selectedUnit].MapY);
                                            if (possibleAttacks.Count == 0) //if there are no possible attacks
                                            {
                                                //switch back to move phase so another unit can do their turn
                                                turnPhase = 0; 
                                                //end that units turn
                                                college2.Units[selectedUnit].TurnDone = true;
                                                selectedUnit = -1;
                                            }
                                        }

                                        possibleMoves.Clear();
                                    }
                                    else if (possibleAttacks.Contains(mainMap.GetTile(row, col))) //if an attack can be made
                                    {
                                        //implement attacking logic here*******************************************************************************************************

                                        turnPhase = 0; //switch phase to move phase
                                        possibleAttacks.Clear();
                                    }
                                    else //if the spot is empty
                                    {
                                        if (turnPhase == 1 && selectedUnit > -1) //to cancel attack
                                        {
                                            turnPhase = 0;
                                            //end that units turn
                                            if (turn == 1) //if first players turn
                                            {
                                                college1.Units[selectedUnit].TurnDone = true;
                                                selectedUnit = -1;
                                            }
                                            else if (turn == 2) //if second players turn
                                            {
                                                college2.Units[selectedUnit].TurnDone = true;
                                                selectedUnit = -1;
                                            }
                                        }
                                        else //bring up option menu
                                        {
                                            selectedUnit = -2;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            mStatePrev = mState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //check for the current game state
            switch (curState)
            {
                case GameState.Menu:
                    break;

                case GameState.MapSelect:
                    spriteBatch.DrawString(font, "Load Default Map", new Vector2(GraphicsDevice.Viewport.Width/2 - 100, GraphicsDevice.Viewport.Height/2 - 50), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 50, 220, 25)));
                    break;

                case GameState.TeamSelect:
                    spriteBatch.DrawString(font, "Load Default Teams", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 50), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 50, 220, 25)));
                    break;

                case GameState.Game:
                    DrawMap(); //add check for map tile scrolled over later with final art pieces
                    college1.DrawCollegeUnits(spriteBatch, GraphicsDevice, turn);
                    college2.DrawCollegeUnits(spriteBatch, GraphicsDevice, turn);

                    if(selectedUnit != -1) //if a unit is selected
                    {
                        if(selectedUnit == -2) //bring up option menu
                        {
                            DrawOptionMenu();
                        }
                        else
                        {
                            DrawUnitActionInfo();
                        }
                    }
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //draws the game board based on the size of the screen
        public void DrawMap()
        {
            for(int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (mainMap.GetTile(row,col).TerrainType == "Field")
                    {
                        spriteBatch.Draw(fieldTilePic, new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), ScrolledOver(new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10)));
                    }
                    else if (mainMap.GetTile(row, col).TerrainType == "River")
                    {
                        spriteBatch.Draw(riverTilePic, new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), ScrolledOver(new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10)));
                    }
                    else if (mainMap.GetTile(row, col).TerrainType == "Pavement")
                    {
                        spriteBatch.Draw(pavementTilePic, new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), ScrolledOver(new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10)));
                    }
                    else if (mainMap.GetTile(row, col).TerrainType == "Forest")
                    {
                        spriteBatch.Draw(forestTilePic, new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), ScrolledOver(new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10)));
                    }
                    else if (mainMap.GetTile(row, col).TerrainType == "Win Tile")
                    {
                        spriteBatch.Draw(winTilePic, new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), ScrolledOver(new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10)));
                    }
                }
            }
        }

        //loads a map from an external file
        public void LoadMap(string fileName)
        {
            MapTile[,] newTiles = new MapTile[10,10];
            try
            {
                //open the file with StreamReader
                StreamReader input = new StreamReader(fileName + ".txt");

                //loop to read in and display each line
                string[] line = new string[10];
                string[] splitLine0, splitLine1, splitLine2, splitLine3, splitLine4, splitLine5, splitLine6, splitLine7, splitLine8, splitLine9;
                for (int i = 0; i < 10; i++)
                {
                    line[i] = input.ReadLine();
                }

                //split up the string lines
                splitLine0 = line[0].Split(',');
                splitLine1 = line[1].Split(',');
                splitLine2 = line[2].Split(',');
                splitLine3 = line[3].Split(',');
                splitLine4 = line[4].Split(',');
                splitLine5 = line[5].Split(',');
                splitLine6 = line[6].Split(',');
                splitLine7 = line[7].Split(',');
                splitLine8 = line[8].Split(',');
                splitLine9 = line[9].Split(',');

                //fill up the map tiles
                for (int col = 0; col < 10; col++)//row 0
                {
                    newTiles[0, col] = new MapTile(0, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 1
                {
                    newTiles[1, col] = new MapTile(1, col, splitLine1[col]);
                }
                for (int col = 0; col < 10; col++)//row 2
                {
                    newTiles[2, col] = new MapTile(2, col, splitLine2[col]);
                }
                for (int col = 0; col < 10; col++)//row 3
                {
                    newTiles[3, col] = new MapTile(3, col, splitLine3[col]);
                }
                for (int col = 0; col < 10; col++)//row 4
                {
                    newTiles[4, col] = new MapTile(4, col, splitLine4[col]);
                }
                for (int col = 0; col < 10; col++)//row 5
                {
                    newTiles[5, col] = new MapTile(5, col, splitLine5[col]);
                }
                for (int col = 0; col < 10; col++)//row 6
                {
                    newTiles[6, col] = new MapTile(6, col, splitLine6[col]);
                }
                for (int col = 0; col < 10; col++)//row 7
                {
                    newTiles[7, col] = new MapTile(7, col, splitLine7[col]);
                }
                for (int col = 0; col < 10; col++)//row 8
                {
                    newTiles[8, col] = new MapTile(8, col, splitLine8[col]);
                }
                for (int col = 0; col < 10; col++)//row 9
                {
                    newTiles[9, col] = new MapTile(9, col, splitLine9[col]);
                }

                // close the file
                input.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Reading: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            mainMap = new Map(fileName, newTiles);
        }
        
        //check for a single left mouse click
        public bool SingleLeftMousePress()
        {
            if (mState.LeftButton == ButtonState.Pressed && mStatePrev.LeftButton == ButtonState.Released) //check for one click
            {
                return true;
            }

            return false;
        }

        //check for a single left mouse click in a certain location
        public bool SingleLeftMouseLocationPress(Rectangle loc)
        {
            if (mState.LeftButton == ButtonState.Pressed && mStatePrev.LeftButton == ButtonState.Released) //check for one click
            {
                if((mState.Position.X > loc.X) && (mState.Position.X < loc.X + loc.Width)) //check for correct x position
                {
                    if ((mState.Position.Y > loc.Y) && (mState.Position.Y < loc.Y + loc.Height)) //check for correct y position
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //check to see if the mouse pointer in a certain location
        public Color ScrolledOver(Rectangle loc)
        {
            if ((mState.Position.X > loc.X) && (mState.Position.X < loc.X + loc.Width)) //check for correct x position
            {
                if ((mState.Position.Y > loc.Y) && (mState.Position.Y < loc.Y + loc.Height)) //check for correct y position
                {
                    return Color.Yellow; //highlight
                }
            }

            return Color.White;
        }

        //draws unit action related info
        private void DrawUnitActionInfo()
        {
            if(turn == 1)
            {
                switch (turnPhase)
                {
                    case 0: //move
                        foreach(MapTile tile in possibleMoves) //highlight tiles
                        {
                            spriteBatch.Draw(blank, new Rectangle(tile.YCord * GraphicsDevice.Viewport.Width / 10, tile.XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), Color.White);
                        }
                        break;
                    case 1: //action
                        foreach (MapTile tile in possibleAttacks) //highlight tiles
                        {
                            spriteBatch.Draw(blank, new Rectangle(tile.YCord * GraphicsDevice.Viewport.Width / 10, tile.XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), Color.White);
                        }
                        break;
                }
            }
            else if(turn == 2)
            {
                switch (turnPhase)
                {
                    case 0: //move
                        foreach (MapTile tile in possibleMoves) //highlight tiles
                        {
                            spriteBatch.Draw(blank, new Rectangle(tile.YCord * GraphicsDevice.Viewport.Width / 10, tile.XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), Color.White);
                        }
                        break;
                    case 1: //action
                        foreach (MapTile tile in possibleAttacks) //highlight tiles
                        {
                            spriteBatch.Draw(blank, new Rectangle(tile.YCord * GraphicsDevice.Viewport.Width / 10, tile.XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), Color.White);
                        }
                        break;
                }
            }
        }

        //draws option menu
        private void DrawOptionMenu()
        {
            spriteBatch.Draw(menu, new Rectangle(GraphicsDevice.Viewport.Width / 20, GraphicsDevice.Viewport.Height / 20, GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.DrawString(font, "Resume Game", new Vector2(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 13), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 13, 180, 25)));
            spriteBatch.DrawString(font, "End Turn", new Vector2(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 6), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 6, 120, 25)));
            spriteBatch.DrawString(font, "Main Menu", new Vector2(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 4), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 4, 140, 25)));
            spriteBatch.DrawString(font, "Exit Game", new Vector2(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 3), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 13, GraphicsDevice.Viewport.Height / 3, 130, 25)));
            if (turn == 1)
            {

            }
            else if (turn == 2)
            {

            }
        }
    }
}
