using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Alpha_Danmaku_Rush_Demo
{
    public class Camera
    {
        public Vector2 Position { get; private set; }
        private readonly Viewport viewport;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        // Keeps the player in the center of the viewport when moving
        public void FollowThePlayer(Player target)
        {
            Position = new Vector2(target.Position.X - viewport.Width / 2, target.Position.Y - viewport.Height / 2);
        }

        public Matrix GetMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0.0f));
        }
    }
}
