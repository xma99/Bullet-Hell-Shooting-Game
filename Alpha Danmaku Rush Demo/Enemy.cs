using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Alpha_Danmaku_Rush_Demo
{
    public enum EnemyType
    {
        RegularA,
        RegularB,
        MidBoss,
        FinalBoss
    }

    public class Enemy
    {
        private Texture2D sprite;
        public Vector2 Position { get; private set; }
        private float movementSpeed;
        private bool isActive;

        private EnemyType type;

        private Vector2 midBossPosition;
        private float midBossMove = 20f;

        public bool IsActive => isActive;

        public Enemy(Texture2D sprite, Vector2 startPosition, float movementSpeed, EnemyType type)
        {
            this.sprite = sprite;
            Position = startPosition;
            this.movementSpeed = movementSpeed;
            isActive = true;
            this.type = type;

            if (type == EnemyType.MidBoss)
            {
                midBossPosition = startPosition;
            }
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // movementSpeed = 80f; // Set enemy speed
            if (!isActive) return;
            switch (type)
            {
                case EnemyType.MidBoss:
                    UpdateMidBoss(gameTime);
                    break;
                default:
                    UpdateRegularEnemy(gameTime, playerPosition);
                    break;
            }
            // Deactivate enemy if it goes off-screen
            if (Position.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
            {
                isActive = false;
            }
        }

        private void UpdateMidBoss(GameTime gameTime)
        {
            // MidBoss do left-right movement
            float delta = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * midBossMove;
            Position = new Vector2(midBossPosition.X + delta, Position.Y);
        }

        private void UpdateRegularEnemy(GameTime gameTime, Vector2 playerPosition)
        {
            movementSpeed = 80f;
            Move(gameTime, playerPosition);
        }
        private void Move(GameTime gameTime, Vector2 playerPosition)
        {
            // Calculate direction towards the player
            Vector2 direction = Vector2.Normalize(playerPosition - Position);
            // Move the enemy towards the player
            Vector2 newPosition = Position + direction * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Ensure the enemy stays within the game screen
            newPosition.X = Math.Clamp(newPosition.X, 0, GraphicsDeviceManager.DefaultBackBufferWidth - sprite.Width);
            newPosition.Y = Math.Clamp(newPosition.Y, 0, GraphicsDeviceManager.DefaultBackBufferHeight - sprite.Height);
            // Update the position
            Position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isActive) return;
            spriteBatch.Draw(sprite, Position, Color.White);
        }

        public void Deactivate() // Method to deactivate the enemy
        {
            isActive = false;
        }
    }
}
