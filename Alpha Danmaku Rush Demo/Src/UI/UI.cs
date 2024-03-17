using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace Alpha_Danmaku_Rush_Demo.Src.UI
{
    public class HealthIcon
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public bool IsActive { get; set; }
        public float Scale { get; set; }

        public HealthIcon(Texture2D texture, Vector2 position, float scale = 0.03f, bool isActive = true)
        {
            Texture = texture;
            Position = position;
            IsActive = isActive;
            Scale = scale;
        }

        // Adjust position relative to the top-right corner
        public void UpdatePosition(Vector2 screenSize)
        {
            Position = new Vector2(Position.X, 0);
        }

        // Draw the health icon
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
