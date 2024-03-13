namespace Alpha_Danmaku_Rush.Src.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

public class Player
{
    public Vector2 Position;
    private Texture2D sprite;
    private float speed = 200; // Speed of the player
    private Rectangle screenBounds; // Bounds to keep player on screen

    // Player's properties like health, score, etc.
    public int Health { get; private set; }
    public object BoundingBox { get; internal set; }

    List<Projectile> projectiles;
    Texture2D projectileSprite;
    float shootCooldown = 0.2f; // Cooldown between shots
    float shootTimer = 0f;

    public Player(Texture2D sprite, Texture2D projectileSprite, Rectangle screenBounds)
    {
        this.sprite = sprite;
        this.screenBounds = screenBounds;
        Position = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2); // Start in center
        Health = 3; // Example starting health

        this.projectileSprite = projectileSprite;
        projectiles = new List<Projectile>();
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
        // Update shooting cooldown timer
        shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Check if player can shoot
        if (shootTimer >= shootCooldown)
        {
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Shoot();
                shootTimer = 0f; // Reset shoot timer
            }
        }
        // Update projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Update();

            // Remove projectiles that are out of bounds
            if (!screenBounds.Contains(projectile.Position))
            {
                projectile.IsActive = false;
            }
        }

        // Implement special abilities here

        // Implement collision detection with enemies/bullets
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, Position, Color.White);

        // Draw projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Draw(spriteBatch);
        }
    }

    void Shoot()
    {
        // Create a new projectile and add it to the list
        Vector2 projectilePosition = Position + new Vector2(sprite.Width / 2, 0); // Adjust position to spawn projectile at player's center
        Vector2 projectileVelocity = new Vector2(0, -500); // Adjust velocity as needed
        int projectileDamage = 1; // Adjust damage as needed
        Projectile newProjectile = new Projectile(projectilePosition, projectileVelocity, projectileDamage);
        projectiles.Add(newProjectile);
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

class Projectile
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public int Damage { get; set; }
    public bool IsActive { get; set; }

    public Projectile(Vector2 position, Vector2 velocity, int damage)
    {
        Position = position;
        Velocity = velocity;
        Damage = damage;
        IsActive = true;
    }

    public void Update()
    {
        // Update projectile position based on velocity
        Position += Velocity;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draw projectile sprite
    }
}
