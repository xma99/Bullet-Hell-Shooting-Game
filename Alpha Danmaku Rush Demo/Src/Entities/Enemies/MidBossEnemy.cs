using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    class MidBossEnemy : Enemy
    {
        private float midBossMove = 20f;

        public MidBossEnemy(Texture2D sprite, Vector2 startPosition, float movementSpeed, Texture2D bulletSprite)
            : base(sprite, startPosition, movementSpeed, bulletSprite) { }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific update logic for MidBossEnemy
            // MidBoss do left-right movement
            float delta = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * midBossMove;
            Position = new Vector2(Position.X + delta, Position.Y);
            DateTime curTime = DateTime.Now;
            int second = curTime.Second;
            if (attackList.Count > 0)
            {
                foreach (Attack attack in attackList)
                {
                    attack.UpdateAttack(gameTime, playerPosition);

                }
            }

            if (second % 3 == 0 && BulletCheck)
            {
                Attack newAttack = new Attack(BulletSprite, Position, DefaultTarget);

                attackList.Add(newAttack);
                BulletCheck = false;
            }
            if (second % 3 != 0 && !BulletCheck)
            {
                BulletCheck = true;
            }
        }
    }
}