using System;

namespace Alpha_Danmaku_Rush.Src.Entities.Enemy;

using Alpha_Danmaku_Rush.Src.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class Enemy
{
    public Texture2D Texture { get; private set; } // 敌人的纹理
    public Vector2 Position { get; set; } // 敌人的位置
    public Vector2 Velocity { get; set; } // 敌人的速度
    public bool IsActive { get; set; } // 指示敌人是否活跃（在屏幕上）
    public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public Vector2 Size { get; set; } // 大小

    public Enemy(Texture2D texture, Vector2 position, Vector2 velocity)
    {
        Texture = texture;
        Position = position;
        Velocity = velocity;
        IsActive = true;
        Size = new Vector2(64, 64);
    }

    // 更新敌人状态
    public void Update(GameTime gameTime)
    {
        Position += Velocity;

        // 如果敌人移出屏幕
        if (Position.Y > 768) // 假设屏幕高度为768
        {
            Position = new Vector2(Position.X, 0);
        }

        if (Position.X > 1024)
        {
            Position = new Vector2(0, Position.Y);
        }
    }

    // 绘制敌人
    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(Texture, destinationRectangle, Microsoft.Xna.Framework.Color.White);
        }
    }
}
