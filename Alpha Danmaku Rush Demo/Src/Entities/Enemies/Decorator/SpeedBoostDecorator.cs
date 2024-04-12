using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator;

public class SpeedBoostDecorator : EnemyDecorator
{
    public SpeedBoostDecorator(IEnemy enemy) : base(enemy) { }

    public override void Update(GameTime gameTime, Vector2 playerPosition)
    {
        Vector2 oldPosition = this.Position;
        base.Update(gameTime, playerPosition);
        Vector2 newPosition = this.Position + (this.Position - oldPosition);
        this.Position = newPosition;
    }
}
