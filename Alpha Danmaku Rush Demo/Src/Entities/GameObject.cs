using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities;

public class GameObject
{
    public Texture2D Sprite { get; protected set; }
    public Vector2 Position { get; set; }

    public Vector2 Velocity { get; set; }

    public bool IsActive { get; set; } = true;

    public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);

    public GameObject(Texture2D sprite, Vector2 initialPosition)
    {
        Sprite = sprite;
        Position = initialPosition;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}