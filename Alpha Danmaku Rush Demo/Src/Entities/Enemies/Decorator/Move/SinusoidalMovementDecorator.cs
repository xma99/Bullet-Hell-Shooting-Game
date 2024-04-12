using Microsoft.Xna.Framework;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Move;

public class SinusoidalMovementDecorator : EnemyDecorator
{
    private float amplitude;

    public SinusoidalMovementDecorator(IEnemy enemy, float amplitude) : base(enemy)
    {
        this.amplitude = amplitude;
    }

    public override void Update(GameTime gameTime, Vector2 playerPosition)
    {
        float delta = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * amplitude;
        DecoratedEnemy.Position = new Vector2(DecoratedEnemy.Position.X + delta, DecoratedEnemy.Position.Y);
        base.Update(gameTime, playerPosition);
    }
}
