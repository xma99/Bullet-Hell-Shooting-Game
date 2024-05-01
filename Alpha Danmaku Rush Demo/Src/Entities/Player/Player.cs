using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player
{
    public class Player : IPlayer
    {

        public GameObject GameObject { get; private set; }
        public int Health { get; set; } = 5;
        private Vector2 initialPosition;

        private TimeSpan invincibilityTimer = TimeSpan.Zero;
        private TimeSpan invincibilityDuration = TimeSpan.FromSeconds(5);
        private bool isInvincible = false;
        public bool IsInvincible => isInvincible;
        public int flag = 1;

        public Vector2 Position
        {
            get => GameObject.Position;
            set => GameObject.Position = value;
        }

        public Texture2D Sprite => GameObject.Sprite;

        public Rectangle BoundingBox => new Rectangle((int)GameObject.Position.X, (int)GameObject.Position.Y, GameObject.Sprite.Width, GameObject.Sprite.Height);

        public Player(Texture2D img, Vector2 initialPosition)
        {
            GameObject = new GameObject(img, initialPosition);
        }

        public void Update(GameTime gameTime, int screenWidth)
        {
            // Check for keyboard input to toggle invincibility
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.H))
            {
                // Toggle invincibility
                isInvincible = true;
                flag = 0;
            }

            if (isInvincible && flag != 0)
            {
                invincibilityTimer += gameTime.ElapsedGameTime;
                if (invincibilityTimer >= invincibilityDuration)
                {
                    isInvincible = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Check if player is in hack mode
            if (isInvincible)
            {
                spriteBatch.Draw(Sprite, Position, Color.Red);
            }
            else
            {
                // Draw the player normally
                GameObject.Draw(spriteBatch);
            }
        }

        public void Respawn()
        {
            Position = new Vector2((800 - Sprite.Width) / 2, 1000 - Sprite.Height); // Reset player position to bottom center
            isInvincible = true;
            invincibilityTimer = TimeSpan.Zero;
        }

        public void SetContent(ContentManager content)
        {
        }

        public Bullet.Bullet GetBullet()
        {
            return null;
        }
    }
}