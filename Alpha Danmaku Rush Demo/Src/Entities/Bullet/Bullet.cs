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

    protected Bullet(Texture2D sprite, Vector2 position, Vector2 velocity, Color color)
    {
        Sprite = sprite;
        Position = position;
        Velocity = velocity;
        Color = color;
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
