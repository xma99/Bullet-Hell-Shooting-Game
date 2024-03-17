using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities
{
    public class Player
    {
        // Player default setting
        public Texture2D Sprite;
        public Vector2 Position { get; set; }
        private float playerSpeed = 5.0f;

        public int Health { get; set; } = 10;  // Player health

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);

        public Player(Texture2D img, Vector2 initialPosition)
        {
            Sprite = img;
            Position = initialPosition;
        }

        public void Update(GameTime gameTime, int screenWidth)
        {
            Vector2 movement = Vector2.Zero;
            KeyboardState direction = Keyboard.GetState();

            // Player movement, WASD
            if (direction.IsKeyDown(Keys.W) || direction.IsKeyDown(Keys.Up))
            {
                movement.Y -= 1;
            }
            if (direction.IsKeyDown(Keys.S) || direction.IsKeyDown(Keys.Down))
            {
                movement.Y += 1;
            }
            if (direction.IsKeyDown(Keys.A) || direction.IsKeyDown(Keys.Left))
            {
                movement.X -= 1;
            }
            if (direction.IsKeyDown(Keys.D) || direction.IsKeyDown(Keys.Right))
            {
                movement.X += 1;
            }

            // Speed change
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                playerSpeed /= 2;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.LeftShift))
            {
                playerSpeed *= 2;
            }

            if (movement.LengthSquared() > 0)
            {
                movement.Normalize();
            }

            // Player can move up but not beyond the width of the screen
            Vector2 updatePosition = Position + movement * playerSpeed;
            updatePosition.X = MathHelper.Clamp(updatePosition.X, 0, screenWidth - Sprite.Width);
            Position = updatePosition;
            playerSpeed = 5.0f;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }
    }
}
