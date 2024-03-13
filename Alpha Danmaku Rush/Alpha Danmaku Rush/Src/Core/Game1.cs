using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Alpha_Danmaku_Rush.Src.Core
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // 玩家的纹理和位置
        private Texture2D playerTexture;
        private Vector2 playerPosition;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Assets";
            IsMouseVisible = true;

            // 设置游戏窗口的大小
            _graphics.PreferredBackBufferWidth = 1024;  // 游戏窗口的宽度
            _graphics.PreferredBackBufferHeight = 768; // 游戏窗口的高度
        }

        protected override void Initialize()
        {
            // 初始化玩家位置
            playerPosition = new Vector2(100, 100);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // 加载玩家纹理
            playerTexture = Content.Load<Texture2D>("Images/player"); // "player.png"的纹理文件在Content目录,使用Content.mgcb编译
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // 玩家控制逻辑
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
                playerPosition.Y -= 2;
            if (keyboardState.IsKeyDown(Keys.Down))
                playerPosition.Y += 2;
            if (keyboardState.IsKeyDown(Keys.Left))
                playerPosition.X -= 2;
            if (keyboardState.IsKeyDown(Keys.Right))
                playerPosition.X += 2;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // 绘制玩家
            _spriteBatch.Begin();
            _spriteBatch.Draw(playerTexture, playerPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
