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

        // Enemy variables
        private Random random;
        private TimeSpan spawnIntervalMin;
        private TimeSpan spawnIntervalMax;
        bool midCheck = false;
        bool finalCheck = false;
        bool midPass = false;
        bool midClear = false;

        private List<HealthIcon> healthIcons;

        private EnemyManager enemyManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            enemyManager = new EnemyManager(Content, _graphics);

            // Changing the window size setting
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 1000;

            _graphics.ApplyChanges();
            

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

            
        }

        protected override void Update(GameTime gameTime)
        {

            TimeSpan gameTimeElapsed = gameTime.TotalGameTime - TimeSpan.Zero;

            // Exit game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Player move
            player.Update(gameTime, GraphicsDevice.Viewport.Width);

            //Important: Spawn Logic need to be change later

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
            _spriteBatch.Begin();

            // Draw the background image to fit the screen
            _spriteBatch.Draw(backImage, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            // Draw health icons
            foreach (var icon in healthIcons)
            {
                icon.Draw(_spriteBatch);
            }

            /*_spriteBatch.Draw(player, _position, Color.White);*/
            player.Draw(_spriteBatch);

            // Draw enemies
            enemyManager.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private TimeSpan GetRandomSpawnInterval() // Timer
        {
            return TimeSpan.FromSeconds(random.NextDouble() * (spawnIntervalMax.TotalSeconds - spawnIntervalMin.TotalSeconds) + spawnIntervalMin.TotalSeconds);
        }
    }
}
