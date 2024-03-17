using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    class FinalBossEnemy : Enemy
    {
        private float finalBossMove = 20f;

        public FinalBossEnemy(Texture2D sprite, Vector2 startPosition, float movementSpeed) 
            : base(sprite, startPosition, movementSpeed)
        {
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific update logic for FinalBossEnemy
            // FinalBoss do left-right movement
            float delta = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * finalBossMove;
            Position = new Vector2(Position.X + delta, Position.Y);
        }
    }
}