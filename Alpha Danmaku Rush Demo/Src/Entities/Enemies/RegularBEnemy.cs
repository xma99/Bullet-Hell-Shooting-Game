using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Content;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    class RegularBEnemy : Enemy
    {
        public RegularBEnemy(ContentManager content, Vector2 startPosition, float movementSpeed)
            : base(content, startPosition, movementSpeed)
        {
            Sprite = content.Load<Texture2D>("b");
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific update logic for MidBossEnemy
            // Deactivate enemy if it goes off-screen
            if (Position.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
            {
                Deactivate();
            }

            Speed = 80f;
            Move(gameTime, playerPosition);
        }

        public override void Attack(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific attack logic for RegularAEnemy
            // RegularAEnemy shoots bullets at the player
            if (gameTime.TotalGameTime.TotalMilliseconds % 500 < 10)
            {
                Vector2 direction = Vector2.Normalize(playerPosition - Position);
                Vector2 bulletVelocity = direction * 8f;
            }
        }
    }
}