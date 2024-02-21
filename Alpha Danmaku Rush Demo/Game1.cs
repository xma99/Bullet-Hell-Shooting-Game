using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Alpha_Danmaku_Rush_Demo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // Player
        private Player player;
        // Bullet
        // private Texture2D bulletObject;

        // private Texture2D background;
        Texture2D backImage;

        // Camera: following the player
        private Camera camera;

        private Enemy enemyA;
        // Enemy variables
        private List<Enemy> enemies;
        private Random random;
        private TimeSpan spawnTimer;
        private TimeSpan spawnIntervalMin;
        private TimeSpan spawnIntervalMax;
        private TimeSpan gameStartTime;
        bool midCheck = false;
        bool finalCheck = false;
        bool midPass = false;
        bool midClear = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Changing the window size setting
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            // Camera setting
            camera = new Camera(GraphicsDevice.Viewport);

            // Initialize enemy variables
            enemies = new List<Enemy>();
            random = new Random();
            spawnIntervalMin = TimeSpan.FromSeconds(5);
            spawnIntervalMax = TimeSpan.FromSeconds(15);
            spawnTimer = TimeSpan.Zero;
            gameStartTime = TimeSpan.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;
            backImage = new Texture2D(GraphicsDevice, width, 1);

            // From green to blue
            Color[] setColor = new Color[width];
            for (int i = 0; i < width; i++)
            {
                float ratio = (float)i / width;
                setColor[i] = new Color(0, 1 - ratio, ratio);
            }

            backImage.SetData(setColor);


            // Player
            Texture2D playerSet = Content.Load<Texture2D>("testplayer1");
            // Player's Bullet
            // bulletObject = Content.Load<Texture2D>("bullettest1");

            // Background image
            // background = Content.Load<Texture2D>("back1");

            // Position: The player is shown in the center of the screen
            int windowWidth = GraphicsDevice.Viewport.Width;
            int windowHeight = GraphicsDevice.Viewport.Height;
            Vector2 initialPosition = new Vector2((windowWidth / 2) - (playerSet.Width / 2), windowHeight - playerSet.Height);
            player = new Player(playerSet, initialPosition);

            // Load the texture for enemy A
            Texture2D enemyASprite = Content.Load<Texture2D>("a");
            // Create an instance of the Enemy class for enemy A
            Vector2 enemyAPosition = new Vector2(windowWidth, windowHeight); // Set the initial position of enemy A
            float enemyAMovementSpeed = 10f;
            Enemy enemyA = new Enemy(enemyASprite, enemyAPosition, enemyAMovementSpeed, EnemyType.RegularA);
        }

        protected override void Update(GameTime gameTime)
        {

            TimeSpan gameTimeElapsed = gameTime.TotalGameTime - gameStartTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Player move
            player.Update(gameTime, GraphicsDevice.Viewport.Width);

            // Then camera follow the player
            //camera.FollowThePlayer(player);

            // Update spawn timer
            spawnTimer += gameTime.ElapsedGameTime;

            if (!midCheck && gameTimeElapsed >= TimeSpan.FromSeconds(48) && gameTimeElapsed <= TimeSpan.FromSeconds(75))
            {
                SpawnEnemyM();
                midCheck = true;
            }
            //Important: Spawn Logic need to be change later
            // Spawn enemy A if spawn interval is reached

            if (gameTimeElapsed <= TimeSpan.FromSeconds(25))
            {
                if (spawnTimer >= GetRandomSpawnInterval())
                {
                    SpawnEnemyA();
                    spawnTimer = TimeSpan.Zero; // Reset timer

                }
                if (spawnTimer >= GetRandomSpawnInterval())
                {
                    SpawnEnemyB();
                    spawnTimer = TimeSpan.Zero; // Reset timer

                }
            }
            if (gameTimeElapsed >= TimeSpan.FromSeconds(30) && midCheck == false)
            {
                midCheck = true;
                ClearEnemy();

                SpawnEnemyM();

            }
            if (gameTimeElapsed >= TimeSpan.FromSeconds(45) && midPass == false)
            {
                if (midClear == false)
                {
                    ClearEnemy();
                    midClear = true;
                }

                if (spawnTimer >= GetRandomSpawnInterval())
                {
                    SpawnEnemyA();
                    spawnTimer = TimeSpan.Zero; // Reset timer

                }
                if (spawnTimer >= GetRandomSpawnInterval())
                {
                    SpawnEnemyB();
                    spawnTimer = TimeSpan.Zero; // Reset timer

                }
            }
            if (gameTimeElapsed >= TimeSpan.FromSeconds(60) && finalCheck == false)
            {
                midPass = true;
                finalCheck = true;
                ClearEnemy();
                SpawnEnemyF();
            }
            if (gameTimeElapsed == TimeSpan.FromSeconds(120))
            {
                ClearEnemy();
            }
            // Update enemies
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime, player.Position);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // display images
            // _spriteBatch.Begin();
            _spriteBatch.Begin(transformMatrix: camera.GetMatrix());
            _spriteBatch.Draw(backImage, new Rectangle(0, 0, backImage.Width, GraphicsDevice.Viewport.Height), Color.White);

            /*_spriteBatch.Draw(player, _position, Color.White);*/
            player.Draw(_spriteBatch);

            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private TimeSpan GetRandomSpawnInterval() // Timer
        {
            return TimeSpan.FromSeconds(random.NextDouble() * (spawnIntervalMax.TotalSeconds - spawnIntervalMin.TotalSeconds) + spawnIntervalMin.TotalSeconds);
        }

        private void SpawnEnemyA()
        {
            Texture2D enemyASprite = Content.Load<Texture2D>("a");
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            Vector2 spawnPosition = new Vector2(random.Next(screenWidth), random.Next(screenHeight));
            float enemySpeed = 3.0f; // Adjust enemy speed as needed
            enemies.Add(new Enemy(enemyASprite, spawnPosition, enemySpeed, EnemyType.RegularA));
        }
        private void SpawnEnemyB()
        {
            Texture2D enemyASprite = Content.Load<Texture2D>("b");
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            Vector2 spawnPosition = new Vector2(random.Next(screenWidth), random.Next(screenHeight));
            float enemySpeed = 5.0f; // Adjust enemy speed as needed
            enemies.Add(new Enemy(enemyASprite, spawnPosition, enemySpeed, EnemyType.RegularA));
        }
        private void SpawnEnemyM()
        {
            Texture2D midBossSprite = Content.Load<Texture2D>("midBoss");
            int screenWidth = GraphicsDevice.Viewport.Width;
            // int screenHeight = GraphicsDevice.Viewport.Height;
            Vector2 spawnPosition = new Vector2((screenWidth / 2) - (midBossSprite.Width / 2), 0);
            float midBossSpeed = 3.0f; // Adjust enemy speed as needed
            Enemy midBoss = new Enemy(midBossSprite, spawnPosition, midBossSpeed, EnemyType.MidBoss);
            enemies.Add(midBoss);
        }
        private void SpawnEnemyF()
        {
            Texture2D enemyASprite = Content.Load<Texture2D>("b");
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            Vector2 spawnPosition = new Vector2(random.Next(screenWidth), random.Next(screenHeight));
            float enemySpeed = 3.0f; // Adjust enemy speed as needed
            enemies.Add(new Enemy(enemyASprite, spawnPosition, enemySpeed, EnemyType.RegularA));
        }
        private void ClearEnemy()
        {
            enemies.Clear();
        }
    }
}
