using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

public class MidBossBullet : Bullet
{
    Random random= new Random();
    public MidBossBullet(Texture2D sprite, Vector2 position, Vector2 velocity, Color color)
            : base(sprite, position, velocity, color)
    {
        Damage = 2;
    }

    public override void Update(GameTime gameTime = null)
    {
        CheckOffScreen();
        Vector2 defaultT = new Vector2(0, 1);
        Position += defaultT * 5.0f;
    }
    public void update1(float s=0)
    {
        CheckOffScreen();
        //Vector2 Ability1_Target = new Vector2(Velocity, 1);
        if (s <= 0)
            Position += Velocity * 0.5f;
        else
            Position += Velocity * s;
    }
}

