using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Alpha_Danmaku_Rush_Demo.Src.Utils
{
    public class Attack
    {
        // Fields for enemy properties
        public Texture2D bullet { get; set; }
        public Vector2 bulletPosition { get; set; }
        public Vector2 bulletVelocity { get; set; }
        public Vector2 DefaultTarget { get; set; }
        public bool checkAttack { get; set; }
        private float bulletSpeed;

        // Enemy attack pattern properties
        // private float attackCooldown;
        // private float currentCooldown;

        // public object BoundingBox { get; internal set; }
        public Attack(Texture2D bullet, Vector2 SpawnPosition, Vector2 defaultTarget)
        {
            this.bullet = bullet;
            checkAttack = true;
            bulletPosition = SpawnPosition;
            bulletSpeed = 10.0f;
            DefaultTarget = defaultTarget;
        }

        public void UpdateAttack(GameTime gameTime, Vector2 playerPosition)
        {
            //Vector2 temp = playerPosition;
            //Vector2 direction = Vector2.Normalize(temp - bulletPosition);
            Vector2 direction = DefaultTarget;
            bulletPosition += direction * bulletSpeed;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (checkAttack)
            {
                spriteBatch.Draw(bullet, bulletPosition, Color.White);
            }
        }
    }
}



