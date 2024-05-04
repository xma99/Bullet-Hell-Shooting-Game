using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;

public class PlayerBullet : Bullet
{
    public PlayerBullet(Texture2D sprite, Vector2 position, Vector2 velocity, Color color)
        : base(sprite, position, velocity, color)
    {
        Damage = 1;
    }

    public Vector2 Scale { get; internal set; }

    public override void Update(GameTime gameTime = null)
    {
        CheckOffScreen();
        Vector2 defaultT = new Vector2(0, 1);
        Position -= defaultT * 5.0f;
    }
}