﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO; //needed for file IO
using System;
using System.Collections.Generic; //for dictionary
using Microsoft.Xna.Framework.Audio; // needed for music 

//Author(s): Beau Marwaha, Sean Hasse, Jared Miller
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
        enum GameState { Menu, TeamSelect, MapSelect, Game};
        GameState curState;
        MouseState mState;
        MouseState mStatePrev;
        Map mainMap;
        College college1;
        College college2;
        int turn; //college's turn
        int turnPhase; //individual units turn phase
        int selectedUnit; //index of selected unit in college.units
        int defendingUnit; //index of defending unit in college.units
        List<MapTile> possibleMoves;
        List<MapTile> possibleAttacks;
        List<MapTile> possibleSuperAttacks;
        bool displayRules;
        bool college1Win;
        bool college2Win;
        //used in tracking the number of turns the capture point has benn captured by a specific team
        int college1WintTileCaptureTurns; 
        int college2WintTileCaptureTurns;

        //used when a normal or special attack could be made
        bool attackChoice;
        int specialCol;
        int specialRow;

        //used as a dictionary of all unit textures
        Dictionary<string, Texture2D> unitSprites;

        SpriteFont font;
        Texture2D blank;
        Texture2D redBlank;
        Texture2D menu;
        Texture2D rules;

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

        //schools
        Texture2D ritLogo;
        Texture2D uofrLogo;

        //maps
        Texture2D awh; // Aliens Were Here
        Texture2D check; // Checkered
        Texture2D cp; // Choke Point
        Texture2D foh; // Face of Evil
        Texture2D hf; // Happy Face
        Texture2D pp; // Pavement Plus
        Texture2D tb; // Target Board
        Texture2D ttb; // The Three Bridges
        Texture2D wf; // Wet Feet
        Texture2D xf; // X-Forest

        //menus
        Texture2D mainMenu;
        Texture2D standardMenu;
        /*
        // music 
        SoundEffect menuMusic;
        SoundEffect gameMusic;
        SoundEffect victoryMusic; // still needs implementing
        */
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
            defendingUnit = -1;
            possibleMoves = new List<MapTile>();
            possibleAttacks = new List<MapTile>();
            possibleSuperAttacks = new List<MapTile>();
            displayRules = false;
            college1Win = false;
            college2Win = false;
            college1WintTileCaptureTurns = 0;
            college2WintTileCaptureTurns = 0;

            attackChoice = false;
            specialCol = 0;
            specialRow = 0;

            this.Window.AllowUserResizing = true;
            this.Window.Title = "Mascot Mayhem";
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
            redBlank = Content.Load<Texture2D>("red blank");
            menu = Content.Load<Texture2D>("menu");
            rules = Content.Load<Texture2D>("rules");

            //units
            hockeyPlayerPic = Content.Load<Texture2D>("Chibi Hockey");
            unitSprites.Add("Hockey", hockeyPlayerPic);

            lacrossePlayerPic = Content.Load<Texture2D>("Chibi Lacrosse");
            unitSprites.Add("Lacrosse", lacrossePlayerPic);

            footballPlayerPic = Content.Load<Texture2D>("Chibi Football");
            unitSprites.Add("Football", footballPlayerPic);

            archeryClubPlayerPic = Content.Load<Texture2D>("Chibi Archer"); 
            unitSprites.Add("Archery Club", archeryClubPlayerPic);

            outdoorClubPlayerPic = Content.Load<Texture2D>("Chibi Outdoor");
            unitSprites.Add("Outdoor Club", outdoorClubPlayerPic);

            fraternityPlayerPic = Content.Load<Texture2D>("Chibi Sorority-Frat");
            unitSprites.Add("Fraternity", fraternityPlayerPic);

            sororityPlayerPic = Content.Load<Texture2D>("Chibi Sorority-Frat");
            unitSprites.Add("Sorority", sororityPlayerPic);

            emsClubPlayerPic = Content.Load<Texture2D>("Chibi EMS");
            unitSprites.Add("EMS Club", emsClubPlayerPic);

            ritchieMascotPic = Content.Load<Texture2D>("Chibi Ritchie");
            unitSprites.Add("Ritchie", ritchieMascotPic);

            rockyMascotPic = Content.Load<Texture2D>("Chibi Rocky");
            unitSprites.Add("Rocky", rockyMascotPic);

            //tiles
            fieldTilePic = Content.Load<Texture2D>("Field Tile");
            riverTilePic = Content.Load<Texture2D>("River Tile");
            pavementTilePic = Content.Load<Texture2D>("Pavement Tile");
            forestTilePic = Content.Load<Texture2D>("Forest Tile");
            winTilePic = Content.Load<Texture2D>("Win Tile");

            //schools
            ritLogo = Content.Load<Texture2D>("RIT Logo");
            uofrLogo = Content.Load<Texture2D>("UofR Logo");

            //maps
            awh = Content.Load<Texture2D>("AliensWereHerePreview");
            check = Content.Load<Texture2D>("CheckeredPreview");
            cp = Content.Load<Texture2D>("NewChokePointPreview");
            foh = Content.Load<Texture2D>("FaceOfEvilPreview");
            hf = Content.Load<Texture2D>("HappyFacePreview");
            pp = Content.Load<Texture2D>("PavementPlusPreview");
            tb = Content.Load<Texture2D>("TargetBoardPreview");
            ttb = Content.Load<Texture2D>("TheThreeBridgesPreview");
            wf = Content.Load<Texture2D>("NewWetFeetPreview");
            xf = Content.Load<Texture2D>("XForestPreview");

            //menus
            mainMenu = Content.Load<Texture2D>("Main menu");
            standardMenu = Content.Load<Texture2D>("Standard Menu");
            /*
            //sounds
            menuMusic = Content.Load<SoundEffect>("Les Toreadors");
            gameMusic = Content.Load<SoundEffect>("Can Can");
            victoryMusic = Content.Load<SoundEffect>("Overture 1812");
            */
            //Update screen size to fullscreen (coment out this line during testing)
            this.graphics.IsFullScreen = true;
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
            /*
            //music handler
            if(curState == GameState.Menu)
            {
                //menuMusic.Play();
            }
            else if(curState == GameState.Game)
            {
                //gameMusic.Play(); 
            } 
            */
            //check for the current game state
            switch (curState)
            {
                case GameState.Menu:
                    if (!displayRules)
                    {
                        if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 190, GraphicsDevice.Viewport.Height - 50, 140, 25))) //play game
                        {
                            //progress to team selection
                            curState = GameState.TeamSelect;
                            college1Win = false;
                            college2Win = false;
                        }
                        else if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 10, GraphicsDevice.Viewport.Height - 50, 75, 25))) //rules
                        {
                            //display the rules image on the screen
                            displayRules = true;
                        }
                        else if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 + 110, GraphicsDevice.Viewport.Height - 50, 140, 25))) //exit game
                        {
                            //exit the game
                            this.Exit();
                        }
                    }
                    else
                    {
                        if (SingleLeftMouseLocationPress(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height))) //remove the rules from the screen
                        {
                            //remove the rules image on the screen
                            displayRules = false;
                        }
                    } 
                    break;

                case GameState.TeamSelect:
                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height - 100, 190, 25))) //load teams
                    {
                        //create default collgege 1(RIT)
                        college1.LoadCollege("RIT", unitSprites, 1);
                        
                        //create default collgege 2(UofR)
                        college2.LoadCollege("UofR", unitSprites, 2);
                        
                        //progress to map selection
                        curState = GameState.MapSelect;
                    }
                    break;

                case GameState.MapSelect:
                    //run through where the mouse is clicking and load that map
                    /*//loaded the default map, no longer in use
                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 50, 220, 25))) //load default map
                    {
                        LoadMap("test"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }
                    */

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 6 - 115, GraphicsDevice.Viewport.Height / 6, 250, 120))) //load Aliens map
                    {
                        LoadMap("Aliens Were Here"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 4 + 47, GraphicsDevice.Viewport.Height / 6, 250, 120))) //load checkered map
                    {
                        LoadMap("Checkered"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 115, GraphicsDevice.Viewport.Height / 6, 250, 120))) //load choke map
                    {
                        LoadMap("Choke Point"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 + 200, GraphicsDevice.Viewport.Height / 6, 250, 120))) //load FoE map
                    {
                        LoadMap("Face of Evil"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 + 510, GraphicsDevice.Viewport.Height / 6, 250, 120))) //load Face map
                    {
                        LoadMap("Happy Face"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 6 - 115, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120))) //load Pavement map
                    {
                        LoadMap("Pavement Plus"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 4 + 47, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120))) //load Target map
                    {
                        LoadMap("Target Board"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 115, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120))) //load Bridges map
                    {
                        LoadMap("The Three Bridges"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 + 200, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120))) //load wet feet map
                    {
                        LoadMap("Wet Feet"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }

                    if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 + 510, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120))) //load X-forest map
                    {
                        LoadMap("X-Forest"); //change this for different file name

                        //set the units onto the board
                        SetUnits();

                        //progress to the game
                        curState = GameState.Game;
                    }
                    break;
                    
                case GameState.Game:
                    if (selectedUnit == -2 && !(college1Win || college2Win)) //if the option menu is brought up
                    {
                        if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 90, GraphicsDevice.Viewport.Height / 2 - 50, 180, 25))) //resume game
                        {
                            selectedUnit = -1;
                        }
                        else if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 65, GraphicsDevice.Viewport.Height / 2 - 25, 120, 25))) //end turn
                        {
                            if (turn == 1) //if first players turn
                            {
                                //check for a capture win
                                CaptureWinCheck();

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
                                //check for a capture win
                                CaptureWinCheck();

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
                        else if(SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 0, 140, 25))) //main menu
                        {
                            //reset game
                            selectedUnit = -1;
                            turnPhase = 0;
                            turn = 1;

                            //change the game state back to menu
                            curState = GameState.Menu;
                        }
                        else if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 + 25, 130, 25))) //exit game
                        {
                            //exit the game
                            this.Exit();
                        }
                    }
                    else if (college1Win || college2Win) //if a team has won
                    {
                        if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 0, 140, 25))) //main menu
                        {
                            //reset game
                            selectedUnit = -1;
                            turnPhase = 0;
                            turn = 1;

                            //change the game state back to menu
                            curState = GameState.Menu;
                        }
                        else if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 + 25, 130, 25))) //exit game
                        {
                            //exit the game
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
                                    //if a unit is in that spot (2nd part of the if statement allows for a unit to not move during their turn)
                                    if (mainMap.GetTile(row, col).Filled && !possibleMoves.Contains(mainMap.GetTile(row, col)) && !possibleAttacks.Contains(mainMap.GetTile(row, col)) && !possibleSuperAttacks.Contains(mainMap.GetTile(row, col))) 
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

                                            //create a list of tiles with friendly units on it
                                            List<MapTile> friendlyTiles = new List<MapTile>();
                                            foreach (Unit unit in college1.Units)
                                            {
                                                if (unit.Alive)
                                                {
                                                    friendlyTiles.Add(mainMap.GetTile(unit.MapY, unit.MapX));
                                                }
                                            }

                                            possibleAttacks = mainMap.PossibleAttacks(college1.Units[selectedUnit].MinAttackRange, college1.Units[selectedUnit].MaxAttackRange, college1.Units[selectedUnit].MapX, college1.Units[selectedUnit].MapY, friendlyTiles);
                                            
                                            //if a mascot unit also do a special check
                                            if(college1.Units[selectedUnit] is Mascot)
                                            {
                                                Mascot mascot = (Mascot)(college1.Units[selectedUnit]);
                                                possibleSuperAttacks = mainMap.PossibleAttacks(mascot.MinSpecialAttackRange, mascot.MaxSpecialAttackRange, college1.Units[selectedUnit].MapX, college1.Units[selectedUnit].MapY, friendlyTiles);
                                            }

                                            //clear friendly tiles
                                            friendlyTiles.Clear();

                                            if (possibleAttacks.Count == 0 && possibleSuperAttacks.Count == 0) //if there are no possible attacks
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

                                            //create a list of tiles with friendly units on it
                                            List<MapTile> friendlyTiles = new List<MapTile>();
                                            foreach(Unit unit in college2.Units)
                                            {
                                                if (unit.Alive)
                                                {
                                                    friendlyTiles.Add(mainMap.GetTile(unit.MapY, unit.MapX));
                                                }
                                                    
                                            }

                                            possibleAttacks = mainMap.PossibleAttacks(college2.Units[selectedUnit].MinAttackRange, college2.Units[selectedUnit].MaxAttackRange, college2.Units[selectedUnit].MapX, college2.Units[selectedUnit].MapY, friendlyTiles);

                                            //if a mascot unit also do a special check
                                            if (college2.Units[selectedUnit] is Mascot)
                                            {
                                                Mascot mascot = (Mascot)(college2.Units[selectedUnit]);
                                                possibleSuperAttacks = mainMap.PossibleAttacks(mascot.MinSpecialAttackRange, mascot.MaxSpecialAttackRange, college2.Units[selectedUnit].MapX, college2.Units[selectedUnit].MapY, friendlyTiles);
                                            }

                                            //clear friendly tiles
                                            friendlyTiles.Clear();

                                            if (possibleAttacks.Count == 0 && possibleSuperAttacks.Count == 0) //if there are no possible attacks
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
                                    else if (possibleAttacks.Contains(mainMap.GetTile(row, col)) || possibleSuperAttacks.Contains(mainMap.GetTile(row, col))) //if an attack can be made
                                    {
                                        //attacking/defending code
                                        if(turn == 1) //first players turn
                                        {
                                            for (int i = 0; i < 10; i++) //check each unit on team 2
                                            {
                                                if (college2.Units[i].MapX == col && college2.Units[i].MapY == row) //if the unit is in that space
                                                {
                                                    //if only a normal attack is possible in that location
                                                    if (possibleAttacks.Contains(mainMap.GetTile(row, col)) && !possibleSuperAttacks.Contains(mainMap.GetTile(row, col))) 
                                                    {
                                                        defendingUnit = i; //define that unit's index as the one being attacked

                                                        //check to make sure the attack will actually do damage to the target
                                                        if ((college1.Units[selectedUnit].Attack - mainMap.GetTile(college2.Units[defendingUnit].MapY, college2.Units[defendingUnit].MapX).DefBonus - college2.Units[defendingUnit].Defense) > 0)
                                                        {
                                                            //deal the damage
                                                            college2.Units[defendingUnit].CurrHealth -= college1.Units[selectedUnit].Attack - mainMap.GetTile(college2.Units[defendingUnit].MapY, college2.Units[defendingUnit].MapX).DefBonus - college2.Units[defendingUnit].Defense;
                                                            turnPhase = 0; //switch phase to move phase
                                                                           //end that units turn
                                                            college1.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleAttacks.Clear();
                                                        }
                                                        else //deal 0 damage
                                                        {
                                                            //deal the damage
                                                            college2.Units[defendingUnit].CurrHealth -= 0;
                                                            turnPhase = 0; //switch phase to move phase
                                                                           //end that units turn
                                                            college1.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleAttacks.Clear();
                                                        }
                                                    }
                                                    //if only a special attack is possible in that location
                                                    else if (!possibleAttacks.Contains(mainMap.GetTile(row, col)) && possibleSuperAttacks.Contains(mainMap.GetTile(row, col)))
                                                    {
                                                        defendingUnit = i; //define that unit's index as the one being attacked
                                                        Mascot mascot = (Mascot)(college1.Units[selectedUnit]); //explicitly cast the mascot unit

                                                        //check to make sure the attack will actually do damage to the target
                                                        if ((mascot.SpecialAttack - mainMap.GetTile(college2.Units[defendingUnit].MapY, college2.Units[defendingUnit].MapX).DefBonus - college2.Units[defendingUnit].Defense) > 0)
                                                        {
                                                            //deal the damage
                                                            college2.Units[defendingUnit].CurrHealth -= mascot.SpecialAttack - mainMap.GetTile(college2.Units[defendingUnit].MapY, college2.Units[defendingUnit].MapX).DefBonus - college2.Units[defendingUnit].Defense;
                                                            
                                                            //run mascot special ability method
                                                            mascot.UseMascotAbility();

                                                            //reapply mascot stats to the selected unit
                                                            college1.Units[selectedUnit] = mascot;

                                                            turnPhase = 0; //switch phase to move phase
                                                                           //end that units turn
                                                            college1.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleSuperAttacks.Clear();
                                                        }
                                                        else //deal 0 damage
                                                        {
                                                            //deal the damage
                                                            college2.Units[defendingUnit].CurrHealth -= 0;

                                                            //run mascot special ability method
                                                            mascot.UseMascotAbility();

                                                            //reapply mascot stats to the selected unit
                                                            college1.Units[selectedUnit] = mascot;

                                                            turnPhase = 0; //switch phase to move phase
                                                                           //end that units turn
                                                            college1.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleSuperAttacks.Clear();
                                                        }
                                                    }
                                                    //if both a normal and super attack is possible in that location
                                                    else
                                                    {
                                                        //open up the special option menu and save the defending unit's location
                                                        attackChoice = true;
                                                        specialCol = col;
                                                        specialRow = row;
                                                    }
                                                }
                                            }
                                            
                                        }
                                        else if(turn == 2) //second players turn
                                        {
                                            for (int i = 0; i < 10; i++) //check each unit on team 1
                                            {
                                                if (college1.Units[i].MapX == col && college1.Units[i].MapY == row) //if the unit is in that space
                                                {
                                                    //if only a normal attack is possible in that location
                                                    if (possibleAttacks.Contains(mainMap.GetTile(row, col)) && !possibleSuperAttacks.Contains(mainMap.GetTile(row, col))) 
                                                    {
                                                        defendingUnit = i; //define that unit's index as the one being attacked

                                                        //check to make sure the attack will actually do damage to the target
                                                        if ((college2.Units[selectedUnit].Attack - mainMap.GetTile(college1.Units[defendingUnit].MapY, college1.Units[defendingUnit].MapX).DefBonus - college1.Units[defendingUnit].Defense) > 0)
                                                        {
                                                            //deal the damage
                                                            college1.Units[defendingUnit].CurrHealth -= college2.Units[selectedUnit].Attack - mainMap.GetTile(college1.Units[defendingUnit].MapY, college1.Units[defendingUnit].MapX).DefBonus - college1.Units[defendingUnit].Defense;
                                                            turnPhase = 0; //switch phase to move phase
                                                                           //end that units turn
                                                            college2.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleAttacks.Clear();
                                                        }
                                                        else //deal 0 damage
                                                        {
                                                            //deal the damage
                                                            college1.Units[defendingUnit].CurrHealth -= 0;
                                                            turnPhase = 0; //switch phase to move phase
                                                                           //end that units turn
                                                            college2.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleAttacks.Clear();
                                                        }
                                                    }
                                                    //if only a special attack is possible in that location
                                                    else if (!possibleAttacks.Contains(mainMap.GetTile(row, col)) && possibleSuperAttacks.Contains(mainMap.GetTile(row, col)))
                                                    {
                                                        defendingUnit = i; //define that unit's index as the one being attacked
                                                        Mascot mascot = (Mascot)(college2.Units[selectedUnit]); //explicitly cast the mascot unit

                                                        //check to make sure the attack will actually do damage to the target
                                                        if ((mascot.SpecialAttack - mainMap.GetTile(college1.Units[defendingUnit].MapY, college1.Units[defendingUnit].MapX).DefBonus - college1.Units[defendingUnit].Defense) > 0)
                                                        {
                                                            //deal the damage
                                                            college1.Units[defendingUnit].CurrHealth -= mascot.SpecialAttack - mainMap.GetTile(college1.Units[defendingUnit].MapY, college1.Units[defendingUnit].MapX).DefBonus - college1.Units[defendingUnit].Defense;

                                                            //run mascot special ability method
                                                            mascot.UseMascotAbility();

                                                            //reapply mascot stats to the selected unit
                                                            college2.Units[selectedUnit] = mascot;

                                                            turnPhase = 0; //switch phase to move phase
                                                                           //end that units turn
                                                            college2.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleSuperAttacks.Clear();
                                                        }
                                                        else //deal 0 damage
                                                        {
                                                            //deal the damage
                                                            college1.Units[defendingUnit].CurrHealth -= 0;

                                                            //run mascot special ability method
                                                            mascot.UseMascotAbility();

                                                            //reapply mascot stats to the selected unit
                                                            college2.Units[selectedUnit] = mascot;

                                                            turnPhase = 0; //switch phase to move phase
                                                                           //end that units turn
                                                            college2.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleSuperAttacks.Clear();
                                                        }
                                                    }
                                                    //if both a normal and super attack is possible in that location
                                                    else
                                                    {
                                                        //open up the special option menu and save the defending unit's location
                                                        attackChoice = true;
                                                        specialCol = col;
                                                        specialRow = row;
                                                    }
                                                }
                                            }
                                        }

                                        RemoveDeadUnits(); //remove any units that have been killed
                                        DominationWinCheck(); //check for a domination win
                                    }
                                    else if (attackChoice) //if either a special attack or normal attack could be made
                                    {
                                        //depending on which choice is made in the option menu
                                        if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 80, GraphicsDevice.Viewport.Height / 2 - 0, 170, 25))) //normal attack
                                        {
                                            //attacking/defending code
                                            if (turn == 1) //first players turn
                                            {
                                                for (int i = 0; i < 10; i++) //check each unit on team 2
                                                {
                                                    if (college2.Units[i].MapX == specialCol && college2.Units[i].MapY == specialRow) //if the unit is in that space
                                                    {
                                                        //perform a normal attack
                                                        defendingUnit = i; //define that unit's index as the one being attacked

                                                        //check to make sure the attack will actually do damage to the target
                                                        if ((college1.Units[selectedUnit].Attack - mainMap.GetTile(college2.Units[defendingUnit].MapY, college2.Units[defendingUnit].MapX).DefBonus - college2.Units[defendingUnit].Defense) > 0)
                                                        {
                                                            //deal the damage
                                                            college2.Units[defendingUnit].CurrHealth -= college1.Units[selectedUnit].Attack - mainMap.GetTile(college2.Units[defendingUnit].MapY, college2.Units[defendingUnit].MapX).DefBonus - college2.Units[defendingUnit].Defense;
                                                            turnPhase = 0; //switch phase to move phase
                                                                            //end that units turn
                                                            college1.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleAttacks.Clear();
                                                        }
                                                        else //deal 0 damage
                                                        {
                                                            //deal the damage
                                                            college2.Units[defendingUnit].CurrHealth -= 0;
                                                            turnPhase = 0; //switch phase to move phase
                                                                            //end that units turn
                                                            college1.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleAttacks.Clear();
                                                        }
                                                    }
                                                }

                                            }
                                            else if (turn == 2) //second players turn
                                            {
                                                for (int i = 0; i < 10; i++) //check each unit on team 1
                                                {
                                                    if (college1.Units[i].MapX == specialCol && college1.Units[i].MapY == specialRow) //if the unit is in that space
                                                    {
                                                        //perform a normal attack
                                                        defendingUnit = i; //define that unit's index as the one being attacked

                                                        //check to make sure the attack will actually do damage to the target
                                                        if ((college2.Units[selectedUnit].Attack - mainMap.GetTile(college1.Units[defendingUnit].MapY, college1.Units[defendingUnit].MapX).DefBonus - college1.Units[defendingUnit].Defense) > 0)
                                                        {
                                                            //deal the damage
                                                            college1.Units[defendingUnit].CurrHealth -= college2.Units[selectedUnit].Attack - mainMap.GetTile(college1.Units[defendingUnit].MapY, college1.Units[defendingUnit].MapX).DefBonus - college1.Units[defendingUnit].Defense;
                                                            turnPhase = 0; //switch phase to move phase
                                                                            //end that units turn
                                                            college2.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleAttacks.Clear();
                                                        }
                                                        else //deal 0 damage
                                                        {
                                                            //deal the damage
                                                            college1.Units[defendingUnit].CurrHealth -= 0;
                                                            turnPhase = 0; //switch phase to move phase
                                                                            //end that units turn
                                                            college2.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleAttacks.Clear();
                                                        }
                                                    }
                                                }
                                            }

                                            RemoveDeadUnits(); //remove any units that have been killed
                                            DominationWinCheck(); //check for a domination win

                                            //reset attack choice and defending unit loc
                                            attackChoice = false;
                                            specialCol = 0;
                                            specialRow = 0;
                                        }
                                        else if (SingleLeftMouseLocationPress(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 81, GraphicsDevice.Viewport.Height / 2 + 25, 170, 25))) //special attack
                                        {
                                            //attacking/defending code
                                            if (turn == 1) //first players turn
                                            {
                                                for (int i = 0; i < 10; i++) //check each unit on team 2
                                                {
                                                    if (college2.Units[i].MapX == specialCol && college2.Units[i].MapY == specialRow) //if the unit is in that space
                                                    {
                                                        //perform a special attack
                                                        defendingUnit = i; //define that unit's index as the one being attacked
                                                        Mascot mascot = (Mascot)(college1.Units[selectedUnit]); //explicitly cast the mascot unit

                                                        //check to make sure the attack will actually do damage to the target
                                                        if ((mascot.SpecialAttack - mainMap.GetTile(college2.Units[defendingUnit].MapY, college2.Units[defendingUnit].MapX).DefBonus - college2.Units[defendingUnit].Defense) > 0)
                                                        {
                                                            //deal the damage
                                                            college2.Units[defendingUnit].CurrHealth -= mascot.SpecialAttack - mainMap.GetTile(college2.Units[defendingUnit].MapY, college2.Units[defendingUnit].MapX).DefBonus - college2.Units[defendingUnit].Defense;

                                                            //run mascot special ability method
                                                            mascot.UseMascotAbility();

                                                            //reapply mascot stats to the selected unit
                                                            college1.Units[selectedUnit] = mascot;

                                                            turnPhase = 0; //switch phase to move phase
                                                                            //end that units turn
                                                            college1.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleSuperAttacks.Clear();
                                                        }
                                                        else //deal 0 damage
                                                        {
                                                            //deal the damage
                                                            college2.Units[defendingUnit].CurrHealth -= 0;

                                                            //run mascot special ability method
                                                            mascot.UseMascotAbility();

                                                            //reapply mascot stats to the selected unit
                                                            college1.Units[selectedUnit] = mascot;

                                                            turnPhase = 0; //switch phase to move phase
                                                                            //end that units turn
                                                            college1.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleSuperAttacks.Clear();
                                                        }
                                                    }
                                                }

                                            }
                                            else if (turn == 2) //second players turn
                                            {
                                                for (int i = 0; i < 10; i++) //check each unit on team 1
                                                {
                                                    if (college1.Units[i].MapX == specialCol && college1.Units[i].MapY == specialRow) //if the unit is in that space
                                                    {
                                                        //perform a special attack
                                                        defendingUnit = i; //define that unit's index as the one being attacked
                                                        Mascot mascot = (Mascot)(college2.Units[selectedUnit]); //explicitly cast the mascot unit

                                                        //check to make sure the attack will actually do damage to the target
                                                        if ((mascot.SpecialAttack - mainMap.GetTile(college1.Units[defendingUnit].MapY, college1.Units[defendingUnit].MapX).DefBonus - college1.Units[defendingUnit].Defense) > 0)
                                                        {
                                                            //deal the damage
                                                            college1.Units[defendingUnit].CurrHealth -= mascot.SpecialAttack - mainMap.GetTile(college1.Units[defendingUnit].MapY, college1.Units[defendingUnit].MapX).DefBonus - college1.Units[defendingUnit].Defense;

                                                            //run mascot special ability method
                                                            mascot.UseMascotAbility();

                                                            //reapply mascot stats to the selected unit
                                                            college2.Units[selectedUnit] = mascot;

                                                            turnPhase = 0; //switch phase to move phase
                                                                            //end that units turn
                                                            college2.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleSuperAttacks.Clear();
                                                        }
                                                        else //deal 0 damage
                                                        {
                                                            //deal the damage
                                                            college1.Units[defendingUnit].CurrHealth -= 0;

                                                            //run mascot special ability method
                                                            mascot.UseMascotAbility();

                                                            //reapply mascot stats to the selected unit
                                                            college2.Units[selectedUnit] = mascot;

                                                            turnPhase = 0; //switch phase to move phase
                                                                            //end that units turn
                                                            college2.Units[selectedUnit].TurnDone = true;
                                                            selectedUnit = -1;
                                                            possibleSuperAttacks.Clear();
                                                        }
                                                    }
                                                }
                                            }

                                            RemoveDeadUnits(); //remove any units that have been killed
                                            DominationWinCheck(); //check for a domination win

                                            //reset attack choice
                                            attackChoice = false;
                                            specialCol = 0;
                                            specialRow = 0;
                                        }
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
                                            //fixes error with trying to move a unit, pausing the game, and then trying to immediately move it again
                                            possibleMoves.Clear();
                                            possibleAttacks.Clear();
                                            possibleSuperAttacks.Clear();

                                            //brings up option menu
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
                    spriteBatch.Draw(mainMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    if (displayRules)
                    {
                        spriteBatch.Draw(rules, new Rectangle(10, 10, GraphicsDevice.Viewport.Width - 20, GraphicsDevice.Viewport.Height - 100), Color.White);
                        spriteBatch.DrawString(font, "Click anywhere to exit the rules", new Vector2(GraphicsDevice.Viewport.Width/2 - 200, GraphicsDevice.Viewport.Height - 60), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Play Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 190, GraphicsDevice.Viewport.Height - 50), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 190, GraphicsDevice.Viewport.Height - 50, 140, 25)));
                        spriteBatch.DrawString(font, "Rules", new Vector2(GraphicsDevice.Viewport.Width / 2 - 10, GraphicsDevice.Viewport.Height - 50), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 10, GraphicsDevice.Viewport.Height - 50, 75, 25)));
                        spriteBatch.DrawString(font, "Exit Game", new Vector2(GraphicsDevice.Viewport.Width / 2 + 110, GraphicsDevice.Viewport.Height - 50), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 + 110, GraphicsDevice.Viewport.Height - 50, 140, 25)));
                    }
                    break;

                case GameState.TeamSelect:
                    spriteBatch.Draw(standardMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(font, "Team Selection", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, 100), Color.White);
                    spriteBatch.DrawString(font, "Team 1", new Vector2(395, GraphicsDevice.Viewport.Height - 200), Color.White);
                    spriteBatch.DrawString(font, "Team 2", new Vector2(GraphicsDevice.Viewport.Width - 490, GraphicsDevice.Viewport.Height - 200), Color.White);
                    spriteBatch.DrawString(font, "Rochester Institute of Technology", new Vector2(240, GraphicsDevice.Viewport.Height - 250), Color.White);
                    spriteBatch.DrawString(font, "University of Rochester", new Vector2(GraphicsDevice.Viewport.Width - 580, GraphicsDevice.Viewport.Height - 250), Color.White);
                    spriteBatch.Draw(ritLogo, new Rectangle(200, 250, 500, 500), Color.White);
                    spriteBatch.Draw(uofrLogo, new Rectangle(GraphicsDevice.Viewport.Width - 700, 250, 500, 500), Color.White);
                    spriteBatch.DrawString(font, "Confirm Teams", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height - 100), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height - 100, 190, 25)));
                    break;

                case GameState.MapSelect:
                    spriteBatch.Draw(standardMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(font, "Map Selection", new Vector2(GraphicsDevice.Viewport.Width / 2 - 80, 100), Color.White);
                    spriteBatch.DrawString(font, "Select A Map By Clicking On It", new Vector2(GraphicsDevice.Viewport.Width/2 - 180, GraphicsDevice.Viewport.Height/2 - 50), Color.White);
                    
                    // load custom maps
                    spriteBatch.Draw(awh, new Rectangle(GraphicsDevice.Viewport.Width / 6 - 115, GraphicsDevice.Viewport.Height / 6, 250, 120), Color.White); // Draw "Aliens Were Here" map preview
                    spriteBatch.DrawString(font, "Aliens Were Here", new Vector2(GraphicsDevice.Viewport.Width / 6 - 98, GraphicsDevice.Viewport.Height / 6 + 140), Color.White);
                    spriteBatch.Draw(check, new Rectangle(GraphicsDevice.Viewport.Width / 4 + 47, GraphicsDevice.Viewport.Height / 6, 250, 120), Color.White); // Draw "Checkered" map preview
                    spriteBatch.DrawString(font, "Checkered", new Vector2(GraphicsDevice.Viewport.Width / 4 + 105, GraphicsDevice.Viewport.Height / 6 + 140), Color.White);
                    spriteBatch.Draw(cp, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 115, GraphicsDevice.Viewport.Height / 6, 250, 120), Color.White); // Draw "Choke Point" map preview
                    spriteBatch.DrawString(font, "Choke Point", new Vector2(GraphicsDevice.Viewport.Width / 2 - 67, GraphicsDevice.Viewport.Height / 6 + 140), Color.White);
                    spriteBatch.DrawString(font, "(King of the Hill)", new Vector2(GraphicsDevice.Viewport.Width / 2 - 90, GraphicsDevice.Viewport.Height / 6 + 180), Color.White);
                    spriteBatch.Draw(foh, new Rectangle(GraphicsDevice.Viewport.Width / 2 + 200, GraphicsDevice.Viewport.Height / 6, 250, 120), Color.White); // Draw "Face of Evil" map preview
                    spriteBatch.DrawString(font, "Face of Evil", new Vector2(GraphicsDevice.Viewport.Width / 2 + 255, GraphicsDevice.Viewport.Height / 6 + 140), Color.White);
                    spriteBatch.Draw(hf, new Rectangle(GraphicsDevice.Viewport.Width / 2 + 510, GraphicsDevice.Viewport.Height / 6, 250, 120), Color.White); // Draw "Happy Face" map preview
                    spriteBatch.DrawString(font, "Happy Face", new Vector2(GraphicsDevice.Viewport.Width / 2 + 565, GraphicsDevice.Viewport.Height / 6 + 140), Color.White);
                    spriteBatch.Draw(pp, new Rectangle(GraphicsDevice.Viewport.Width / 6 - 115, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120), Color.White); // Draw "Pavement Plus" map preview
                    spriteBatch.DrawString(font, "Pavement Plus", new Vector2(GraphicsDevice.Viewport.Width / 6 - 80, GraphicsDevice.Viewport.Height / 2 + 310), Color.White);
                    spriteBatch.Draw(tb, new Rectangle(GraphicsDevice.Viewport.Width / 4 + 47, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120), Color.White); // Draw "Target Board" map preview
                    spriteBatch.DrawString(font, "Target Board", new Vector2(GraphicsDevice.Viewport.Width / 4 + 82, GraphicsDevice.Viewport.Height / 2 + 310), Color.White);
                    spriteBatch.Draw(ttb, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 115, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120), Color.White); // Draw "The Three Bridges" map preview
                    spriteBatch.DrawString(font, "The Three Bridges", new Vector2(GraphicsDevice.Viewport.Width / 2 - 102, GraphicsDevice.Viewport.Height / 2 + 310), Color.White);
                    spriteBatch.Draw(wf, new Rectangle(GraphicsDevice.Viewport.Width / 2 + 200, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120), Color.White); // Draw "Wet Feet" map preview
                    spriteBatch.DrawString(font, "Wet Feet", new Vector2(GraphicsDevice.Viewport.Width / 2 + 270, GraphicsDevice.Viewport.Height / 2 + 310), Color.White);
                    spriteBatch.DrawString(font, "(King of the Hill)", new Vector2(GraphicsDevice.Viewport.Width / 2 + 235, GraphicsDevice.Viewport.Height / 2 + 350), Color.White);
                    spriteBatch.Draw(xf, new Rectangle(GraphicsDevice.Viewport.Width / 2 + 510, GraphicsDevice.Viewport.Height / 2 + 170, 250, 120), Color.White); // Draw "X-Forest" map preview
                    spriteBatch.DrawString(font, "X-Forest", new Vector2(GraphicsDevice.Viewport.Width / 2 + 585, GraphicsDevice.Viewport.Height / 2 + 310), Color.White);
                    break;
                    
                case GameState.Game:
                    DrawMap(); //draws the game board
                    college1.DrawCollegeUnits(spriteBatch, GraphicsDevice, turn); //draws team 1's units
                    college2.DrawCollegeUnits(spriteBatch, GraphicsDevice, turn); //draws team 2's units

                    //if the menu or win screen is not currently up
                    if (selectedUnit != -2 || !(college1Win || college2Win))
                    {
                        DrawTileInfo(); //draws individual tile info
                    }
                    
                    if(selectedUnit != -1) //if a unit/option menu is selected
                    {
                        if(selectedUnit == -2) //bring up option menu
                        {
                            DrawOptionMenu();
                        }
                        else //highlight possible moves/attacks
                        {
                            DrawUnitActionInfo();
                        }
                    }

                    //if the player has clicked on an attack space that could either be a normal or special attack draw a special options menu
                    if (attackChoice)
                    {
                        DrawAttackOptionMenu();
                    }

                    //if a team wins draw the victory screen
                    if (college1Win)
                    {
                        spriteBatch.Draw(menu, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 125, GraphicsDevice.Viewport.Height / 2 - 55, 250, 120), Color.White);
                        spriteBatch.DrawString(font, college1.CollegeName + " Wins!", new Vector2(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2 - 50), Color.White);
                        spriteBatch.DrawString(font, "Main Menu", new Vector2(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 0), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 0, 140, 25)));
                        spriteBatch.DrawString(font, "Exit Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 + 25), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 + 25, 130, 25)));
                    }
                    else if (college2Win)
                    {
                        spriteBatch.Draw(menu, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 125, GraphicsDevice.Viewport.Height / 2 - 55, 250, 120), Color.White);
                        spriteBatch.DrawString(font, college2.CollegeName + " Wins!", new Vector2(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2 - 50), Color.White);
                        spriteBatch.DrawString(font, "Main Menu", new Vector2(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 0), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 0, 140, 25)));
                        spriteBatch.DrawString(font, "Exit Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 + 25), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 + 25, 130, 25)));
                    }
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //draws info related to the tile currently scrolled over and a unit that may be on it
        public void DrawTileInfo()
        {
            //check for the tile currently scrolled over
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    //checks to see if a tile is scrolled over by checking if it is highlighted
                    if (ScrolledOver(new Rectangle(mainMap.GetTile(row, col).YCord * GraphicsDevice.Viewport.Width / 10, mainMap.GetTile(row, col).XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10)) == Color.Orange)
                    {
                        //if the scrolled over tile is in the left side of the screen draw the info at the bottom right
                        if (col > 5) 
                        {
                            //draw tile info
                            spriteBatch.Draw(menu, new Rectangle(0, GraphicsDevice.Viewport.Height - 96, 350, 96), Color.White);
                            spriteBatch.DrawString(font, "Terrain Type: " + mainMap.GetTile(row, col).TerrainType, new Vector2(GraphicsDevice.Viewport.Width / 55, GraphicsDevice.Viewport.Height - 91), Color.White);
                            spriteBatch.DrawString(font, "Movement Cost: " + mainMap.GetTile(row, col).MovementCost, new Vector2(GraphicsDevice.Viewport.Width / 55, GraphicsDevice.Viewport.Height - 64), Color.White);
                            spriteBatch.DrawString(font, "Def Bonus: " + mainMap.GetTile(row, col).DefBonus, new Vector2(GraphicsDevice.Viewport.Width / 55, GraphicsDevice.Viewport.Height - 37), Color.White);

                            //if there is a unit in that tile
                            if (mainMap.GetTile(row, col).Filled)
                            {
                                //draw unit info
                                spriteBatch.Draw(menu, new Rectangle(0, GraphicsDevice.Viewport.Height - 260, 273, 164), Color.White);
                                college1.DrawUnitInfo(spriteBatch, GraphicsDevice, col, row, font, GraphicsDevice.Viewport.Width / 55);
                                college2.DrawUnitInfo(spriteBatch, GraphicsDevice, col, row, font, GraphicsDevice.Viewport.Width / 55);
                            }
                        }
                        else //draw the info at the botom left
                        {
                            //draw tile info
                            spriteBatch.Draw(menu, new Rectangle(GraphicsDevice.Viewport.Width - 350, GraphicsDevice.Viewport.Height - 96, 350, 96), Color.White);
                            spriteBatch.DrawString(font, "Terrain Type: " + mainMap.GetTile(row, col).TerrainType, new Vector2(GraphicsDevice.Viewport.Width - 330, GraphicsDevice.Viewport.Height - 91), Color.White);
                            spriteBatch.DrawString(font, "Movement Cost: " + mainMap.GetTile(row, col).MovementCost, new Vector2(GraphicsDevice.Viewport.Width - 330, GraphicsDevice.Viewport.Height - 64), Color.White);
                            spriteBatch.DrawString(font, "Def Bonus: " + mainMap.GetTile(row, col).DefBonus, new Vector2(GraphicsDevice.Viewport.Width - 330, GraphicsDevice.Viewport.Height - 37), Color.White);

                            //if there is a unit in that tile
                            if (mainMap.GetTile(row, col).Filled)
                            {
                                //draw unit info
                                spriteBatch.Draw(menu, new Rectangle(GraphicsDevice.Viewport.Width - 273, GraphicsDevice.Viewport.Height - 260, 273, 164), Color.White);
                                college1.DrawUnitInfo(spriteBatch, GraphicsDevice, col, row, font, GraphicsDevice.Viewport.Width - 257);
                                college2.DrawUnitInfo(spriteBatch, GraphicsDevice, col, row, font, GraphicsDevice.Viewport.Width - 257);
                            }
                        }
                    }
                }
            }
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
                StreamReader input = new StreamReader("../../../Maps/" + fileName + ".txt");

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
                    return Color.Orange; //highlight
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
                        foreach (MapTile tile in possibleSuperAttacks) //highlight tiles
                        {
                            spriteBatch.Draw(redBlank, new Rectangle(tile.YCord * GraphicsDevice.Viewport.Width / 10, tile.XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), Color.White);
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
                        foreach (MapTile tile in possibleSuperAttacks) //highlight tiles
                        {
                            spriteBatch.Draw(redBlank, new Rectangle(tile.YCord * GraphicsDevice.Viewport.Width / 10, tile.XCord * GraphicsDevice.Viewport.Height / 10, GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height / 10), Color.White);
                        }
                        break;
                }
            }
        }

        //draws option menu
        private void DrawOptionMenu()
        {
            spriteBatch.Draw(menu, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 125, GraphicsDevice.Viewport.Height / 2 - 55, 250, 120), Color.White);
            spriteBatch.DrawString(font, "Resume Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 90, GraphicsDevice.Viewport.Height / 2 - 50), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 90, GraphicsDevice.Viewport.Height / 2 - 50, 180, 25)));
            spriteBatch.DrawString(font, "End Turn", new Vector2(GraphicsDevice.Viewport.Width / 2 - 65, GraphicsDevice.Viewport.Height / 2 - 25), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 65, GraphicsDevice.Viewport.Height / 2 - 25, 120, 25)));
            spriteBatch.DrawString(font, "Main Menu", new Vector2(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 0), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 75, GraphicsDevice.Viewport.Height / 2 - 0, 140, 25)));
            spriteBatch.DrawString(font, "Exit Game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 + 25), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 70, GraphicsDevice.Viewport.Height / 2 + 25, 130, 25)));
        }

        //draws attack option menu
        private void DrawAttackOptionMenu()
        {
            spriteBatch.Draw(menu, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 125, GraphicsDevice.Viewport.Height / 2 - 55, 250, 120), Color.White);
            spriteBatch.DrawString(font, "Choose An Attack", new Vector2(GraphicsDevice.Viewport.Width / 2 - 110, GraphicsDevice.Viewport.Height / 2 - 50), Color.White);
            spriteBatch.DrawString(font, "Normal Attack", new Vector2(GraphicsDevice.Viewport.Width / 2 - 80, GraphicsDevice.Viewport.Height / 2 - 0), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 80, GraphicsDevice.Viewport.Height / 2 - 0, 170, 25)));
            spriteBatch.DrawString(font, "Special Attack", new Vector2(GraphicsDevice.Viewport.Width / 2 - 81, GraphicsDevice.Viewport.Height / 2 + 25), ScrolledOver(new Rectangle(GraphicsDevice.Viewport.Width / 2 - 81, GraphicsDevice.Viewport.Height / 2 + 25, 170, 25)));
        }

        //checks for a win by defeating all enemy units
        public void DominationWinCheck()
        {
            if(turn == 1) //check for an entirely dead team 2
            {
                int deadUnits = 0;
                foreach(Unit unit in college2.Units)
                {
                    if (!unit.Alive)
                    {
                        deadUnits++;
                    }
                }

                //if the entire enemy team is dead que victory screen
                if(deadUnits == 10)
                {
                    college1Win = true;
                }
            }
            else if(turn == 2) //check for an entirely dead team 1
            {
                int deadUnits = 0;
                foreach (Unit unit in college1.Units)
                {
                    if (!unit.Alive)
                    {
                        deadUnits++;
                    }
                }

                //if the entire enemy team is dead que victory screen
                if (deadUnits == 10)
                {
                    college2Win = true;
                }
            }
        }

        //checks for a win by capturing the win tiles
        public void CaptureWinCheck()
        {
            //get a list of all win tiles on the map
            List<MapTile> winTiles = new List<MapTile>();
            for (int row = 0; row < 10; row++) //check each map row
            {
                for (int col = 0; col < 10; col++) //check each map col
                {
                    if(mainMap.GetTile(row, col).TerrainType == "Win Tile")
                    {
                        winTiles.Add(mainMap.GetTile(col, row));
                    }
                }
            }

            //check to see how many of the win tiles have been captured by team 1
            int winTilesTeam1Occupied = 0;
            foreach (MapTile wTiles in winTiles)
            {
                foreach (Unit unit in college1.Units)
                {
                    if(wTiles.XCord == unit.MapX && wTiles.YCord == unit.MapY)
                    {
                        winTilesTeam1Occupied++;
                    }
                }
            }

            //check to see how many of the win tiles have been captured by team 2
            int winTilesTeam2Occupied = 0;
            foreach (MapTile wTiles in winTiles)
            {
                foreach (Unit unit in college2.Units)
                {
                    if (wTiles.XCord == unit.MapX && wTiles.YCord == unit.MapY)
                    {
                        winTilesTeam2Occupied++;
                    }
                }
            }
            
            //at the end of all this update capture turns and check to see if either team has won
            if(turn == 1)
            {
                //if all 4 win tiles have been captured by team 1 add 1 to the team 1 win check
                if (winTilesTeam1Occupied == 4)
                {
                    college1WintTileCaptureTurns += 1;
                }
                //capture turns have to be back to back so if this isn't true reset the team 1 win check
                else
                {
                    college1WintTileCaptureTurns = 0;
                }

                //a team can only win after 2 full turns so only check for a win after the other teams turn
                if (college2WintTileCaptureTurns == 2)
                {
                    college1WintTileCaptureTurns = 0;
                    college2WintTileCaptureTurns = 0;
                    college2Win = true;
                }
            }
            else if(turn == 2)
            {
                //if all 4 win tiles have been captured by team 2 add 1 to the team 2 win check
                if (winTilesTeam2Occupied == 4)
                {
                    college2WintTileCaptureTurns += 1;
                }
                //capture turns have to be back to back so if this isn't true reset the team 2 win check
                else
                {
                    college2WintTileCaptureTurns = 0;
                }

                //a team can only win after 2 full turns so only check for a win after the other teams turn
                if (college1WintTileCaptureTurns == 2)
                {
                    college1WintTileCaptureTurns = 0;
                    college2WintTileCaptureTurns = 0;
                    college1Win = true;
                }
            }
            
        } 

        //removes dead units frrom the board and updates their status
        public void RemoveDeadUnits()
        {
            if (turn == 1) //check for dead units on team 2
            {
                foreach (Unit unit in college2.Units)
                {
                    //if the unit was alive but is now dead
                    if (unit.Alive && unit.CurrHealth <= 0)
                    {
                        //remove the unit from the board
                        mainMap.GetTile(unit.MapY, unit.MapX).Filled = false;

                        //update unit info
                        unit.Alive = false;
                        unit.MapX = 99;
                        unit.MapY = 99;
                    }
                }    
            }
            else if (turn == 2) //check for dead units on team 1
            {
                foreach (Unit unit in college1.Units)
                {
                    //if the unit was alive but is now dead
                    if (unit.Alive && unit.CurrHealth <= 0)
                    {
                        //remove the unit from the board
                        mainMap.GetTile(unit.MapY, unit.MapX).Filled = false;

                        //update unit info
                        unit.Alive = false;
                        unit.MapX = 99;
                        unit.MapY = 99;
                    }
                }
            }
            
        }

        //sets the startin of units onto the board
        public void SetUnits()
        {
            //set up starting positions
            //set default starting positions for team 1
            for (int i = 0; i < 10; i++)
            {
                college1.Units[i].MapX = 0;
                college1.Units[i].MapY = i;
                mainMap.GetTile(i, 0).Filled = true;
            }

            //set default starting positions
            for (int i = 0; i < 10; i++)
            {
                college2.Units[i].MapX = 9;
                college2.Units[i].MapY = i;
                mainMap.GetTile(i, 9).Filled = true;
            }
        }
    }

}
