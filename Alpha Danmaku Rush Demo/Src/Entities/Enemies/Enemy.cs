using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Microsoft.Xna.Framework.Content;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    public abstract class Enemy
    {
        private ContentManager content;
        protected Texture2D Sprite;

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        protected float Speed;
        public bool isActive;

        // Timer to track the time since the last attack
        protected TimeSpan attackTimer = TimeSpan.Zero;
        // Interval between attacks
        protected TimeSpan attackInterval;


        public List<Bullet.Bullet> bulletList = new List<Bullet.Bullet>();

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);

        protected Vector2 DefaultTarget = new Vector2(0, 1);//default bullet moving direction

        protected Enemy(ContentManager content, Vector2 startPosition, float movementSpeed)
        {
            this.content = content;
            Position = startPosition;
            Speed = movementSpeed;
            isActive = true;
        }

        public abstract void Update(GameTime gameTime, Vector2 playerPosition);

        public abstract void Attack(GameTime gameTime, Vector2 playerPosition);

        protected void Move(GameTime gameTime, Vector2 playerPosition)
        {
            // Calculate direction towards the player
            Vector2 direction = Vector2.Normalize(playerPosition - Position);
            // Move the enemy towards the player
            Vector2 newPosition = Position + direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Ensure the enemy stays within the game screen
            newPosition.X = Math.Clamp(newPosition.X, 0, GraphicsDeviceManager.DefaultBackBufferWidth - Sprite.Width);
            newPosition.Y = Math.Clamp(newPosition.Y, 0, GraphicsDeviceManager.DefaultBackBufferHeight - Sprite.Height);
            // Update the position
            Position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isActive) return;
            spriteBatch.Draw(Sprite, Position, Color.White);
        }

        public void Deactivate()
        {
            isActive = false;
        }

        public void AddBullet(EnemyBulletType type)
        {
            for (int i = 0; i < type.Amount; i++)
            {
                bulletList.Add(BulletFactory.CreateBullet(content, this.Position, this.Velocity, type));
            }
        }
    }
    
}
