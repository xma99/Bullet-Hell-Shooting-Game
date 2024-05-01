using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public interface IPlayer
{
    Vector2 Position { get; set; }
    Texture2D Sprite { get; }
    Rectangle BoundingBox { get; }
    int Health { get; set; }
    bool IsInvincible { get; }

    public void Update(GameTime gameTime, int screenWidth);

    public void Draw(SpriteBatch spriteBatch);

    public void SetContent(ContentManager content);

    void Respawn();
}