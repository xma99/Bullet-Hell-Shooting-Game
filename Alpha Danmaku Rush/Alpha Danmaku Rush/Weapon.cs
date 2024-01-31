namespace Alpha_Danmaku_Rush;

using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class Weapon
{
    // Fields to represent weapon properties
    public float FireRate { get; private set; } // Bullets per second
    private float timeSinceLastShot;
    private Vector2 position;
    private bool isFiring;

    // Constructor
    public Weapon(float fireRate)
    {
        FireRate = fireRate;
        timeSinceLastShot = 0;
        isFiring = false;
    }

    // Update method - called every frame
    public void Update(GameTime gameTime)
    {
        if (isFiring)
        {
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastShot >= 1.0f / FireRate)
            {
                FireBullet();
                timeSinceLastShot = 0;
            }
        }
    }

    // Method to start firing
    public void StartFiring()
    {
        isFiring = true;
        timeSinceLastShot = 0; // Reset the shot timer
    }

    // Method to stop firing
    public void StopFiring()
    {
        isFiring = false;
    }

    // Method to fire a bullet
    private void FireBullet()
    {
        // Create and position a new bullet
        // Depending on your game's architecture, you might add the bullet to a game manager,
        // a bullet manager, or directly to the game world.
    }

    // Method to set the position of the weapon
    public void SetPosition(Vector2 newPosition)
    {
        position = newPosition;
    }
}
