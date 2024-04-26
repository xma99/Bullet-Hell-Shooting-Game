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
        CheckOffScreen();
        // Example update logic for BulletA
        Vector2 defaultT = new Vector2(0, 1);
        //Position +=defaultT * gameTime.ElapsedGameTime.Milliseconds / 1000f;
        Position += defaultT * 5.0f ;
    }
}

