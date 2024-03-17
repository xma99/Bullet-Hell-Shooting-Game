using Alpha_Danmaku_Rush_Demo.Src.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies
{
    class RegularAEnemy : Enemy
    {
        public RegularAEnemy(Texture2D sprite, Vector2 startPosition, float movementSpeed, Texture2D bulletSprite)
            : base(sprite, startPosition, movementSpeed, bulletSprite) { }

        public override void Update(GameTime gameTime, Vector2 playerPosition)
        {
            // Implement specific update logic for RegularAEnemy
            // Deactivate enemy if it goes off-screen
            if (Position.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
            {
                Deactivate();
            }

            movementSpeed = 80f;
            Move(gameTime, playerPosition);
            DateTime curTime = DateTime.Now;
            int second = curTime.Second;
            if (attackList.Count > 0)
            {
                foreach (Attack attack in attackList.ToList())
                {
                    attack.UpdateAttack(gameTime, playerPosition);
                }
            }

            if (second % 4 == 0 && BulletCheck == true)
            {
                attack = new Attack(BulletSprite, Position, DefaultTarget);
                attackList.Add(attack);
                BulletCheck = false;

            }

            if (second % 4 != 0 && !BulletCheck)
            {
                BulletCheck = true;
            }
        }
    }
}