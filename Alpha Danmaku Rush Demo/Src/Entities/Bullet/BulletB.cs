using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

public class BulletB : Bullet
{
    public BulletB(Texture2D sprite, Vector2 position, Vector2 velocity, Color color)
        : base(sprite, position, velocity, color)
    {
        Damage = 2;
    }

    public override void Update(GameTime gameTime=null)
    {
        // Example update logic for BulletB, potentially different from BulletA
        Vector2 defaultT=new Vector2(0,1);
        Position += defaultT * 10.0f;
    }
}