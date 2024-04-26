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
        private Boolean attackSwitch;
        private SpriteBatch SB;
        List<Bullet.Bullet> FiredBullets;

        public RegularAAllocator(IEnemy enemy, TimeSpan attackInterval) : base(enemy)
        {
            this.attackInterval = attackInterval;
            this.FiredBullets = new List<Bullet.Bullet>();
            this.attackSwitch = true;
        }
        public void attackstrategy(List<Bullet.Bullet> bullets, Vector2 playerPosition, GameTime gameTime,SpriteBatch spriteBatch = null)
        {
            if(spriteBatch!=null)
            this.SB = spriteBatch;
            attackInterval = TimeSpan.FromSeconds(2);
            if (bullets.Count <= 0)
                return;
            attackTimer += gameTime.ElapsedGameTime;
            if (attackTimer > TimeSpan.FromSeconds(1))
            {
                attackSwitch = true;
            }

            if (attackTimer >= attackInterval&&attackSwitch==true)
            {
                attackTimer = TimeSpan.Zero; // Reset timer
                                             // Execute attack logic
                Vector2 direction = Vector2.Normalize(playerPosition - DecoratedEnemy.Position);
                Vector2 bulletVelocity = direction * 8f; // Example speed
                                                         // Potentially activate a bullet here
                Bullet.Bullet bullet= bullets[bullets.Count-1];
                bullets.RemoveAt(bullets.Count-1);
                bullet.IsActive = true;
                FiredBullets.Add(bullet);
                //bullet.Draw(SB);
                
                //spriteBatch.Dispose();
                attackSwitch = false;
                updateAttack(gameTime);
                return;
            }
            
            attackstrategy(bullets, playerPosition, gameTime);
            //attackHelper(attackTimer,bullets, playerPosition, gameTime);
        }
        public void updateAttack(GameTime gameTime)
        {
            if(FiredBullets.Count <=0)
            { return; }
            for(int i = 0; i < FiredBullets.Count; i++)
            {

                FiredBullets[i].Update(gameTime);
                FiredBullets[i].Draw(SB);
                if (FiredBullets[i].IsActive == false) { FiredBullets.RemoveAt(i); } 
            }

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
