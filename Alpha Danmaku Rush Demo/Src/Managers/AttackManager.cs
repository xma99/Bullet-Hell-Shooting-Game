using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Danmaku_Rush_Demo.Src.Managers
{
    public class Regular:StrategyManager
    {
        SpriteBatch _spriteBatch;
        EnemyManager _enemyManager;
        //bullet control
        private List<EnemyBulletType> enemyBulletTypes;
        private Queue<Bullet> currBullets = new Queue<Bullet>();
        private Bullet testbullet;
        private Queue<Bullet> LoadedBullets = new Queue<Bullet>();
        TimeSpan AttackTimer = TimeSpan.Zero;


        public Regular(EnemyManager enemymanager,SpriteBatch sprite)
        {
            _enemyManager = enemymanager;
            _spriteBatch = sprite;

        }
        public Regular(SpriteBatch spriteBatch, EnemyManager enemyManager, List<EnemyBulletType> enemyBulletTypes,  TimeSpan attackTimer)
        {
            _spriteBatch = spriteBatch;
            _enemyManager = enemyManager;
            this.enemyBulletTypes = enemyBulletTypes;
            //this.currBullets = currBullets;
           // this.testbullet = testbullet;
            //LoadedBullets = loadedBullets;
            AttackTimer = attackTimer;
        }
        public void update(EnemyManager enemyManager)
        {
            _enemyManager=enemyManager;
        }
        public void updateAttack(GameTime gameTime, int interval = 0)
        {

            if (interval != 0)
            {
                //Do control logic
            }
            else
            {
                AttackTimer += gameTime.ElapsedGameTime;
                if (AttackTimer.TotalSeconds > 2)//default attack interval
                {
                    AttackTimer = TimeSpan.Zero;
                    foreach (var enemy in _enemyManager.enemies)
                    {
                        Bullet bullet = enemy.bulletList.Dequeue();
                        LoadedBullets.Enqueue(bullet);
                    }

                }
            }
        }
        public void updateBullet()
        {
            if (LoadedBullets.Count > 0)
            {
                foreach (var item in LoadedBullets)
                {
                    item.Draw(_spriteBatch);
                    item.Update();
                }
            }
        }
        public void attackstrategy(GameTime gameTime)
        {

            updateAttack(gameTime);
            updateBullet();
            
        }

    }
    public class MidBoss : StrategyManager
    {



        public void attackstrategy(GameTime gameTime)
        {

        }
    }
}
