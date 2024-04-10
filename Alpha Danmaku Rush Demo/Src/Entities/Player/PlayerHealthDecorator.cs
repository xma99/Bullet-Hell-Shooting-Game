using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public class PlayerHealthDecorator : IPlayer
{
    private IPlayer wrappedPlayer;
    private int extraLifeTime = 3;

    public PlayerHealthDecorator(IPlayer player, int extraLifeTime)
    {
        this.wrappedPlayer = player;
        this.extraLifeTime = extraLifeTime;
    }

    public void Update(GameTime gameTime, int screenWidth)
    {
    }

}