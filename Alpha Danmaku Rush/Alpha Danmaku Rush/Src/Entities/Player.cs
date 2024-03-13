namespace Alpha_Danmaku_Rush.Src.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class Player
{
    public Texture2D Texture { get; private set; } // 玩家纹理
    public Vector2 Position { get; set; } // 玩家位置
    private Vector2 _velocity; // 玩家移动速度
    private const float Speed = 2f; // 玩家移动速度常数
    public Vector2 Size { get; set; } // 玩家大小


    private Texture2D bulletTexture; // 子弹的纹理
    public List<Bullet> Bullets { get; private set; } // 玩家发射的子弹列表


    // 碰撞箱
    public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);


    public Player()
    {
        Position = Vector2.Zero;
        _velocity = Vector2.Zero;
        Size = new Vector2(100,100);

        Bullets = new List<Bullet>();
    }

    // 加载纹理
    public void LoadContent(Texture2D playerTexture, Texture2D bulletTexture)
    {
        Texture = playerTexture;

        this.bulletTexture = bulletTexture;
    }

    // 更新玩家状态
    public void Update(GameTime gameTime)
    {
        _velocity = Vector2.Zero;

        // 玩家控制
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
            _velocity.Y = -Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
            _velocity.Y = Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
            _velocity.X = -Speed;
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
            _velocity.X = Speed;

        Position += _velocity;


        // 处理子弹发射
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            Shoot();
        }

        // 更新子弹
        foreach (var bullet in Bullets)
        {
            bullet.Update(gameTime);
        }
        Bullets.RemoveAll(b => !b.IsActive); // 移除非活跃的子弹


        // 更新玩家位置
        Vector2 newPosition = Position;

        // 限制玩家在屏幕内移动
        newPosition.X = MathHelper.Clamp(newPosition.X, 0, 1024 - Size.X);
        newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, 768 - Size.Y);

        // 更新 Position 属性
        Position = newPosition;
    }

    // 绘制玩家
    public void Draw(SpriteBatch spriteBatch)
    {
        if (Texture != null)
        {
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(Texture, destinationRectangle, Color.White);
        }

        // 绘制子弹
        foreach (var bullet in Bullets)
        {
            bullet.Draw(spriteBatch);
        }
    }


    private void Shoot()
    {
        var newBullet = new Bullet(bulletTexture)
        {
            Position = new Vector2(Position.X + Texture.Width / 2 - bulletTexture.Width / 2, Position.Y),
            Velocity = new Vector2(0, -5)
        };
        Bullets.Add(newBullet);
    }
}

