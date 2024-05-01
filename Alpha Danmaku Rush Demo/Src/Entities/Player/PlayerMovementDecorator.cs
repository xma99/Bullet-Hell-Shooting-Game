using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public class PlayerMovementDecorator : IPlayer
{
    private IPlayer _wrappedPlayer;
    private float _playerSpeed = 5.0f;
    private bool isInvincible = false;
    public bool IsInvincible => _wrappedPlayer.IsInvincible;

    public PlayerMovementDecorator(IPlayer player, float speed)
    {
        _wrappedPlayer = player;
        _playerSpeed = speed;
    }

    public Vector2 Position
    {
        get => _wrappedPlayer.Position;
        set => _wrappedPlayer.Position = value;
    }

    public Texture2D Sprite => _wrappedPlayer.Sprite;

    public Rectangle BoundingBox => _wrappedPlayer.BoundingBox;

    public int Health { get => _wrappedPlayer.Health; set => _wrappedPlayer.Health = value; }

    public void Update(GameTime gameTime, int screenWidth)
    {
        Move(gameTime, screenWidth);
        _wrappedPlayer.Update(gameTime, screenWidth);
    }

    private void Move(GameTime gameTime, int screenWidth)
    {
        Vector2 movement = Vector2.Zero;
        KeyboardState direction = Keyboard.GetState();

        if (direction.IsKeyDown(Keys.W) || direction.IsKeyDown(Keys.Up)) movement.Y -= 1;
        if (direction.IsKeyDown(Keys.S) || direction.IsKeyDown(Keys.Down)) movement.Y += 1;
        if (direction.IsKeyDown(Keys.A) || direction.IsKeyDown(Keys.Left)) movement.X -= 1;
        if (direction.IsKeyDown(Keys.D) || direction.IsKeyDown(Keys.Right)) movement.X += 1;

        if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) _playerSpeed /= 2;
        else if (Keyboard.GetState().IsKeyUp(Keys.LeftShift)) _playerSpeed = 5.0f;

        if (movement.LengthSquared() > 0) movement.Normalize();

        Vector2 updatePosition = this.Position + movement * _playerSpeed;
        updatePosition.X = MathHelper.Clamp(updatePosition.X, 0, screenWidth - this.Sprite.Width);
        this.Position = updatePosition;
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

    public void SetContent(ContentManager content)
    {
    }
}
