using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public class PlayerMovementDecorator : IPlayer
{
    private IPlayer wrappedPlayer;
    private float playerSpeed = 5.0f;

    public PlayerMovementDecorator(IPlayer player, float speed)
    {
        wrappedPlayer = player;
        playerSpeed = speed;
    }

    public void Update(GameTime gameTime, int screenWidth)
    {
        Move(gameTime, screenWidth);
    }

    private void Move(GameTime gameTime, int screenWidth)
    {
        Vector2 movement = Vector2.Zero;
        KeyboardState direction = Keyboard.GetState();

        if (direction.IsKeyDown(Keys.W) || direction.IsKeyDown(Keys.Up)) movement.Y -= 1;
        if (direction.IsKeyDown(Keys.S) || direction.IsKeyDown(Keys.Down)) movement.Y += 1;
        if (direction.IsKeyDown(Keys.A) || direction.IsKeyDown(Keys.Left)) movement.X -= 1;
        if (direction.IsKeyDown(Keys.D) || direction.IsKeyDown(Keys.Right)) movement.X += 1;

        if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) playerSpeed /= 2;
        else if (Keyboard.GetState().IsKeyUp(Keys.LeftShift)) playerSpeed = 5.0f; // Reset speed if not holding shift

        if (movement.LengthSquared() > 0) movement.Normalize();

        if (wrappedPlayer is GameObject gameObject)
        {
            Vector2 updatePosition = gameObject.Position + movement * playerSpeed;
            updatePosition.X = MathHelper.Clamp(updatePosition.X, 0, screenWidth - gameObject.Sprite.Width);
            gameObject.Position = updatePosition;
        }
    }
}
