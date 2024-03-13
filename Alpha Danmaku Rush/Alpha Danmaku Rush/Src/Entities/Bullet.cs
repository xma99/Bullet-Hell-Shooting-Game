namespace Alpha_Danmaku_Rush.Src.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Drawing;
    using Rectangle = Microsoft.Xna.Framework.Rectangle;

    public class Bullet
    {
        public Texture2D Texture { get; private set; } // 子弹的纹理
        public Vector2 Position { get; set; } // 子弹的位置
        public Vector2 Velocity { get; set; } // 子弹的速度
        public bool IsActive { get; set; } // 指示子弹是否活跃（在屏幕上）
        public Vector2 Size { get; set; } // 大小

        // 碰撞箱
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public Bullet(Texture2D texture)
        {
            Texture = texture;
            IsActive = true;

            // 设置子弹大小
            Size = new Vector2(30, 30);
        }

        // 更新子弹状态
        public void Update(GameTime gameTime)
        {
            // 根据速度更新位置
            Position += Velocity;

            // 如果子弹移出屏幕，标记为非活跃
            if (Position.Y < 0)
            {
                IsActive = false;
            }
        }

        // 绘制子弹
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
                spriteBatch.Draw(Texture, destinationRectangle, Microsoft.Xna.Framework.Color.White);
            }
        }
    }
}
