namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Bullet
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Color Color { get; set; }
    public bool IsActive { get; set; } = true;

    protected Bullet(Vector2 position, Vector2 velocity, Color color)
    {
        Position = position;
        Velocity = velocity;
        Color = color;
    }

    public abstract void Update(GameTime gameTime);

    public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        if (IsActive)
        {
            spriteBatch.Draw(texture, Position, Color);
        }
    }
}
