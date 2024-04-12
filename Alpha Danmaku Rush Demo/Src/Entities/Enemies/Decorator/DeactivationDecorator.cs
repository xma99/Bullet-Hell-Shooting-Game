using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator;

public class DeactivationDecorator : EnemyDecorator
{
    public DeactivationDecorator(IEnemy enemy) : base(enemy) { }

    public override void Update(GameTime gameTime, Vector2 playerPosition)
    {
        if (DecoratedEnemy.Position.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
        {
            DecoratedEnemy.IsActive = false; // Deactivate if off-screen
        }
        base.Update(gameTime, playerPosition);
    }
}
