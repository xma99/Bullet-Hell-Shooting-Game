using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Player
{
    public class Player : IPlayer
    {

        public GameObject GameObject { get; private set; }
        public int Health { get; set; } = 5;

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
            throw new System.NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameObject.Draw(spriteBatch);
        }
    }
}