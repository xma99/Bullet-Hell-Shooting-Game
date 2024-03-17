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

        public FinalBossEnemy(Texture2D sprite, Vector2 startPosition, float movementSpeed, Texture2D bulletSprite) 
            : base(sprite, startPosition, movementSpeed, bulletSprite)
        {
        }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific update logic for FinalBossEnemy
            // FinalBoss do left-right movement
            float delta = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * finalBossMove;
            Position = new Vector2(Position.X + delta, Position.Y);

            DateTime curTime = DateTime.Now;
            int second = curTime.Second;
            if (attackList.Count > 0)
            {
                foreach (Attack attack in attackList.ToList())
                {
                    attack.UpdateAttack(gameTime, playerPosition);
                }
            }

            if (second % 2 == 0 && BulletCheck)
            {
                Attack newAttack = new Attack(BulletSprite, Position, DefaultTarget);
                attackList.Add(newAttack);

                BulletCheck = false;
            }
            if (second % 2 != 0 && !BulletCheck)
            {
                BulletCheck = true;
            }

        }
    }
}