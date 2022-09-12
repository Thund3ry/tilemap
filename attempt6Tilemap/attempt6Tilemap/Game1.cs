using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Collections.Generic;

namespace attempt6Tilemap
{        
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Camera camera;

        private Player player;

        private Song backgroundMusic;

        public const int screenWidth = 1028;
        public const int screenHeight = 256;

        private int buttonCount = 0;
        private ButtonCount buttonPressCount;

        private Texture2D square;        
        private Texture2D tileMap;
        private Texture2D sidebarTexture;

        private SideBar sidebar;
        private InputManager inputManager = new InputManager();

        private SettingsMenu settingsMenu = new SettingsMenu();
        private StartMenu startMenu = new StartMenu();
        private PauseMenu pauseMenu = new PauseMenu();

        private Sprite playButton;
        private Sprite settingsButton;
        private Sprite returnButton;
        private Sprite exitButton;
        private Sprite slowDown1;

        private int[,] level1Map = new int[64, 16];
        private int[,] level1Obstacles = new int[64, 16];

        private Rectangle stone1 = new Rectangle(272, 170, 16, 16);
        private Rectangle stone2 = new Rectangle(289, 170, 16, 16);
        private Rectangle stone3 = new Rectangle(306, 170, 16, 16);
        private Rectangle stone4 = new Rectangle(323, 170, 16, 16);
        private Rectangle stone5 = new Rectangle(340, 170, 16, 16);
        private Rectangle block1 = new Rectangle(17, 51, 16, 16);

        private Rectangle slow1 = new Rectangle(238, 204, 16, 16);

        private TilleMap firstLevel = new TilleMap();
        private ObstacleMap firstLevelObst = new ObstacleMap();

