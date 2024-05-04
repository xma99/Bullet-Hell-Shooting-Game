using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Function;

public class StealthModeDecorator : EnemyDecorator
{
    public StealthModeDecorator(IEnemy enemy) : base(enemy) { }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite, Position, new Color(255, 255, 255, 128));
    }
}
