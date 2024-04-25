using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack
{


    public class RegularAAllocator: EnemyDecorator,AttackStrategy
    {
        private TimeSpan attackInterval;
        private TimeSpan attackTimer;
        

        public RegularAAllocator(IEnemy enemy, TimeSpan attackInterval) : base(enemy)
        {
            this.attackInterval = attackInterval;
        }
        public void attackstrategy(List<Bullet.Bullet> bullets, Vector2 playerPosition, GameTime gameTime,SpriteBatch spriteBatch)
        {
            if (bullets.Count <= 0)
                return;
            attackTimer += gameTime.ElapsedGameTime;
            if (attackTimer >= attackInterval)
            {
                attackTimer = TimeSpan.Zero; // Reset timer
                                             // Execute attack logic
                Vector2 direction = Vector2.Normalize(playerPosition - DecoratedEnemy.Position);
                Vector2 bulletVelocity = direction * 8f; // Example speed
                                                         // Potentially activate a bullet here
                Bullet.Bullet bullet= bullets[bullets.Count-1];
                bullets.RemoveAt(bullets.Count-1);
                bullet.IsActive = true;
               
            }
            attackstrategy(bullets, playerPosition, gameTime,spriteBatch);
            //attackHelper(attackTimer,bullets, playerPosition, gameTime);
        }
       
    }
    public class BossAllocator: EnemyDecorator,AttackStrategy {
        private TimeSpan attackInterval;
        private TimeSpan attackTimer;

        public BossAllocator(IEnemy enemy, TimeSpan attackInterval) : base(enemy)
        {
            this.attackInterval = attackInterval;
        }
        public void AttackPattern(int n)
        {
            throw new NotImplementedException();
        }
        public void attackstrategy(List<Bullet.Bullet> bullets, Vector2 playerPosition, GameTime gameTime,SpriteBatch spriteBatch)
        {
            attackTimer += gameTime.ElapsedGameTime;
            int n = 0;
            if (attackTimer >= attackInterval)
            {
                attackTimer = TimeSpan.Zero;
                AttackPattern(n);
                n++;
            }         
            attackstrategy(bullets, playerPosition, gameTime,spriteBatch);
        }
    }
}
