using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public interface IPlayer
{
    Vector2 Position { get; set; }
    Texture2D Sprite { get; }
    Rectangle BoundingBox { get; }
    int Health { get; set; }

    public void Update(GameTime gameTime, int screenWidth);

    public void Draw(SpriteBatch spriteBatch);
}