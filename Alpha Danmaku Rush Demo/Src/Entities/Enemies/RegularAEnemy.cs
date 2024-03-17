using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Content;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    class RegularAEnemy : Enemy
    {
        public RegularAEnemy(ContentManager content, Vector2 startPosition, float movementSpeed)
            : base(content, startPosition, movementSpeed)
        {
            Sprite = content.Load<Texture2D>("a");
            attackInterval = TimeSpan.FromSeconds(2);
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific update logic for RegularAEnemy
            // Deactivate enemy if it goes off-screen
            if (Position.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
            {
                Deactivate();
            }

            Speed = 80f;
            Move(gameTime, playerPosition);

            //attack
            Attack(gameTime, playerPosition);
        }

        public override void Attack(GameTime gameTime, Vector2 playerPosition)
        {
            // Timer and Interval logic as previously discussed
            attackTimer += gameTime.ElapsedGameTime;

            if (attackTimer >= attackInterval)
            {
                attackTimer = TimeSpan.Zero; // Reset the timer for the next attack

                foreach (var bullet in bulletList)
                {
                    if (!bullet.IsActive)
                    {
                        bullet.IsActive = true; // Activate the bullet
                        // Set the bullet's velocity towards the player
                        bullet.Position = Position;
                        bullet.Velocity = Vector2.Normalize(playerPosition - bullet.Position) * bullet.Speed; // Assuming bullet has a Speed property
                        break; // This assumes you want to activate only one bullet per attack interval
                    }
                }
            }
        }
    }
}