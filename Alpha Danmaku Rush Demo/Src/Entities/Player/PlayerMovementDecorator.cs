using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player;

public class PlayerMovementDecorator : GameObject
{
    private GameObject wrappedPlayer;
    private float playerSpeed = 5.0f;

    public PlayerMovementDecorator(GameObject player, float speed) : base(player.Sprite, player.Position)
    {
        wrappedPlayer = player;
        playerSpeed = speed;
    }

    public void Update(GameTime gameTime, int screenWidth)
    {
        Vector2 movement = Vector2.Zero;
        KeyboardState direction = Keyboard.GetState();

        if (direction.IsKeyDown(Keys.W) || direction.IsKeyDown(Keys.Up)) movement.Y -= 1;
        if (direction.IsKeyDown(Keys.S) || direction.IsKeyDown(Keys.Down)) movement.Y += 1;
        if (direction.IsKeyDown(Keys.A) || direction.IsKeyDown(Keys.Left)) movement.X -= 1;
        if (direction.IsKeyDown(Keys.D) || direction.IsKeyDown(Keys.Right)) movement.X += 1;

        if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) playerSpeed /= 2;
        if (Keyboard.GetState().IsKeyUp(Keys.LeftShift)) playerSpeed *= 2;

        if (movement.LengthSquared() > 0) movement.Normalize();

        Vector2 updatePosition = wrappedPlayer.Position + movement * playerSpeed;
        updatePosition.X = MathHelper.Clamp(updatePosition.X, 0, screenWidth - wrappedPlayer.Sprite.Width);
        wrappedPlayer.Position = updatePosition;
        playerSpeed = 5.0f;
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        wrappedPlayer.Draw(spriteBatch);
    }
}
