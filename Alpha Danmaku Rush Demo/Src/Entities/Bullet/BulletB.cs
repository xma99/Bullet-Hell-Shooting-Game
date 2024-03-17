using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

public class BulletB : Bullet
{
    public BulletB(Vector2 position, Vector2 velocity, Color color)
        : base(position, velocity, color)
    {
    }

    public override void Update(GameTime gameTime)
    {
        // Example update logic for BulletB, potentially different from BulletA
        Position += Velocity * gameTime.ElapsedGameTime.Milliseconds / 1000f;
    }
}