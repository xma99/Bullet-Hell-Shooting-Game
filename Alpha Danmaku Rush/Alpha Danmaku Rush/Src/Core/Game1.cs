using Alpha_Danmaku_Rush.Src.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Alpha_Danmaku_Rush.Src.Core
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private Player _player; // 添加玩家对象


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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // 加载玩家纹理 "player.png"的纹理文件在Content目录,使用Content.mgcb编译
            Texture2D playerTexture = Content.Load<Texture2D>("Images/player");
            Texture2D bulletTexture = Content.Load<Texture2D>("Images/bullet"); // 加载子弹纹理

            _player = new Player();
            _player.LoadContent(playerTexture, bulletTexture);
            _player.Position = new Vector2(100, 100); // 设置初始位置


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // 更新玩家状态
            _player.Update(gameTime);


            // 更新游戏状态
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // 绘制玩家
            _player.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
