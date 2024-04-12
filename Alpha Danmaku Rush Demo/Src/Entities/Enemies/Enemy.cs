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
    public class Enemy : IEnemy
    {
        private GameObject gameObject;
        private float speed;
        private ContentManager content;
        public List<Bullet.Bullet> bulletList = new List<Bullet.Bullet>();

        public Vector2 Position
        {
            get => gameObject.Position;
            set => gameObject.Position = value;
        }

        public Texture2D Sprite => gameObject.Sprite;

        public bool IsActive
        {
            get => gameObject.IsActive;
            set => gameObject.IsActive = value;
        }

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);

        public Enemy(ContentManager content, Vector2 startPosition, float movementSpeed)
        {
            this.content = content;
            this.gameObject = new GameObject(content.Load<Texture2D>("a"), startPosition);
            this.speed = movementSpeed;
        }

        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            Move(gameTime, playerPosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(Sprite, Position, Color.White);
            }
        }

        public void Attack(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement attack logic
        }

        private void Move(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement movement logic
            Vector2 direction = Vector2.Normalize(playerPosition - Position);
            Position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }

}
