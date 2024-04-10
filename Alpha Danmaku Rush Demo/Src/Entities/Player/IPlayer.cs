using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public interface IPlayer
{
    public void Update(GameTime gameTime, int screenWidth);
}