        private List<int>[,] nodesLists = new List<int>[64, 16];
        private List<int>[,] weightLists = new List<int>[64, 16];

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            //It's supposed to be 800, 800 // is now a constant go got line 14 & 15
            _graphics.ApplyChanges();
            //Changes the size of the window

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            backgroundMusic = Content.Load<Song>("Wii Music - Background Music");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);

            square = Content.Load<Texture2D>("Square");
            tileMap = Content.Load<Texture2D>("roguelikeDungeon_transparent");
            sidebarTexture = Content.Load<Texture2D>("SideBar");

            slowDown1 = new Sprite(new Vector2(16, 16), new Vector2(32, 32), tileMap);
            playButton = new Sprite(square, new Vector2(400, 200), new Vector2(200, 50));
            playButton.spriteColor = Color.OrangeRed;
            settingsButton = new Sprite(square, new Vector2(400, 130), new Vector2(200, 50));
            settingsButton.spriteColor = Color.OrangeRed;
            returnButton = new Sprite(square, new Vector2(400, 160), new Vector2(200, 50));
            returnButton.spriteColor = Color.BlueViolet;
            exitButton = new Sprite(square, new Vector2(400, 20), new Vector2(200, 50));
            exitButton.spriteColor = Color.PaleGreen;

            buttonPressCount = new ButtonCount(buttonCount);

            sidebar = new SideBar(sidebarTexture, new Vector2(0, 0), new Vector2(256, 512));

            camera = new Camera();

            player = new Player(square, new Vector2(64, 128), new Vector2(16, 64));

            firstLevel = new TilleMap(level1Map);
            firstLevelObst = new ObstacleMap(level1Obstacles);

            firstLevel.ReadLevel();//calls method to read the txt file and put it into
            firstLevelObst.ReadLevelObst();//an array so it can be drawn

            //intilizes the arrays that record the adjacency list
            //and the weights of each arc
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    nodesLists[i, j] = new List<int>();
                    weightLists[i, j] = new List<int>();
                }
            }

            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (i > 0 && firstLevelObst.levelMapObst[i - 1, j] == 48)
                    {
                        nodesLists[i, j].Add(i - 1);
                        nodesLists[i, j].Add(j);
                    }
                    if (i < 63 && firstLevelObst.levelMapObst[i + 1, j] == 48)
                    {
                        nodesLists[i, j].Add(i + 1);
                        nodesLists[i, j].Add(j);
                    }
                    if (j < 15 && firstLevelObst.levelMapObst[i, j + 1] == 48)
                    {
                        nodesLists[i, j].Add(i);
                        nodesLists[i, j].Add(j + 1);
                    }
                    if (j > 0 && firstLevelObst.levelMapObst[i, j - 1] == 48)
                    {
                        nodesLists[i, j].Add(i);
                        nodesLists[i, j].Add(j - 1);
                    }
                }
            }

            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (i > 0 && firstLevelObst.levelMapObst[i - 1, j] == 49)
                    {
                        weightLists[i, j].Add(1);
                    }
                    else
                    {
                        weightLists[i, j].Add(0);
                    }
                    if (i < 63 && firstLevelObst.levelMapObst[i + 1, j] == 49)
                    {
                        weightLists[i, j].Add(1);
                    }
                    else
                    {
                        weightLists[i, j].Add(0);
                    }
                    if (j < 15 && firstLevelObst.levelMapObst[i, j + 1] == 49)
                    {
                        weightLists[i, j].Add(1);
                    }
                    else
                    {
                        weightLists[i, j].Add(0);
                    }
                    if (j > 0 && firstLevelObst.levelMapObst[i, j - 1] == 49)
                    {
                        weightLists[i, j].Add(1);
                    }
                    else
                    {
                        weightLists[i, j].Add(0);
                    }
                }
            }

            startMenu.isActive = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (buttonPressCount.count != 0)
            {
                buttonPressCount.count--;
            }

            if (settingsMenu.isActive == false && startMenu.isActive == true)
            {
                if (inputManager.CheckButtonPressed(playButton.spritePosition, playButton.spriteSize,
                    buttonCount, buttonPressCount) == true && buttonPressCount.count < 100)
                {
                    startMenu.isActive = false;
                }
                if (inputManager.CheckButtonPressed(settingsButton.spritePosition, settingsButton.spriteSize,
                    buttonCount, buttonPressCount) == true && buttonPressCount.count < 100)
                {
                    settingsMenu.isActive = true;
                }
                if (inputManager.CheckButtonPressed(exitButton.spritePosition, exitButton.spriteSize,
                    buttonCount, buttonPressCount) == true && buttonPressCount.count < 100)
                {
                    Exit();
                }
            }
            if (settingsMenu.isActive == true)
            {
                if (inputManager.CheckButtonPressed(returnButton.spritePosition, returnButton.spriteSize,
                    buttonCount, buttonPressCount) == true && buttonPressCount.count < 100)
                {
                    settingsMenu.isActive = false;
                }
                if (inputManager.CheckButtonPressed(exitButton.spritePosition, exitButton.spriteSize,
                    buttonCount, buttonPressCount) == true && buttonPressCount.count < 100)
                {
                    Exit();
                }
            }
            if (settingsMenu.isActive == false && startMenu.isActive == false)
            {

            }
            if (pauseMenu.isActive == true)
            {

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Purple);

            // TODO: Add your drawing code here

            if (settingsMenu.isActive == false && startMenu.isActive == true)
            {
                GraphicsDevice.Clear(Color.DarkGoldenrod);
                playButton.DrawSprite(_spriteBatch, square);
                settingsButton.DrawSprite(_spriteBatch, square);
                exitButton.DrawSprite(_spriteBatch, square);
            }
            if (settingsMenu.isActive == true)
            {
                GraphicsDevice.Clear(Color.DarkTurquoise);
                returnButton.DrawSprite(_spriteBatch, square);
                exitButton.DrawSprite(_spriteBatch, square);
            }
            if (settingsMenu.isActive == false && startMenu.isActive == false && pauseMenu.isActive == false)
            {
                player.DrawSpriteM(_spriteBatch, square, camera);

                //calls the method to draws the background
                firstLevel.DrawLevel1(tileMap, _spriteBatch, camera, stone1, stone2, stone3, stone4, stone5);
                //calls the method to draw the obstacles and any sprites that are
                //drawn over the level back ground
                firstLevelObst.DrawLevel1Obst(tileMap, _spriteBatch, camera, block1);
                //sidebar.DrawSprite(_spriteBatch, sidebarTexture);
                //slowDown1.DraWSpriteTileMap(_spriteBatch, slow1);
            }
            if (pauseMenu.isActive == true)
            {
                settingsButton.DrawSprite(_spriteBatch, square);
            }

            

            base.Draw(gameTime);
        }
    }
}