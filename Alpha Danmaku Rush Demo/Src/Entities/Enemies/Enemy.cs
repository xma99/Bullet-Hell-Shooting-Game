using Alpha_Danmaku_Rush_Demo.Src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Alpha_Danmaku_Rush_Demo.Src.Utils;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    public abstract class Enemy
    {
        protected Texture2D sprite;
        public Vector2 Position { get; protected set; }
        protected float movementSpeed;
        public bool isActive;

        protected Texture2D BulletSprite;
        public List<Attack> attackList;
        protected Attack attack;//an attack object
        public bool BulletCheck = true;

        protected Vector2 DefaultTarget = new Vector2(0, 1);//default bullet moving direction

        protected Enemy(Texture2D sprite, Vector2 startPosition, float movementSpeed, Texture2D bulletSprite)
        {
            this.sprite = sprite;
            Position = startPosition;
            this.movementSpeed = movementSpeed;
            isActive = true;
            BulletSprite = bulletSprite;
            attackList = new List<Attack>();
            attack = new Attack(BulletSprite, Position, DefaultTarget);
            attackList.Add(attack);
        }

        public abstract void Update(GameTime gameTime, Vector2 playerPosition);

        protected void Move(GameTime gameTime, Vector2 playerPosition)
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
            foreach (Attack attack in attackList)
            {
                attack.Draw(spriteBatch);
            }
        }

        public void Deactivate()
        {
            isActive = false;
        }
    }
    
}
