using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player
{
    public class Player : GameObject, IPlayer
    {

        public int Health { get; set; } = 10;  // Player health

        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);

        public Player(Texture2D img, Vector2 initialPosition) : base(img, initialPosition)
        {
        }

        public void Update(GameTime gameTime, int screenWidth)
        {
            throw new System.NotImplementedException();
        }
    }
}
