using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
public class BossAbility2 : Bullet
{
    public BossAbility2(Texture2D sprite, Vector2 position, Vector2 velocity, Color color, Vector2 playerPosition)
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
}
