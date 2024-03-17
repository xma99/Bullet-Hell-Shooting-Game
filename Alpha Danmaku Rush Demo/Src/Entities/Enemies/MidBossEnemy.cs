using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    class MidBossEnemy : Enemy
    {
        private float midBossMove = 20f;

        public MidBossEnemy(Texture2D sprite, Vector2 startPosition, float movementSpeed)
            : base(sprite, startPosition, movementSpeed) { }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific update logic for MidBossEnemy
            // MidBoss do left-right movement
            float delta = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * midBossMove;
            Position = new Vector2(Position.X + delta, Position.Y);
        }
    }
}