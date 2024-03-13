using System;

namespace Alpha_Danmaku_Rush.Src.Entities.Enemy;

using Alpha_Danmaku_Rush.Src.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public enum EnemyType
{
    RegularA,
    RegularB,
    MidBoss,
    FinalBoss
}

public class Enemy
{
    //// Fields for enemy properties
    //public Vector2 Position { get; private set; }
    //private Texture2D sprite;
    //private float movementSpeed;
    //private bool isActive;
    //private EnemyType type;

    //// Enemy attack pattern properties
    //private float attackCooldown;
    //private float currentCooldown;

    //public object BoundingBox { get; internal set; }

    //public Enemy(Texture2D sprite, Vector2 startPosition, float movementSpeed)
    //{
    //    this.sprite = sprite;
    //    Position = startPosition;
    //    this.movementSpeed = movementSpeed;
    //    isActive = true;
    //    type = type;

    //    // Set attack cooldown based on enemy type
    //    switch (type)
    //    {
    //        case EnemyType.RegularA:
    //            attackCooldown = 4.0f;
    //            break;
    //        case EnemyType.RegularB:
    //            attackCooldown = 4.0f;
    //            break;
    //        case EnemyType.MidBoss:
    //            attackCooldown = 3.0f;
    //            break;
    //        case EnemyType.FinalBoss:
    //            attackCooldown = 2.0f;
    //            break;
    //    }
    //    currentCooldown = attackCooldown;
    //}

    //public void Update(GameTime gameTime)
    //{
    //    if (!isActive) return;

    //    // Update enemy movement
    //    Move();

    //    // Update attack pattern
    //    UpdateAttack(gameTime);
    //}

    //private void Move()
    //{
    //    // Implement movement logic based on enemy type
    //    switch (type)
    //    {
    //        case EnemyType.RegularA:
    //        case EnemyType.RegularB:
    //            // Simple horizontal movement for Regular enemies
    //            Position.X += movementSpeed;
    //            break;

    //        case EnemyType.MidBoss:
    //            // Circular movement pattern for MidBoss
    //            float radius = 100; // Adjust radius as needed
    //            float angularSpeed = 0.5f; // Adjust angular speed as needed
    //            float time = (float)Game1.GameTime.TotalGameTime.TotalSeconds; // Assuming GameTime is a static property in 
    //            Position.X = startPosition.X + (float)Math.Cos(time * angularSpeed) * radius;
    //            Position.Y = startPosition.Y + (float)Math.Sin(time * angularSpeed) * radius;
    //            break;

    //        case EnemyType.FinalBoss:
    //            // 8 movement pattern for FinalBoss
    //            float xOffset = 100; // Adjust offsets as needed
    //            float yOffset = 50;
    //            float speedX = 100; // Adjust speed as needed
    //            float speedY = 50;
    //            Position.X = startPosition.X + (float)Math.Sin(Game1.GameTime.TotalGameTime.TotalSeconds * speedX) * xOffset;
    //            Position.Y = startPosition.Y + (float)Math.Sin(Game1.GameTime.TotalGameTime.TotalSeconds * speedY) * yOffset;
    //            break;
    //    }
    //}

    //private void UpdateAttack(GameTime gameTime)
    //{
    //    currentCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
    //    if (currentCooldown <= 0)
    //    {
    //        // Reset cooldown
    //        currentCooldown = attackCooldown;

    //        // Execute attack pattern
    //        Shoot();
    //    }
    //}

    //private void Shoot()
    //{
    //    // Implement bullet shooting logic
    //    // Depending on the enemy type, bullets may have different patterns or behavior
    //    Bullet bullet = new Bullet(/* parameters */);
    //    // Add bullet to the game's list of active projectiles or shoot it directly
    //}

    //public void Draw(SpriteBatch spriteBatch)
    //{
    //    if (!isActive) return;

    //    spriteBatch.Draw(sprite, Position, Color.White);
    //}

    //// Method to deactivate the enemy
    //public void Deactivate()
    //{
    //    isActive = false;
    //}
}
