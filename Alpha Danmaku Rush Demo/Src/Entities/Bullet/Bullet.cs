namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Bullet
{
    protected Texture2D Sprite;

    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Color Color { get; set; }
    public bool IsActive { get; set; } = true;

    public int Damage { get; set; }

    public int Speed { get; set; }

    public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);

    protected Bullet(Texture2D sprite, Vector2 position, Vector2 velocity, Color color)
    {
        Sprite = sprite;
        Position = position;
        Velocity = velocity;
        Color = color;
        IsActive = false;
    }

    public abstract void Update(GameTime gameTime);

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            spriteBatch.Draw(Sprite, Position, Color);
        }
    }
}
