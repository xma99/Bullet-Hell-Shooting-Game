namespace Alpha_Danmaku_Rush;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Player
{
    public Vector2 Position;
    private Texture2D sprite;
    private float speed = 200; // Speed of the player
    private Rectangle screenBounds; // Bounds to keep player on screen

    // Player's properties like health, score, etc.
    public int Health { get; private set; }
    public object BoundingBox { get; internal set; }

    public Player(Texture2D sprite, Rectangle screenBounds)
    {
        this.sprite = sprite;
        this.screenBounds = screenBounds;
        Position = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2); // Start in center
        Health = 3; // Example starting health
    }

    public void Update(GameTime gameTime)
    {
        // Player movement
        var keyboardState = Keyboard.GetState();
        Vector2 direction = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.W))
            direction.Y = -1;
        if (keyboardState.IsKeyDown(Keys.S))
            direction.Y = 1;
        if (keyboardState.IsKeyDown(Keys.A))
            direction.X = -1;
        if (keyboardState.IsKeyDown(Keys.D))
            direction.X = 1;

        direction.Normalize();
        Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Keep player in bounds
        Position.X = Math.Clamp(Position.X, 0, screenBounds.Width - sprite.Width);
        Position.Y = Math.Clamp(Position.Y, 0, screenBounds.Height - sprite.Height);

        // Implement shooting logic here
        // Implement special abilities here

        // Implement collision detection with enemies/bullets
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, Position, Color.White);
    }

    // Method to call when player is hit
    public void TakeDamage()
    {
        Health--;
        // Additional logic for when player is hit
    }


    // Additional methods for shooting, using bombs, power-ups, etc.
    internal void PowerUp(float effectValue)
    {
        throw new NotImplementedException();
    }

    internal void AddScore(int effectValue)
    {
        throw new NotImplementedException();
    }

    internal void RestoreHealth(float effectValue)
    {
        throw new NotImplementedException();
    }
}
