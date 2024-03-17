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
            sprite = content.Load<Texture2D>("a");
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific update logic for RegularAEnemy
            // Deactivate enemy if it goes off-screen
            if (Position.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
            {
                Deactivate();
            }

            movementSpeed = 80f;
            Move(gameTime, playerPosition);
        }
    }
}