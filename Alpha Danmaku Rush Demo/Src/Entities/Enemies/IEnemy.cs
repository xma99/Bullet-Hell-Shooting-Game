using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies;

public interface IEnemy
{
    Vector2 Position { get; set; }
    Texture2D Sprite { get; }
    bool IsActive { get; set; }
    Rectangle BoundingBox { get; }

    void Update(GameTime gameTime, Vector2 playerPosition);
    void Draw(SpriteBatch spriteBatch);
    void Attack(GameTime gameTime, Vector2 playerPosition,EnemyBulletType bulletType=null);
}
