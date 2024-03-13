using Alpha_Danmaku_Rush.Src.Entities;
using Alpha_Danmaku_Rush.Src.Entities.Enemy;
using Alpha_Danmaku_Rush.Src.Managers;
using Alpha_Danmaku_Rush.Src.UI;
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


        // 分数管理器
        private ScoreManager scoreManager;


        // HUD
        private HUD hud;


        // SceneManager
        SceneManager sceneManager;


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
            // 创建实例
            scoreManager = new ScoreManager();

            // 初始化LevelManager，提供敌人和关卡数据文件的路径
            levelManager = new LevelManager(enemyTexture, "Levels/levels.json");
            sceneManager = new SceneManager(levelManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            // 加载玩家纹理 "player.png"的纹理文件在Content目录,使用Content.mgcb编译
            Texture2D playerTexture = Content.Load<Texture2D>("Images/player");
            Texture2D bulletTexture = Content.Load<Texture2D>("Images/bullet"); // 加载子弹纹理
            enemyTexture = Content.Load<Texture2D>("Images/enemy"); // "enemy"的敌人纹理
            var font = Content.Load<SpriteFont>("Fonts/Font");


            _player = new Player();
            _player.LoadContent(playerTexture, bulletTexture);
            _player.Position = new Vector2(100, 100); // 设置初始位置


            // 初始化HUD
            hud = new HUD(font, _player, scoreManager);

            collisionManager = new CollisionManager(_player, levelManager.enemies, scoreManager);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // 更新玩家状态
            _player.Update(gameTime);


            // 更新关卡
            levelManager.Update(gameTime);

            // 更新碰撞检测
            collisionManager.Update();


            // 更新分数管理器
            scoreManager.Update(gameTime);

            // 更新场景管理器
            sceneManager.Update(gameTime);

            // 更新游戏状态
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // 绘制玩家
            _player.Draw(_spriteBatch);

            // 绘制HUD
            hud.Draw(_spriteBatch);

            // 绘制场景管理器
            sceneManager.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
