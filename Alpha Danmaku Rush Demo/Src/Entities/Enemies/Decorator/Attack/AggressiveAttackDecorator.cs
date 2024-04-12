using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack;

public class AggressiveAttackDecorator : EnemyDecorator
{
    public AggressiveAttackDecorator(IEnemy enemy) : base(enemy) { }



    public override void Attack(GameTime gameTime, Vector2 playerPosition)
    {
        base.Attack(gameTime, playerPosition);
    }
}
