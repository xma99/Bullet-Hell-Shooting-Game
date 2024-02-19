using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Player move
            player.Update(gameTime, GraphicsDevice.Viewport.Width);

            // Then camera follow the player
            camera.FollowThePlayer(player);

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
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}