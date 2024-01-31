namespace Alpha_Danmaku_Rush;

using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class CollisionManager
{
    public Player Player { get; set; }
    public List<Enemy> Enemies { get; set; }
    public List<Bullet> Bullets { get; set; }
    public List<PowerUp> PowerUps { get; set; }

    public CollisionManager(Player player, List<Enemy> enemies, List<Bullet> bullets, List<PowerUp> powerUps)
    {
        Player = player;
        Enemies = enemies;
        Bullets = bullets;
        PowerUps = powerUps;
    }

    public void Update()
    {
        CheckPlayerCollisions();
        CheckBulletCollisions();
    }

    private void CheckPlayerCollisions()
    {
        // Check for collisions between the player and enemies
        foreach (var enemy in Enemies)
        {
            if (Player.BoundingBox.Intersects(enemy.BoundingBox))
            {
                // Handle player-enemy collision (e.g., reduce player health)
            }
        }

        // Check for collisions between the player and power-ups
        foreach (var powerUp in PowerUps)
        {
            if (Player.BoundingBox.Intersects(powerUp.BoundingBox))
            {
                // Handle player-power-up collision (e.g., apply power-up)
            }
        }
    }

    private void CheckBulletCollisions()
    {
        // Check for collisions between bullets and enemies
        foreach (var bullet in Bullets)
        {
            foreach (var enemy in Enemies)
            {
                if (bullet.BoundingBox.Intersects(enemy.BoundingBox))
                {
                    // Handle bullet-enemy collision (e.g., damage enemy)
                }
            }
        }
    }
}
