using Microsoft.Xna.Framework;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack;

public class TimedAttackDecorator : EnemyDecorator
{
    private TimeSpan attackInterval;
    private TimeSpan attackTimer;

    public TimedAttackDecorator(IEnemy enemy, TimeSpan attackInterval) : base(enemy)
    {
        this.attackInterval = attackInterval;
    }

    public override void Attack(GameTime gameTime, Vector2 playerPosition)
    {
        attackTimer += gameTime.ElapsedGameTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = TimeSpan.Zero; // Reset timer
            // Execute attack logic
            Vector2 direction = Vector2.Normalize(playerPosition - DecoratedEnemy.Position);
            Vector2 bulletVelocity = direction * 8f; // Example speed
            // Potentially activate a bullet here
        }
        base.Attack(gameTime, playerPosition);
    }
}
