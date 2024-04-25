using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator;

public abstract class EnemyDecorator : IEnemy
{
    protected IEnemy DecoratedEnemy;

    public EnemyDecorator(IEnemy enemy)
    {
        DecoratedEnemy = enemy;
    }

    public Vector2 Position
    {
        get => DecoratedEnemy.Position;
        set => DecoratedEnemy.Position = value;
    }

    public Texture2D Sprite => DecoratedEnemy.Sprite;

    public bool IsActive
    {
        get => DecoratedEnemy.IsActive;
        set => DecoratedEnemy.IsActive = value;
    }

    public Rectangle BoundingBox => DecoratedEnemy.BoundingBox;

    public virtual void Update(GameTime gameTime, Vector2 playerPosition)
    {
        DecoratedEnemy.Update(gameTime, playerPosition);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        DecoratedEnemy.Draw(spriteBatch);
    }

    public virtual void Attack(GameTime gameTime, Vector2 playerPosition,EnemyBulletType bulletType = null)
    {
        DecoratedEnemy.Attack(gameTime, playerPosition);
    }
}
