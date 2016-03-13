using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO; //needed for file IO
using System;

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

        Map mainMap;
        College college1;
        College college2;

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
            //check for the current game state
            switch (curState)
            {
                case GameState.Menu:
                    break;

                case GameState.MapSelect:
                    
                    break;

                case GameState.TeamSelect:

                    break;

                case GameState.Game:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //check for the current game state
            switch (curState)
            {
                case GameState.Menu:
                    break;

                case GameState.MapSelect:
                    break;

                case GameState.TeamSelect:
                    break;

                case GameState.Game:
                    break;
            }

            base.Draw(gameTime);
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
                    newTiles[1, col] = new MapTile(1, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 2
                {
                    newTiles[2, col] = new MapTile(2, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 3
                {
                    newTiles[3, col] = new MapTile(3, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 4
                {
                    newTiles[4, col] = new MapTile(4, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 5
                {
                    newTiles[5, col] = new MapTile(5, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 6
                {
                    newTiles[6, col] = new MapTile(6, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 7
                {
                    newTiles[7, col] = new MapTile(7, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 8
                {
                    newTiles[8, col] = new MapTile(8, col, splitLine0[col]);
                }
                for (int col = 0; col < 10; col++)//row 9
                {
                    newTiles[9, col] = new MapTile(9, col, splitLine0[col]);
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
    }
}
