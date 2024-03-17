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
        /// <summary>
        /// Gloable virable for bullet texture
        /// </summary>
       
        Texture2D enemyABullet;
        Texture2D enemyBBullet;
        Texture2D midBossBullet;
        Texture2D finalBossBullet;

        private List<HealthIcon> healthIcons;
        private int playerHealth;
        private Texture2D bulletTexture;

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

            // Initialize health icons
            healthIcons = new List<HealthIcon>();
            int iconSpacing = 1; // Spacing between health icons
            float scale = 0.03f; // Scale for the heart icon
            Texture2D heartTexture = Content.Load<Texture2D>("heart");
            int totalIconWidth = (int)(heartTexture.Width * scale);
            int totalSpacingWidth = iconSpacing * 9; // Total spacing width between 9 pairs of icons
            int totalRequiredWidth = 10 * totalIconWidth + totalSpacingWidth;
            int startPositionX = (GraphicsDevice.Viewport.Width - totalRequiredWidth);
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
            // Player's Bullet
            // bulletObject = Content.Load<Texture2D>("bullettest1");

            // Load the bullet texture
            // bulletTexture = Content.Load<Texture2D>("bubble");

            // Background image
            backImage = Content.Load<Texture2D>("back1");

            // Position: The player is shown in the center of the screen
            int windowWidth = GraphicsDevice.Viewport.Width;
            int windowHeight = GraphicsDevice.Viewport.Height;
            Vector2 initialPosition = new Vector2((windowWidth / 2) - (playerSet.Width / 2), windowHeight - playerSet.Height);
            player = new Player(playerSet, initialPosition);

            // Load the texture for enemy A
            Texture2D enemyASprite = Content.Load<Texture2D>("a");
            enemyABullet = Content.Load<Texture2D>("bullettest1");
            enemyBBullet = Content.Load<Texture2D>("bullettest1");
            midBossBullet = Content.Load<Texture2D>("bubble");
             finalBossBullet = Content.Load<Texture2D>("bubble");
            // Create an instance of the Enemy class for enemy A
            Vector2 enemyAPosition = new Vector2(windowWidth, windowHeight); // Set the initial position of enemy A
            float enemyAMovementSpeed = 10f;
            Enemy enemyA = new Enemy(enemyASprite, enemyAPosition, enemyAMovementSpeed, EnemyType.RegularA,enemyABullet);
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

        private void SpawnEnemyA()
        {
            Texture2D enemyASprite = Content.Load<Texture2D>("a");
            // Texture2D bubbleTexture = Content.Load<Texture2D>("bubble");
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            Vector2 spawnPosition = new Vector2(random.Next(screenWidth), random.Next(screenHeight));
            float enemySpeed = 3.0f; // Adjust enemy speed as needed
            enemies.Add(new Enemy(enemyASprite, spawnPosition, enemySpeed, EnemyType.RegularA,enemyABullet));
        }
        private void SpawnEnemyB()
        {
            Texture2D enemyASprite = Content.Load<Texture2D>("b");
            // Texture2D bubbleTexture = Content.Load<Texture2D>("bubble");
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            Vector2 spawnPosition = new Vector2(random.Next(screenWidth), random.Next(screenHeight));
            float enemySpeed = 5.0f; // Adjust enemy speed as needed
            enemies.Add(new Enemy(enemyASprite, spawnPosition, enemySpeed, EnemyType.RegularA, enemyBBullet));
        }
        private void SpawnEnemyM()
        {
            Texture2D midBossSprite = Content.Load<Texture2D>("midBoss");
            // Texture2D bubbleTexture = Content.Load<Texture2D>("bubble");
            int screenWidth = GraphicsDevice.Viewport.Width;
            // int screenHeight = GraphicsDevice.Viewport.Height;
            Vector2 spawnPosition = new Vector2((screenWidth / 2) - (midBossSprite.Width / 2), 0);
            float midBossSpeed = 3.0f; // Adjust enemy speed as needed
            Enemy midBoss = new Enemy(midBossSprite, spawnPosition, midBossSpeed, EnemyType.MidBoss,midBossBullet);
            enemies.Add(midBoss);
        }
        private void SpawnEnemyF()
        {
            Texture2D finalBossSprite = Content.Load<Texture2D>("finalBoss");
            // Texture2D bubbleTexture = Content.Load<Texture2D>("bubble");
            int screenWidth = GraphicsDevice.Viewport.Width;
            // int screenHeight = GraphicsDevice.Viewport.Height;
            Vector2 spawnPosition = new Vector2((screenWidth / 2) - (finalBossSprite.Width / 2), 0);
            float finalBossSpeed = 3.0f; // Adjust enemy speed as needed
            Enemy finalBoss = new Enemy(finalBossSprite, spawnPosition, finalBossSpeed, EnemyType.FinalBoss,finalBossBullet);
            enemies.Add(finalBoss);
        }
        private void ClearEnemy()
        {
            enemies.Clear();
        }
    }
}
