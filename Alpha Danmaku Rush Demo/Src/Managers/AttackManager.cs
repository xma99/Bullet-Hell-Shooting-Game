using Alpha_Danmaku_Rush_Demo.Src.Entities.Bullet;
using Alpha_Danmaku_Rush_Demo.Src.Entities.Enemies.Decorator.Attack;
using Alpha_Danmaku_Rush_Demo.Src.Managers.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        SpriteBatch _spriteBatch;
        EnemyManager _enemyManager;
        Stack<MidBossBullet> bulletWave=new Stack<MidBossBullet>();
        Stack<Stack<MidBossBullet>> KiMeRuWaZa=new Stack<Stack<MidBossBullet>>();
        ContentManager _content;
        EnemyBulletType Type;
        int AbilityCount;
        int individualAttackCount = 0;
        Boolean initiated = false;
        TimeSpan firstTimer= TimeSpan.Zero;
        TimeSpan secondTimer= TimeSpan.Zero;
        int w, h;
        Random rand = new Random();
        public MidBoss(EnemyManager enemymanager, SpriteBatch sprite,ContentManager _content,EnemyBulletType type)
        {
            _enemyManager = enemymanager;
            _spriteBatch = sprite;
            this._content = _content;
            this.Type = type;
            w = _enemyManager.getSize()[0];
            h= _enemyManager.getSize()[1];  

        }
        public void attackstrategy(GameTime gameTime)
        {
            //need a ability control logic to make sure perform next ability after previous ability done
            Ability(gameTime);
            //AbilityCount++;
        }
        public void Ability(GameTime gameTime)
        {//Accourding to AbilityCount to perform ability
            switch(AbilityCount)
            {
                case 0:
                    secondTimer += gameTime.ElapsedGameTime;
                    firstTimer += gameTime.ElapsedGameTime;
                    if (firstTimer >= TimeSpan.FromMilliseconds(1000))
                    {
                        firstTimer = TimeSpan.Zero;
                        for (int i = 0; i < 10; i++)
                        {
                            KiMeRuWaZa.Push(new Stack<MidBossBullet>());
                            float x = (float)rand.Next(-20, 20);
                            MidBossBullet bullet = (MidBossBullet)BulletFactory.CreateBullet(_content, new Vector2(w/2, h/2), new Vector2(x, 1), Type);
                            bulletWave.Push(bullet);
                            
                            
                        }
                        
                    }
                    
                    
                       
                    //Stack<MidBossBullet> bullets= new Stack<MidBossBullet>();
                    if (bulletWave.Count > 0)
                    {
                        foreach (var bullet in bulletWave)
                        {
                            bullet.Draw(_spriteBatch);
                            bullet.update1();
                        }

                    }
                    

                    
                    if (secondTimer>=TimeSpan.FromSeconds(10))
                    {
                        AbilityCount++;
                        individualAttackCount = 0;
                        initiated = false; break;
                        
                    }
                    break;


                default:
                    break;
            }

        }
        
    }
    public class FinalBoss: StrategyManager
    {
        public void attackstrategy(GameTime gameTime)
        {

        }
    }

}
