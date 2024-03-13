using Alpha_Danmaku_Rush.Src.Entities;
using Alpha_Danmaku_Rush.Src.Entities.Enemy;
using Alpha_Danmaku_Rush.Src.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System.Collections.Generic;

namespace Alpha_Danmaku_Rush.Src.Core
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;


        private Player _player; // 添加玩家对象


        // 关卡管理器
        private LevelManager levelManager;


        // 敌人
        private Texture2D enemyTexture;


        // 碰撞管理器
        private CollisionManager collisionManager;


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
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            // 加载玩家纹理 "player.png"的纹理文件在Content目录,使用Content.mgcb编译
            Texture2D playerTexture = Content.Load<Texture2D>("Images/player");
            Texture2D bulletTexture = Content.Load<Texture2D>("Images/bullet"); // 加载子弹纹理
            enemyTexture = Content.Load<Texture2D>("Images/enemy"); // "enemy"的敌人纹理


            // 初始化LevelManager，提供敌人纹理和关卡数据文件的路径
            levelManager = new LevelManager(enemyTexture, "Levels/levels.json");


            _player = new Player();
            _player.LoadContent(playerTexture, bulletTexture);
            _player.Position = new Vector2(100, 100); // 设置初始位置


            collisionManager = new CollisionManager(_player, levelManager.enemies);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // 更新玩家状态
            _player.Update(gameTime);


            // 更新关卡
            levelManager.Update(gameTime);


            collisionManager.Update();


            // 更新游戏状态
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // 绘制玩家
            _player.Draw(_spriteBatch);



            // 绘制由LevelManager管理的敌人
            foreach (var enemy in levelManager.enemies)
            {
                if (enemy.IsActive)
                {
                    enemy.Draw(_spriteBatch);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
