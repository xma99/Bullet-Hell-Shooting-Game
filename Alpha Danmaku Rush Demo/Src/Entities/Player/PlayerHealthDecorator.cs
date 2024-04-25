using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public class PlayerHealthDecorator : IPlayer
{
    private IPlayer _wrappedPlayer;
    private int extraLifeTime = 3;
    private bool isInvincible = false;
    public bool IsInvincible => _wrappedPlayer.IsInvincible;

    public Vector2 Position
    {
        get => _wrappedPlayer.Position;
        set => _wrappedPlayer.Position = value;
    }

    public Texture2D Sprite => _wrappedPlayer.Sprite;

    public Rectangle BoundingBox => _wrappedPlayer.BoundingBox;

    public int Health { get => _wrappedPlayer.Health; set => _wrappedPlayer.Health = value; }

    public PlayerHealthDecorator(IPlayer player, int extraLifeTime)
    {
        this._wrappedPlayer = player;
        this.extraLifeTime = extraLifeTime;
    }

    public void Update(GameTime gameTime, int screenWidth)
    {
        _wrappedPlayer.Update(gameTime, screenWidth);
    }

    public void Respawn()
    {
        _wrappedPlayer.Respawn();
        isInvincible = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _wrappedPlayer.Draw(spriteBatch);
    }

}