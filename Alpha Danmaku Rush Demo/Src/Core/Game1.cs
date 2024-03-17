using Alpha_Danmaku_Rush_Demo.Src.Entities;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;
using Alpha_Danmaku_Rush_Demo.Src.Managers;
using Alpha_Danmaku_Rush_Demo.Src.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Alpha_Danmaku_Rush_Demo.Src.Core
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Player
        private Player player;

        // private Texture2D background;
        Texture2D backImage;

        // Camera: following the player
        private Camera camera;
        

        // Enemy variables
        private Random random;
        private TimeSpan spawnTimer;
        private TimeSpan spawnIntervalMin;
        private TimeSpan spawnIntervalMax;
        private TimeSpan gameStartTime;
        bool midCheck = false;
        bool finalCheck = false;
        bool midPass = false;
        bool midClear = false;

        Texture2D enemyABullet;
        Texture2D enemyBBullet;
        Texture2D midBossBullet;
        Texture2D finalBossBullet;

        private List<HealthIcon> healthIcons;
        private int playerHealth;
        private Texture2D bulletTexture;

        private EnemyManager enemyManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            enemyManager = new EnemyManager();


            // Changing the window size setting
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();

            // Camera setting
            camera = new Camera(GraphicsDevice.Viewport);

            // Initialize enemy variables
            random = new Random();
            spawnIntervalMin = TimeSpan.FromSeconds(5);
            spawnIntervalMax = TimeSpan.FromSeconds(15);
            spawnTimer = TimeSpan.Zero;
            gameStartTime = TimeSpan.Zero;

            // Initialize health icons
            healthIcons = new List<HealthIcon>();
            int iconSpacing = 1; // Spacing between health icons
            float scale = 0.03f; // Scale for the heart icon
            Texture2D heartTexture = Content.Load<Texture2D>("heart");
            int totalIconWidth = (int)(heartTexture.Width * scale);
            int totalSpacingWidth = iconSpacing * 9; // Total spacing width between 9 pairs of icons
            int totalRequiredWidth = 10 * totalIconWidth + totalSpacingWidth;
            int startPositionX = GraphicsDevice.Viewport.Width - totalRequiredWidth;
            for (int i = 0; i < 10; i++)
            {
                Vector2 iconPosition = new Vector2(startPositionX + (totalIconWidth + iconSpacing) * i, 20);
                healthIcons.Add(new HealthIcon(heartTexture, iconPosition, scale, isActive: true));
            }
            playerHealth = 10; // Set initial player health

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;

            // Player
            Texture2D playerSet = Content.Load<Texture2D>("testplayer1");

            // Background image
            backImage = Content.Load<Texture2D>("back1");

            // Position: The player is shown in the center of the screen
            int windowWidth = GraphicsDevice.Viewport.Width;
            int windowHeight = GraphicsDevice.Viewport.Height;
            Vector2 initialPosition = new Vector2(windowWidth / 2 - playerSet.Width / 2, windowHeight - playerSet.Height);
            player = new Player(playerSet, initialPosition);

            // Load the texture
            enemyABullet = Content.Load<Texture2D>("bullettest1");
            enemyBBullet = Content.Load<Texture2D>("bullettest1");
            midBossBullet = Content.Load<Texture2D>("bubble");
            finalBossBullet = Content.Load<Texture2D>("bubble");
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

            // MidBoss show
            if (!midCheck && gameTimeElapsed >= TimeSpan.FromSeconds(48) && gameTimeElapsed <= TimeSpan.FromSeconds(75))
            {
                SpawnEnemyM();
                midCheck = true;
            }

            // FinalBoss show
            if (gameTimeElapsed >= TimeSpan.FromSeconds(92) && !finalCheck)
            {
                SpawnEnemyF();
                finalCheck = true;
            }

            if (gameTimeElapsed >= TimeSpan.FromMinutes(3))
            {
                Exit();
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

            // Update player health based on collisions with enemy bullets
            foreach (var enemy in enemies)
            {
                foreach (var attack in enemy.attackList)
                {
                    if (attack.checkAttack && Vector2.Distance(player.Position, attack.bulletPosition) < 7) // Adjust the collision detection radius as needed
                    {
                        attack.checkAttack = false; // Prevent multiple collisions with the same bullet
                        playerHealth--; // Decrement player health
                        if (playerHealth <= 0)
                        {
                            // Game over logic
                        }
                        else
                        {
                            // Find the first active heart icon and remove it
                            for (int i = 0; i < healthIcons.Count; i++)
                            {
                                if (healthIcons[i].IsActive)
                                {
                                    healthIcons[i].IsActive = false;
                                    break; // Exit the loop after removing one heart icon
                                }
                            }
                        }
                        break;
                    }
                }
            }

            // Update positions of health icons
            foreach (var icon in healthIcons)
            {
                icon.UpdatePosition(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // display images
            // _spriteBatch.Begin();
            _spriteBatch.Begin(transformMatrix: camera.GetMatrix());

            // Draw the background image to fit the screen
            _spriteBatch.Draw(backImage, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            // Draw health icons
            foreach (var icon in healthIcons)
            {
                icon.Draw(_spriteBatch);
            }

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
    }
}
