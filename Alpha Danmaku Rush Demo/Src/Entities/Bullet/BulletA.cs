using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

public class BulletA : Bullet
{
    public BulletA(Texture2D sprite, Vector2 position, Vector2 velocity, Color color)
        : base(sprite, position, velocity, color)
    {
        Damage = 1;
    }

    public override void Update(GameTime gameTime)
    {
        // Example update logic for BulletA
        Position += Velocity * gameTime.ElapsedGameTime.Milliseconds / 1000f;
    }
}