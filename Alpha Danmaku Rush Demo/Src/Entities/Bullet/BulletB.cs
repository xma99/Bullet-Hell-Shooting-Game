using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

public class BulletB : Bullet
{
    public BulletB(Texture2D sprite, Vector2 position, Vector2 velocity, Color color)
        : base(sprite, position, velocity, color)
    {
    }

    public override void Update(GameTime gameTime)
    {
        // Example update logic for BulletB, potentially different from BulletA
        Position += Velocity * gameTime.ElapsedGameTime.Milliseconds / 1000f;
    }
}